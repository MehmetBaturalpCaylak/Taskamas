using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamSettings : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name == "StartMapTriggerF1"|| gameObject.name == "StartMapTriggerF2"|| gameObject.name == "StartMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 2f;
            }
        }
        else if(gameObject.name== "EndMapTriggerF1"|| gameObject.name == "EndMapTriggerF2"|| gameObject.name == "EndMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 2f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.name == "StartMapTriggerF1" || gameObject.name == "StartMapTriggerF2" || gameObject.name == "StartMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 2f;
            }
        }
        else if (gameObject.name == "EndMapTriggerF1" || gameObject.name == "EndMapTriggerF2" || gameObject.name == "EndMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 2f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "StartMapTriggerF1" || gameObject.name == "StartMapTriggerF2" || gameObject.name == "StartMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;
            }
        }
        else if (gameObject.name == "EndMapTriggerF1" || gameObject.name == "EndMapTriggerF2" || gameObject.name == "EndMapTriggerF3")
        {
            if (collision.CompareTag("mc"))
            {
                cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;
            }
        }
    }
}
