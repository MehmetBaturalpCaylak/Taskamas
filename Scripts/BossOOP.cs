using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOOP : MonoBehaviour
{
    public GameObject chapterNumber;
    [HideInInspector] public bool changeablePhase;
    [HideInInspector] public int maxPhaseNumber;
    [HideInInspector] public float passPhaseTime;
    [HideInInspector] public int maxRound;
    private void Start()
    {
        if(chapterNumber.name == "1")
        {
            changeablePhase = false;
            maxPhaseNumber = 5;
            passPhaseTime = 0.65f;
            maxRound = 3;
        }
        else if (chapterNumber.name == "2")
        {
            changeablePhase = true;
            maxPhaseNumber = 6;
            passPhaseTime = 0.55f;
            maxRound = 5;
        }
        else if (chapterNumber.name == "3")
        {
            changeablePhase = true;
            maxPhaseNumber = 6;
            passPhaseTime = 0.4f;
            maxRound = 7;
        }
    }
}
