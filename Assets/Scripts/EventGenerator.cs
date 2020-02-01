using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    public int timeMin;
    public int timeMax;

    private float lastTime = -10f;

    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + lastTime)
        {
            time = Random.Range(timeMin, timeMax);
            //Events event = Random.range(0, 6);
        }
    }
}

public enum Events
{
    Shields,
    Engines,
    Gravity,
    PowerGrid,
    Sensors,
    LifeSupport
};

