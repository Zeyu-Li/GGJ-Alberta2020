using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI distance;
    private TextMeshProUGUI jungleHealth;
    private TextMeshProUGUI desertHealth;
    private TextMeshProUGUI oceanHealth;

    private GameObject manager;
    private GameObject jungle;
    private GameObject desert;
    private GameObject ocean;


  
    void Start()
    {
       
        manager = GameObject.Find("Manager");
        jungle = GameObject.Find("Jungle");
        desert = GameObject.Find("Desert");
        ocean = GameObject.Find("Ocean");

        distance = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        jungleHealth = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        desertHealth = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        oceanHealth = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        distance.text = (Mathf.Clamp(manager.GetComponent<DistanceManager>().currentDistance, 0, 9999999) / 1000000).ToString("0.00") + " million Km";
        jungleHealth.text = (Mathf.Clamp(jungle.GetComponent<BiomeHealth>().currentHealth, 0, 100).ToString()) + " %";
        desertHealth.text = (Mathf.Clamp(desert.GetComponent<BiomeHealth>().currentHealth, 0, 100).ToString()) + " %";
        oceanHealth.text = (Mathf.Clamp(ocean.GetComponent<BiomeHealth>().currentHealth, 0, 100).ToString()) + " %";
    }

  
}