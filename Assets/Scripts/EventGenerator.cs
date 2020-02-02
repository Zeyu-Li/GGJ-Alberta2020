﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    public int majorTimeMin;
    public int majorTimeMax;

    private float lastMajorTime = -10f;

    private float majorTime;

    public int minorTimeMin;
    public int minorTimeMax;

    private float lastMinorTime = -10f;

    private float minorTime;

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
    public bool jungleFireDamaged = false;
    [HideInInspector]
    public bool desertFireDamaged = false;
    [HideInInspector]
    public bool oceanFireDamaged = false;

    [HideInInspector]
    public bool jungleGasDamaged = false;
    [HideInInspector]
    public bool desertGasDamaged = false;
    [HideInInspector]
    public bool oceanGasDamaged = false;

    [HideInInspector]
    public bool jungleWindowDamaged = false;
    [HideInInspector]
    public bool desertWindowDamaged = false;
    [HideInInspector]
    public bool oceanWindowDamaged = false;

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
        Major();
        Minor();
    }

    private void Major()
    {
        if (Time.time > majorTime + lastMajorTime)
        {
            lastMajorTime = Time.time;
            majorTime = Random.Range(majorTimeMin, majorTimeMax);
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
    }

    private void Minor()
    {
        if (Time.time > minorTime + lastMinorTime)
        {
            lastMinorTime = Time.time;
            minorTime = Random.Range(minorTimeMin, minorTimeMax);
            MinorDamaged damage = (MinorDamaged)Random.Range(0, 3);
            if (damage == MinorDamaged.Fire)
            {
                int biome = Random.Range(0, 3);
                if (biome == 0)
                {
                    jungleFireDamaged = true;
                }
                else if (biome == 1)
                {
                    desertFireDamaged = true;
                }
                else if (biome == 2)
                {
                    oceanFireDamaged = true;
                }
            }
            else if (damage == MinorDamaged.Gas)
            {
                int biome = Random.Range(0, 3);
                if (biome == 0)
                {
                    jungleGasDamaged = true;
                }
                else if (biome == 1)
                {
                    desertGasDamaged = true;
                }
                else if (biome == 2)
                {
                    oceanGasDamaged = true;
                }
            }
            else if (damage == MinorDamaged.Window)
            {
                int biome = Random.Range(0, 3);
                if (biome == 0)
                {
                    jungleWindowDamaged = true;
                }
                else if (biome == 1)
                {
                    desertWindowDamaged = true;
                }
                else if (biome == 2)
                {
                    oceanWindowDamaged = true;
                }
            }
        }
    }

    private IEnumerator Shield()
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

    private IEnumerator LifeSupport()
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

public enum MinorDamaged
{
    Fire,
    Window,
    Gas
};

public enum Damaged
{
    Shields,
    Engines,
    Gravity,
    PowerGrid,
    Sensors,
    LifeSupport
};