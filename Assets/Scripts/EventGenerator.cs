using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    public int timeMin;
    public int timeMax;

    private float lastTime = -10f;

    private float time;

    [HideInInspector]
    public bool shieldDamaged = false;
    [HideInInspector]
    public bool enginesDamaged = false;
    [HideInInspector]
    public bool gravityDamaged = false;
    [HideInInspector]
    public bool powerDamaged = false;
    [HideInInspector]
    public bool sensorsDamaged = false;
    [HideInInspector]
    public bool lifeSupportDamaged = false;
    [HideInInspector]
    public bool fireDamaged = false;

    private GameObject jungle;
    private GameObject desert;
    private GameObject ocean;


    // Start is called before the first frame update
    void Start()
    {
        jungle = GameObject.Find("Jungle");
        desert = GameObject.Find("Desert");
        ocean = GameObject.Find("Ocean");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + lastTime)
        {
            lastTime = Time.time;
            time = Random.Range(timeMin, timeMax);
            Damaged damage = (Damaged)Random.Range(0, 0);
            if (damage == Damaged.Shields && !shieldDamaged)
            {
                shieldDamaged = true;
                StartCoroutine(Shield());
            }
        }
    }

    private IEnumerator Shield()
    {
        yield return new WaitForSeconds(1);
        while (shieldDamaged)
        {
            if (jungle.GetComponent<BiomeHealth>().currentHealth > 0)
            {
                jungle.GetComponent<BiomeHealth>().TakeDamge(10);
                yield return new WaitForSeconds(1);
            }
            else if (desert.GetComponent<BiomeHealth>().currentHealth > 0)
            {
                desert.GetComponent<BiomeHealth>().TakeDamge(10);
                yield return new WaitForSeconds(1);
            }
            else if (ocean.GetComponent<BiomeHealth>().currentHealth > 0)
            {
                ocean.GetComponent<BiomeHealth>().TakeDamge(10);
                yield return new WaitForSeconds(1);
                Debug.Log("1");
            }
            else
            {
                break;
            }
        }
    }
}



public enum Damaged
{
    Shields,
    Engines,
    Gravity,
    PowerGrid,
    Sensors,
    LifeSupport,
    Fire
};

