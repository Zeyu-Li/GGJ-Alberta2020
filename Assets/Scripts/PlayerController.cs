using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;

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

    //Grounded Vars
    bool grounded = false;


    void Update()
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


        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if (rbody.velocity.magnitude > maxGlideSpd)
        {
            rbody.velocity = Vector2.ClampMagnitude(rbody.velocity, maxGlideSpd);
        }
        movementVec = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * jetPackAccel, Input.GetAxis("Vertical") * Time.deltaTime * jetPackAccel);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementVec *= overPressure;
        }
        rbody.AddForce(rotate2DVec(movementVec, rbody.rotation), ForceMode2D.Impulse);




        if (Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, brakeForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rbody.rotation += rotationSpd * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rbody.rotation -= rotationSpd * Time.deltaTime;
        }

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
        {
            grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            //Jumping
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                }
            }

            moveVelocity = 0;

            //Left Right Movement
            if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            moveVelocity = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}