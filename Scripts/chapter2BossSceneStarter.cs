using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class chapter2BossSceneStarter : MonoBehaviour
{
    private bool doitonce=true;

    public SpeakController sC;
    public Transform bossPos;
    public CinemachineVirtualCamera cinemachine;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("mc"))
        {
            if(collision.gameObject.GetComponent<mcOOP>().wonNumber < 5)
            {
                sC.speakHimself();
                sC.enoghToGetInClass();
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("mc"))
        {
            if(collision.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                cinemachine.Follow = collision.transform;
            }
            else
            {
                if (doitonce)
                {
                    sC.preparetionToBanditBoss();
                    sC.banditBossConversationfirst();
                    doitonce = false;
                }
                cinemachine.Follow = bossPos;

            }
        }
    }
}
