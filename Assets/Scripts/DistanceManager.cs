using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            timeLeft -= Time.deltaTime;
        }
        else if (GetComponent<EventGenerator>().enginesDamaged)
        {
            timeLeft -= Time.deltaTime / 2;
        }
        currentDistance = timeLeft * 33333.33f;

        if (currentDistance <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
