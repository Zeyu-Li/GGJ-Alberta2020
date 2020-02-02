using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    //Movement
    public float speed;
    public float jump;
    public float moveVelocity;

    public bool isZeroG = false;

    public float jetPackAccel = 0.3f;
    public float rotationSpd = 0.3f;
    public float maxGlideSpd = 6;
    public float overPressure = 1.6f;
    bool isRunning = false;

    public Vector2 movementVec;
    public Vector2 velocityVec;
    [Range(1f, 2f)]
    public float brakeForce = 1f;

    public float gravity = 20f;
    public float hJGravScaler = 0.6f;



    public float climbSpd = 4f;

    //Grounded Vars
    public bool isGrounded = false;

    public bool isClimbing = false;
    public bool canClimb = false;

    private Rigidbody2D rbody;
    private BoxCollider2D boxCollider;
    public Animator anim;
    public Transform spaceGuy;

    void Awake()
    {
        //anim = gameObject.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        transform.parent = null;
        if (isClimbing)
        {
            Climb();
        }
        else if (isZeroG)
        {
            ClimbCheck();
            ZeroGMove();
        }
        else
        {
            ClimbCheck();
            Movement();
        }

    }

    void ClimbCheck()
    {
        isClimbing = canClimb && (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f);
        anim.SetBool("isClimb", isClimbing);
    }

    void Climb()
    {
        movementVec = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * climbSpd);
        if (Input.GetButtonDown("Jump") || !canClimb)
        {
            isClimbing = false;

        }
        if (Input.GetButtonDown("Jump"))
        {
            movementVec.y += jump;
            anim.SetTrigger("Jump");
        }

        rbody.velocity = movementVec;
    }

    void ZeroGMove()
    {



        if (rbody.velocity.magnitude > maxGlideSpd)
        {
            rbody.velocity = Vector2.ClampMagnitude(rbody.velocity, maxGlideSpd);
        }
        movementVec = new Vector2(Input.GetAxis("Horizontal") * Time.fixedDeltaTime * jetPackAccel, Input.GetAxis("Vertical") * Time.fixedDeltaTime * jetPackAccel);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementVec *= overPressure;
        }
        rbody.AddForce(Rotate2DVec(movementVec, rbody.rotation), ForceMode2D.Impulse);

        if (Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, brakeForce * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rbody.rotation += rotationSpd * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rbody.rotation -= rotationSpd * Time.fixedDeltaTime;
        }
        //debug
        velocityVec = rbody.velocity;
    }

    Vector2 Rotate2DVec(Vector2 v, float angle)
    {

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);

    }
    void Movement()
    {
        // check for movement after being knocked back


        movementVec = new Vector2(speed * Input.GetAxis("Horizontal"), rbody.velocity.y);
        //Jumping
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                movementVec.y = jump;
            }
        }
        else
        {
            float downSpeed = -gravity * Time.fixedDeltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                downSpeed *= hJGravScaler;
            }
            movementVec.y += downSpeed;
        }

        //Left Right Movement

        /* !!for testing
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }*/

        rbody.velocity = movementVec;
        anim.SetFloat("Vspeed", movementVec.y);
        if(movementVec.x !=0) anim.SetFloat("Hspeed", 1);
        else anim.SetFloat("Hspeed", 0);

        spaceGuy.localScale = new Vector3( Mathf.Sign(movementVec.x),1,1);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            canClimb = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            canClimb = false;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && other.contacts[0].normal == Vector2.up)
        {
            isGrounded = true;
            isClimbing = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isGrounded = true;
            isClimbing = false;
            transform.SetParent(other.gameObject.transform);
        }


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isClimbing = false;
            canClimb = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isGrounded = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("startLevel");
        }
    }

}