using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class GlobalLightController : MonoBehaviour
{
    public float maxTimeOn = 1f;
    public float minTimeOn = 15f;
    public float maxTimeOff = 0.05f;
    public float minTimeoff = 0.05f;
    private float timeOn = 10f;

    private float lastChange = -10f;
    private UnityEngine.Experimental.Rendering.Universal.Light2D lt;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastChange + timeOn)
        {
            lastChange = Time.time;
            StartCoroutine(Flicker());
            timeOn = Random.Range(minTimeOn, maxTimeOn);
        }   
    }

    private IEnumerator Flicker()
    {
        for (int i = 0; i < 4; i++)
        {
            lt.enabled = !lt.enabled;
            yield return new WaitForSeconds(Random.Range(minTimeoff, maxTimeOff));
        }
    }
}
