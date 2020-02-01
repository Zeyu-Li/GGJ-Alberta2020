using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeHealth : MonoBehaviour
{
    public float totalHealth = 100;
    [HideInInspector]
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamge(int dmg)
    {
        currentHealth -= dmg;
    }
}
