using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
    GameController gC;
    ControlBattle cB;
    Enemy en;
    MCMovement mc;
    SpeakController sC;
    mcOOP mcOOP;

    public GameObject chapterNumber;

    private int mcPoint = 0;
    private int enemyPoint = 0;

    public GameObject battleTus;

    [HideInInspector]public bool enemyAction;
    private int phasenumber;
    private float phaseduration;
    private int selectactionID;
    private bool battleMode;

    public Animator animMC;

    public GameObject balon;
    public SpriteRenderer paper1;
    public SpriteRenderer paper2;
    public SpriteRenderer paper3;
    public SpriteRenderer scissor1;
    public SpriteRenderer scissor2;
    public SpriteRenderer scissor3;
    public SpriteRenderer rock;

    public AudioSource rpss1;
    public AudioSource rpssEnd;

    public AudioSource winAudio;
    public AudioSource loseAudio;

    public AudioSource bgAudioch1;
    public AudioSource bgAudioch2;
    public AudioSource bgAudioch3;
    public AudioSource bgAudioDovus;

    private void Awake()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        mc = GameObject.Find("MC").GetComponent<MCMovement>();
        mcOOP = GameObject.Find("MC").GetComponent<mcOOP>();
        sC = GameObject.Find("SpeakController").GetComponent<SpeakController>();
        cB = gameObject.GetComponent<ControlBattle>();
    }
    void Start()
    {
        battleMode = false;
        enemyAction = false;
        cB.mcAction = false;
        phasenumber = 0;
        phaseduration = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gC.gamePaused)
        {
            return;
        }
        if (enemyAction)
        {
           if (phaseduration > 0)
            {
                phaseduration -= Time.deltaTime;
            }
            else
            {
                phasenumber += 1;
                phaserunner(phasenumber);
            }
        }
