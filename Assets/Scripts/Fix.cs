using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix : MonoBehaviour
{

    //!requires: a collider with isTrigger enabled!!!
    public string fixType = "";
    public float timeRemains;
    public bool isTouchingPlayer;
    public bool damaged = false;
    EventGenerator events;




    //{"type of damage", duration to fix}
    Dictionary<string, float> fixTimeDict = new Dictionary<string, float>(){
        {"shield", 5},
        {"engine", 1},
        {"gravity", 1},
        {"power", 1},
        {"sensor", 1},
        {"lifeSupport", 1},
        {"jungleFire", 1},
        {"jungleGas", 1},
        {"jungleWindow", 1},
        {"desertFire", 1},
        {"desertGas", 1},
        {"desertWindow", 1},
        {"oceanFire", 1},
        {"oceanGas", 1},
        {"oceanWindow", 1},

    };
    void Awake()
    {
        events = GameObject.Find("Manager").GetComponent<EventGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        damaged = isDamaged();

        if (damaged)
        {

            if (Input.GetKey(KeyCode.F) && isTouchingPlayer)
            {
                timeRemains -= Time.deltaTime;
            }
            else
            {
                timeRemains = fixTimeDict[fixType];
            }
            if (timeRemains < 0)
            {
                FixRoom();

            }
        }
        else
        {
            timeRemains = fixTimeDict[fixType];
        }
    }

    private void OnGUI()
    {
        Camera cam = Camera.main;
        if (damaged && isTouchingPlayer)
        {

            GUIStyle s = new GUIStyle("box");
            s.fontSize = 25;
            if (Input.GetKey(KeyCode.F))
            {
                GUI.Box(new Rect(cam.WorldToScreenPoint(transform.position).x - 30, cam.WorldToScreenPoint(transform.position).y + 20, 60, 40), "" + (int)((timeRemains * 100) / fixTimeDict[fixType]) + "%", s);
            }
            else
            {
                GUI.Box(new Rect(cam.WorldToScreenPoint(transform.position).x - 20, cam.WorldToScreenPoint(transform.position).y + 20, 40, 40), "F", s);
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTouchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    void FixRoom()
    {
        events.damageStatus[fixType] = false;
    }
    /*void FixRoom()
    {
        switch (fixType)
        {
            case "shield":
                events.shieldDamaged = false;
                break;

            case "engine":
                events.enginesDamaged = false;
                break;
            case "gravity":
                events.gravityDamaged = false;
                break;
            case "power":
                events.powerDamaged = false;
                break;
            case "sensor":
                events.sensorsDamaged = false;
                break;
            case "lifeSupport":
                events.lifeSupportDamaged = false;
                break;
            case "jungleFire":
                events.jungleFireDamaged = false;
                break;

            case "jungleGas":
                events.jungleGasDamaged = false;
                break;
            case "jungleWindow":
                events.jungleWindowDamaged = false;
                break;
            case "desertFire":
                events.desertFireDamaged = false;
                break;
            case "desertGas":
                events.desertGasDamaged = false;
                break;
            case "desertWindow":
                events.desertWindowDamaged = false;
                break;
            case "oceanFire":
                events.oceanFireDamaged = false;
                break;
            case "oceanGas":
                events.oceanGasDamaged = false;
                break;
            case "oceanWindow":
                events.oceanWindowDamaged = false;
                break;
            default:
                break;
        }
    }*/
    bool isDamaged()
    {

        return events.GetDamaged().Contains(fixType);
    }

    /*bool isDamaged()
    {
        switch (fixType)
        {
            case "shield":
                return events.shieldDamaged;


            case "engine":
                return events.enginesDamaged;

            case "gravity":
                return events.gravityDamaged;

            case "power":
                return events.powerDamaged;

            case "sensor":
                return events.sensorsDamaged;

            case "lifeSupport":
                return events.lifeSupportDamaged;

            case "jungleFire":
                return events.jungleFireDamaged;


            case "jungleGas":
                return events.jungleGasDamaged;

            case "jungleWindow":
                return events.jungleWindowDamaged;

            case "desertFire":
                return events.desertFireDamaged;

            case "desertGas":
                return events.desertGasDamaged;

            case "desertWindow":
                return events.desertWindowDamaged;

            case "oceanFire":
                return events.oceanFireDamaged;

            case "oceanGas":
                return events.oceanGasDamaged;

            case "oceanWindow":
                return events.oceanWindowDamaged;

            default:
                return false;

        }
    }*/
}
