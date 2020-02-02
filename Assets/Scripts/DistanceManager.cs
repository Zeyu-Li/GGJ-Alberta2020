using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public float totalDistance = 9999999;
    [HideInInspector]
    public float currentDistance;

    private float timeLeft = 300;

    // Start is called before the first frame update
    void Start()
    {
        currentDistance = totalDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<EventGenerator>().enginesDamaged)
        {
            timeLeft -= Time.deltaTime * 2;
        }
        else if (GetComponent<EventGenerator>().enginesDamaged)
        {
            timeLeft -= Time.deltaTime;
        }
        currentDistance = timeLeft * 33333.33f;
        Debug.Log(currentDistance);
    }
}
