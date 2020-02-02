using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    public int timeMin;
    public int timeMax;

    private float lastTime = -10f;

    private float time;

    //[HideInInspector]
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

    private GameObject player;

    private GameObject globalLight;


    // Start is called before the first frame update
    void Start()
    {
        jungle = GameObject.Find("Jungle");
        desert = GameObject.Find("Desert");
        ocean = GameObject.Find("Ocean");

        player = GameObject.Find("Player");

        globalLight = GameObject.Find("Global Light 2D");
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (Time.time > time + lastTime)
=======
        //Major();
        //Minor();
    }

    private void Major()
    {
        if (Time.time > majorTime + lastMajorTime)
>>>>>>> Stashed changes
        {
            lastTime = Time.time;
            time = Random.Range(timeMin, timeMax);
            Damaged damage = (Damaged)Random.Range(0, 2);
            if (damage == Damaged.Shields && !shieldDamaged)
            {
                shieldDamaged = true;
                StartCoroutine(Shield());
            }
            else if (damage == Damaged.Engines && !enginesDamaged)
            {
                enginesDamaged = true;
            }
            else if (damage == Damaged.Gravity && !gravityDamaged)
            {
                gravityDamaged = true;
                player.GetComponent<PlayerController>().isZeroG = true;
            }
            else if (damage == Damaged.PowerGrid && !powerDamaged)
            {
                powerDamaged = true;
                globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
            }
            else if (damage == Damaged.Sensors && !sensorsDamaged)
            {
                sensorsDamaged = true;
            }
            else if (damage == Damaged.LifeSupport && !lifeSupportDamaged)
            {
                lifeSupportDamaged = true;
                StartCoroutine(LifeSupport());
            }


        }

        IEnumerator Shield()
        {
            yield return new WaitForSeconds(30);
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

        IEnumerator LifeSupport()
        {
            yield return new WaitForSeconds(30);
            while (lifeSupportDamaged)
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

