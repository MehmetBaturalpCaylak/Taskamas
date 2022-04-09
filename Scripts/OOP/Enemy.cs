using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enID;
    public bool isLostToday;
    public bool changeablePhase;
    public int maxPhaseNumber;
    public float passPhaseTime;
    public int maxRound;

    public GameObject chapterNumber;

    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;
    public GameObject sprite4;
    public GameObject sprite5;
    public GameObject sprite6;
    public GameObject sprite7;
    public GameObject sprite8;
    public GameObject sprite9;

    public Animator trainigAnim;

    public Animator ch1Anim1;
    public Animator ch1Anim2;
    public Animator ch1Anim3;
    public Animator ch2Anim1;
    public Animator ch2Anim2;
    public Animator ch2Anim3;
    public Animator ch3Anim1;
    public Animator ch3Anim2;
    public Animator ch3Anim3;



    private void Start()
    {
        isLostToday = false;
        if (chapterNumber.name == "0")
        {
            enID = 9;
        }
        if (chapterNumber.name == "1")
        {
            enID = Random.Range(0, 3);
            switch (enID)
            {
                case 0:
                    sprite1.SetActive(true);
                    ch1Anim1.SetBool("rpsStart", false);
                    break;
                case 1:
                    sprite2.SetActive(true);
                    ch1Anim2.SetBool("rpsStart", false);
                    break;
                case 2:
                    sprite3.SetActive(true);
                    ch1Anim3.SetBool("rpsStart", false);
                    break;
            }
        }
        else if (chapterNumber.name == "2")
        {
            enID = Random.Range(3, 6);
            switch (enID)
            {
                case 3:
                    sprite4.SetActive(true);
                    ch2Anim1.SetBool("rpsStart", false);
                    break;
                case 4:
                    sprite5.SetActive(true);
                    ch2Anim2.SetBool("rpsStart", false);
                    break;
                case 5:
                    sprite6.SetActive(true);
                    ch2Anim3.SetBool("rpsStart", false);
                    break;
            }
        }
        else if (chapterNumber.name == "3")
        {
            enID = Random.Range(6, 9);
            switch (enID)
            {
                case 6:
                    sprite7.SetActive(true);
                    ch3Anim1.SetBool("rpsStart", false);
                    break;
                case 7:
                    sprite8.SetActive(true);
                    ch3Anim2.SetBool("rpsStart", false);
                    break;
                case 8:
                    sprite9.SetActive(true);
                    ch3Anim3.SetBool("rpsStart", false);
                    break;
            }
        }
    }
    public void rpsStartAnim()
    {
        if (enID == 0)
        {
            ch1Anim1.SetBool("rpsStart", true);
            ch1Anim1.SetInteger("id", enID);
        }
        else if (enID == 1)
        {
            ch1Anim2.SetBool("rpsStart", true);
            ch1Anim2.SetInteger("id", enID);
        }
        else if (enID == 2)
        {
            ch1Anim3.SetBool("rpsStart", true);
            ch1Anim3.SetInteger("id", enID);
        }
        else if (enID == 3)
        {
            ch2Anim1.SetBool("rpsStart", true);
            ch2Anim1.SetInteger("id", enID);
        }
        else if (enID == 4)
        {
            ch2Anim2.SetBool("rpsStart", true);
            ch2Anim2.SetInteger("id", enID);
        }
        else if (enID == 5)
        {
            ch2Anim3.SetBool("rpsStart", true);
            ch2Anim3.SetInteger("id", enID);
        }
        else if (enID == 6)
        {
            ch3Anim1.SetBool("rpsStart", true);
            ch3Anim1.SetInteger("id", enID);
        }
        else if (enID == 7)
        {
            ch3Anim2.SetBool("rpsStart", true);
            ch3Anim2.SetInteger("id", enID);
        }
        else if (enID == 8)
        {
            ch3Anim3.SetBool("rpsStart", true);
            ch3Anim3.SetInteger("id", enID);
        }
        else if (enID == 9)
        {
            trainigAnim.SetBool("rpsStart", true);
        }
    }
    public void rpsEndAnim()
    {
        if (enID == 0)
        {
            ch1Anim1.SetBool("rpsStart", false);
        }
        else if (enID == 1)
        {
            ch1Anim2.SetBool("rpsStart", false);
        }
        else if (enID == 2)
        {
            ch1Anim3.SetBool("rpsStart", false);
        }
        else if (enID == 3)
        {
            ch2Anim1.SetBool("rpsStart", false);
        }
        else if (enID == 4)
        {
            ch2Anim2.SetBool("rpsStart", false);
        }
        else if (enID == 5)
        {
            ch2Anim3.SetBool("rpsStart", false);
        }
        else if (enID == 6)
        {
            ch3Anim1.SetBool("rpsStart", false);
        }
        else if (enID == 7)
        {
            ch3Anim2.SetBool("rpsStart", false);
        }
        else if (enID == 8)
        {
            ch3Anim3.SetBool("rpsStart", false);
        }
        else if (enID == 9)
        {
            trainigAnim.SetBool("rpsStart", false);
        }
    }
    public void makeEnCry()
    {
        switch (enID)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                return;
        }
    }
}