using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormControl : MonoBehaviour
{


    public Transform from;
    public Transform to;

    public float travelTime = 2;
    private float increment = 0;
    private float step;
    private int dir = 1;
    private
    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        step = Time.fixedDeltaTime / travelTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dir == 1)
        {
            rbody.MovePosition(Vector3.Lerp(from.position, to.position, increment));
            increment += step;
            if (increment >= 1)
            {
                dir = -1;
                increment = 0;
            }
        }
        else
        {
            rbody.MovePosition(Vector3.Lerp(to.position, from.position, increment));
            increment += step;
            if (increment >= 1)
            {
                dir = 1;
                increment = 0;
            }
        }
    }
    
}