/*        else if (battleMode)
        {
            if (chapterNumber.name == "3")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    battleMode = false;
                }
            }
        }*/
    }
    public void startBattle(GameObject ai)
    {
        en = ai.GetComponent<Enemy>();
        if (chapterNumber.name == "0")
        {
            balon.transform.position = new Vector3(ai.transform.position.x - 2.5f, ai.transform.position.y + 2.5f);
        }
        else if(chapterNumber.name == "1")
        {
            balon.transform.position = new Vector3(ai.transform.position.x - 2.5f, ai.transform.position.y + 1.25f);
            bgAudioch1.volume = 0.2f;
        }
        else if(chapterNumber.name == "2")
        {
            balon.transform.position = new Vector3(ai.transform.position.x - 2.5f, ai.transform.position.y + 3.5f);
            bgAudioch2.volume = 0.2f;
        }
        else if(chapterNumber.name == "3")
        {
            balon.transform.position = new Vector3(ai.transform.position.x - 3, ai.transform.position.y + 3.5f);
            bgAudioch3.volume = 0.2f;
        }
        balon.GetComponent<SpriteRenderer>().enabled = true;
        bgAudioDovus.Play();
        StartCoroutine(waitEffects());
    }
    private void phaserunner(int phase)
    {
        if (phase == 1)
        {
            rpss1.Play();
            selectactionID = 1;
        }
        else if (phase == 2)
        {
            rpss1.Play();
            selectactionID = Random.Range(1, 4);
        }
        else if(phase < en.maxPhaseNumber - 1)//lastphase - 1
        {
            rpss1.Play();
            if (en.changeablePhase)
            {
                selectactionID = Random.Range(1, 4);
            }
        }
        else
        {
            rpssEnd.Play();
        }
        switch (selectactionID)
        {
            case 1://taþ
                disableSymbols();
                rock.enabled = true;
                break;
            case 2://kaðýt
                disableSymbols();
                if (phase < en.maxPhaseNumber - 1)
                {
                    paper1.enabled = true;
                }
                else if (phase == en.maxPhaseNumber - 1)
                {
                    paper2.enabled = true;
                }
                else if (phase == en.maxPhaseNumber)
                {
                    paper3.enabled = true;
                }
                break;
            case 3://makas
                disableSymbols();
                if (phase < en.maxPhaseNumber - 1)
                {
                    scissor1.enabled = true;
                }
                else if (phase == en.maxPhaseNumber - 1)
                {
                    scissor2.enabled = true;
                }
                else if (phase == en.maxPhaseNumber)
                {
                    scissor2.enabled = true;
                }
                break;
            default:
                Debug.LogWarning("A problem have been appeared in ai select taþ,kaðýt,makas");
                break;
        }
        if(phase == en.maxPhaseNumber)//last phase
        {
            StartCoroutine(waitLastPhase());
        }
        phaseduration = en.passPhaseTime;
    }
    private void duel(int enemy, int mc)
    {
        if (mc == 0)
        {
            enemyPoint += 1;
            newRound();
            return;
        }
        if (mc == enemy)
        {
            //draw
        }
        else if(enemy == 1)
        {
            if(mc == 2)
            {
                mcPoint += 1;
            }
            else if(mc == 3)
            {
                enemyPoint += 1;
            }
        }
        else if (enemy == 2)
        {
            if (mc == 1)
            {
                enemyPoint += 1;
            }
            else if (mc == 3)
            {
                mcPoint += 1;
            }
        }
        else if (enemy == 3)
        {
            if (mc == 1)
            {
                mcPoint += 1;
            }
            else if (mc == 2)
            {
                enemyPoint += 1;
            }
        }
        newRound();
    }
    private void newRound()
    {
        if (mcPoint == en.maxRound) //could be changed
        {
            bgAudioDovus.Stop();
            winAudio.Play();
            bgAudioch1.volume = 1f;
            bgAudioch2.volume = 1f;
            bgAudioch3.volume = 1f;

            enemyAction = false;
            cB.mcAction = false;
            battleTus.SetActive(false);
            en.rpsEndAnim();
            animMC.SetBool("battleRPS", false);

            if (chapterNumber.name=="0")
            {
                sC.speakHimself();
                sC.afterTrainingWin();
            }
            else
            {
                sC.afterBattleWithMob(en.gameObject);
                sC.afterBattleSpeakMCWON();
            }

            balon.GetComponent<SpriteRenderer>().enabled = false;
            disableSymbols();
            mcOOP.wonNumber += 1;
            phasenumber = 0;
            mcPoint = 0;
            en.isLostToday = true;
            en.makeEnCry();
            Debug.Log("mcWon");
        }
        else if(enemyPoint == en.maxRound)
        {
            bgAudioDovus.Stop();
            loseAudio.Play();
            bgAudioch1.volume = 1f;
            bgAudioch2.volume = 1f;
            bgAudioch3.volume = 1f;

            enemyAction = false;
            cB.mcAction = false;
            battleTus.SetActive(false);
            if (chapterNumber.name == "0")
            {
                sC.speakHimself();
                sC.afterTrainingLost();
            }
            else
            {
                sC.afterBattleWithMob(en.gameObject);
                sC.afterEnWon();
            }

            en.rpsEndAnim();
            animMC.SetBool("battleRPS", false);
            balon.GetComponent<SpriteRenderer>().enabled = false;
            disableSymbols();
            phasenumber = 0;
            enemyPoint = 0;
            Debug.Log("enemyWon");
        }
        else
        {
            StartCoroutine(wait());
        }
    }
    IEnumerator waitEffects()
    {
        Debug.Log("GetReady");
        mc.movable = false;
        mc.interactable = false;
        //battleMusicStarts
        //popUpEffects
        en.rpsStartAnim();
        yield return new WaitForSeconds(1f);
        Debug.Log("Start");
        phaseduration = 0;

        battleMode = true;
        enemyAction = true;
        cB.mcAction = true;
        battleTus.SetActive(true);
        animMC.SetBool("battleRPS", true);
        balon.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator waitLastPhase()
    {
        yield return new WaitForSeconds(en.passPhaseTime);
        duel(selectactionID, cB.mcSelectionID);
    }
    IEnumerator wait()
    {
        enemyAction = false;
        cB.mcAction = false;
        battleTus.SetActive(false);
        en.rpsEndAnim();
        animMC.SetBool("battleRPS", false);
        balon.GetComponent<SpriteRenderer>().enabled = false;
        disableSymbols();

        yield return new WaitForSeconds(0.7f);
        phasenumber = 0;
        phaseduration = en.passPhaseTime;

        enemyAction = true;
        cB.mcAction = true;
        battleTus.SetActive(true);
        en.rpsStartAnim();
        animMC.SetBool("battleRPS", true);
        balon.GetComponent<SpriteRenderer>().enabled = true;
    }
    public bool getBattleMode()
    {
        return battleMode;
    }
    public void setBattleMode(bool temp)
    {
        battleMode = temp;
    }
    private void disableSymbols()
    {
        rock.enabled = false;
        paper1.enabled = false;
        paper2.enabled = false;
        paper3.enabled = false;
        scissor1.enabled = false;
        scissor2.enabled = false;
        scissor3.enabled = false;
    }
}