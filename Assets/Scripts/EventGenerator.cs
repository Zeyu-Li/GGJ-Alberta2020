using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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


    public Dictionary<string, bool> damageStatus = new Dictionary<string, bool>(){
         {"shield", false},
        {"engine", false},
        {"gravity", false},
        {"power", false},
        {"sensor", false},
        {"lifeSupport", false},
        {"jungleFire", false},
        {"jungleGas", false},
        {"jungleWindow", false},
        {"desertFire", false},
        {"desertGas", false},
        {"desertWindow", false},
        {"oceanFire", false},
        {"oceanGas", false},
        {"oceanWindow", false}
    };
    private GameObject jungle;
    private GameObject desert;
    private GameObject ocean;

    private GameObject player;

    private GameObject globalLight;
    private GameObject engineParticles;


    // Start is called before the first frame update
    void Start()
    {
        jungle = GameObject.Find("Jungle");
        desert = GameObject.Find("Desert");
        ocean = GameObject.Find("Ocean");

        player = GameObject.Find("Player");

        globalLight = GameObject.Find("Global Light 2D");
        engineParticles = GameObject.FindGameObjectWithTag("EngineParticle");

    }

    // Update is called once per frame
    void Update()
    {
        Major();
        Minor();
        if (jungle.GetComponent<BiomeHealth>().currentHealth <= 0 &&
            desert.GetComponent<BiomeHealth>().currentHealth <= 0 &&
            ocean.GetComponent<BiomeHealth>().currentHealth <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
        CheckFixing();
    }

    public List<string> GetDamaged()
    {
        List<string> temp = new List<string>();
        foreach (string key in damageStatus.Keys)
        {
            if (damageStatus[key])
            {
                temp.Add(key);
            }
        }
        return temp;
    }

    void CheckFixing()
    {
        if (damageStatus["gravity"] == false)
        {
            player.GetComponent<PlayerController>().isZeroG = false;
        }
        if (damageStatus["power"] == false)
        {
            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
        }
        if (damageStatus["jungleFire"] == false)
        {
            jungle.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["desertFire"] == false)
        {
            desert.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["oceanFire"] == false)
        {
            ocean.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["jungleGas"] == false)
        {
            jungle.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["desertGas"] == false)
        {
            jungle.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["oceanGas"] == false)
        {
            jungle.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (damageStatus["engine"] == false)
        {
            engineParticles.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    private void Major()
    {
        if (Time.time > majorTime + lastMajorTime)
        {
            // TODO: Start alarm
            lastMajorTime = Time.time;
            majorTime = Random.Range(majorTimeMin, majorTimeMax);
            Damaged damage = (Damaged)Random.Range(0, 6);
            if (damage == Damaged.Shields && !shieldDamaged)
            {
                damageStatus["shield"] = true;
                StartCoroutine(Shield());
            }
            else if (damage == Damaged.Engines && !enginesDamaged)
            {
                damageStatus["engine"] = true;
                engineParticles.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (damage == Damaged.Gravity && !gravityDamaged)
            {
                damageStatus["gravity"] = true;
                player.GetComponent<PlayerController>().isZeroG = true;
            }
            else if (damage == Damaged.PowerGrid && !powerDamaged)
            {
                damageStatus["power"] = true;
                globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
            }
            else if (damage == Damaged.Sensors && !sensorsDamaged)
            {
                damageStatus["sensor"] = true;
            }
            else if (damage == Damaged.LifeSupport && !lifeSupportDamaged)
            {
                damageStatus["lifeSupport"] = true;
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
            MinorDamaged damage = (MinorDamaged)Random.Range(0, 2);
            if (damage == MinorDamaged.Fire)
            {
                int biome = Random.Range(0, 3);
                if (biome == 0)
                {
                    damageStatus["jungleFire"] = true;
                    jungle.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (biome == 1)
                {
                    damageStatus["desertFire"] = true;
                    desert.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (biome == 2)
                {
                    damageStatus["oceanFire"] = true;
                    ocean.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else if (damage == MinorDamaged.Gas)
            {
                int biome = Random.Range(0, 3);
                if (biome == 0)
                {
                    damageStatus["jungleGas"] = true;
                    jungle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (biome == 1)
                {
                    damageStatus["desertGas"] = true;
                    jungle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (biome == 2)
                {
                    damageStatus["oceanGas"] = true;
                    jungle.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    private IEnumerator Shield()
    {
        yield return new WaitForSeconds(30);
        while (damageStatus["shield"])
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
        while (damageStatus["lifeSupport"])
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