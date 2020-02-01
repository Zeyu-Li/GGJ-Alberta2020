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

    bool zeroG = false;

    public float jetPackAccel = 0.3f;
    public float rotationSpd = 0.3f;
    public float maxGlideSpd = 6;

    [Range(0f, 1f)]
    public float brakeForce = 0.6f;

    //Grounded Vars
    bool grounded = false;


    void Update()
    {
        if (zeroG)
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
        rbody.velocity += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rbody.velocity *= Time.deltaTime * jetPackAccel;


        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q))
        {
            rbody.velocity = Vector2.ClampMagnitude(rbody.velocity, rbody.velocity.magnitude * brakeForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rbody.rotation += rotationSpd * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rbody.rotation -= rotationSpd * Time.deltaTime;
        }


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