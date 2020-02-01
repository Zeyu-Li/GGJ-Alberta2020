using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public CinemachineConfiner conf;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if(other.transform.name == "Player")
        {
            conf.m_BoundingShape2D = this.GetComponent<Collider2D>();
            Debug.Log("Player");
        }
    }
}
