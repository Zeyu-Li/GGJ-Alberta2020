using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{


    Dictionary<string, Image> errorDots;
    EventGenerator events;
    void Start()
    {
        events = GameObject.Find("Manager").GetComponent<EventGenerator>();
        errorDots = new Dictionary<string, Image>(){
         {"shield", transform.GetChild(0).GetComponent<Image>()},
        {"engine", transform.GetChild(1).GetComponent<Image>()},
        {"gravity", transform.GetChild(2).GetComponent<Image>()},
        {"power", transform.GetChild(3).GetComponent<Image>()},
        {"sensor", transform.GetChild(4).GetComponent<Image>()},
        {"lifeSupport", transform.GetChild(5).GetComponent<Image>()},
        {"jungleFire", transform.GetChild(6).GetComponent<Image>()},
        {"jungleGas", transform.GetChild(6).GetComponent<Image>()},
        {"jungleWindow", transform.GetChild(6).GetComponent<Image>()},
        {"desertFire", transform.GetChild(7).GetComponent<Image>()},
        {"desertGas", transform.GetChild(7).GetComponent<Image>()},
        {"desertWindow", transform.GetChild(7).GetComponent<Image>()},
        {"oceanFire", transform.GetChild(8).GetComponent<Image>()},
        {"oceanGas", transform.GetChild(8).GetComponent<Image>()},
        {"oceanWindow", transform.GetChild(8).GetComponent<Image>()}
    };
        foreach (string key in errorDots.Keys)
        {

            errorDots[key].enabled = (false);
        }
    }

    void Update()
    {
        if (events.damageStatus["sensor"])
        {

            foreach (string key in errorDots.Keys)
            {
                errorDots[key].enabled = (int)Time.time % 2 == 1;
            }
        }
        else
        {


            foreach (string key in events.GetDamaged())
            {


                if (events.damageStatus[key])
                {

                    errorDots[key].enabled = (int)Time.time % 2 == 1;
                }
                else
                {
                    errorDots[key].enabled = false;
                }
            }
        }
    }
}
