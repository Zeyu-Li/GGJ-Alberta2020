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

    //Grounded Vars
    bool grounded = false;

    void Update()
    {
            // check for movement after being knocked back
            {
                grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

                //Jumping
                if (Input.GetButtonDown("Vertical"))
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