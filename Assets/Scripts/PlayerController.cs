using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float airStrafe = 0.4f;

    //Grounded Vars
    public bool isGrounded = false;
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {

        if (isZeroG)
        {
            zeroGMove();
        }
        else
        {
            movement();
        }

    }
    void zeroGMove()
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
        rbody.AddForce(rotate2DVec(movementVec, rbody.rotation), ForceMode2D.Impulse);

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

    Vector2 rotate2DVec(Vector2 v, float angle)
    {

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);

    }
    void movement()
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
            movementVec.x *= airStrafe;


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


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("test");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}