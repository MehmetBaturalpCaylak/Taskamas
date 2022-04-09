using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    ControlBattle cB;
    SpeakController sC;
    MCMovement mc;
    GameController gc;
    private int mcPoint = 0;
    private int bossPoint = 0;
    private int eksPoint = 0;

    public GameObject battleTus;
    public GameObject chapterNumber;

    [HideInInspector] public bool bossAction;
    private float phaseDuration;
    private int phaseNumber;
    private int selectactionID;

    public BossOOP bossOOP;

    public Animator animMC;

    public GameObject balon;
    public SpriteRenderer paper1;
    public SpriteRenderer paper2;
    public SpriteRenderer paper3;
    public SpriteRenderer scissor1;
    public SpriteRenderer scissor2;
    public SpriteRenderer scissor3;
    public SpriteRenderer rock;

    public Animator anim;

    public AudioSource rpss1;
    public AudioSource rpssEnd;

    public AudioSource winAudio;
    public AudioSource loseAudio;

    public bool battleMode;

    public AudioSource bgAudioch1;
    public AudioSource bgAudioch2;
    public AudioSource bgAudioch3;
    public AudioSource bgAudioDovus;

    private void Awake()
    {
        mc = GameObject.Find("MC").GetComponent<MCMovement>();
        sC = GameObject.Find("SpeakController").GetComponent<SpeakController>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        cB = gameObject.GetComponent<ControlBattle>();
    }
    // Start is called before the first frame update
    void Start()
    {
        battleMode = false;
        bossAction = false;
        cB.mcAction = false;
        phaseNumber = 0;
        phaseDuration = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.gamePaused)
        {
            return;
        }
        if (battleMode)
        {
            if (bossAction)
            {
                if (phaseDuration > 0)
                {
                    phaseDuration -= Time.deltaTime;
                }
                else
                {
                    phaseNumber += 1;
                    phaserunner(phaseNumber);
                }
            }
        }
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
        else if (phase < bossOOP.maxPhaseNumber - 1)//lastphase - 1
        {
            rpss1.Play();
            if (bossOOP.changeablePhase)
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
                if (phase < bossOOP.maxPhaseNumber - 1)
                {
                    paper1.enabled = true;
                }
                else if (phase == bossOOP.maxPhaseNumber - 1)
                {
                    paper2.enabled = true;
                }
                else if (phase == bossOOP.maxPhaseNumber)
                {
                    paper3.enabled = true;
                }
                break;
            case 3://makas
                disableSymbols();
                if (phase < bossOOP.maxPhaseNumber - 1)
                {
                    scissor1.enabled = true;
                }
                else if (phase == bossOOP.maxPhaseNumber - 1)
                {
                    scissor2.enabled = true;
                }
                else if (phase == bossOOP.maxPhaseNumber)
                {
                    scissor2.enabled = true;
                }
                break;
            default:
                Debug.LogWarning("A problem have been appeared in ai select taþ,kaðýt,makas");
                break;
        }
        if (phase == bossOOP.maxPhaseNumber)//last phase
        {
            StartCoroutine(waitPhaseTime());
        }
        phaseDuration = bossOOP.passPhaseTime;
    }
    private void duel(int enemy, int mc)
    {
        if (mc == 0)
        {
            bossPoint += 1;
            newRound();
            return;
        }
        if (mc == enemy)
        {
            eksPoint += 1;
        }
        else if (enemy == 1)
        {
            if (mc == 2)
            {
                mcPoint += 1;
            }
            else if (mc == 3)
            {
                bossPoint += 1;
            }
        }
        else if (enemy == 2)
        {
            if (mc == 1)
            {
                bossPoint += 1;
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
                bossPoint += 1;
            }
        }
        newRound();
    }
    private void newRound()
    {
        if (mcPoint == bossOOP.maxRound)
        {
            bgAudioDovus.Stop();
            winAudio.Play();
            bgAudioch1.volume = 1f;
            bgAudioch2.volume = 1f;
            bgAudioch3.volume = 1f;
            if (bossOOP.gameObject.name == "BossTeacher")
            {
                sC.conversationAfterWinAgainstTeacher();
            }
            else if (bossOOP.gameObject.name == "BanditBoss")
            {
                sC.battleWonAgainstBandit();
            }
            else if (bossOOP.gameObject.name == "King")
            {
                sC.kingWon();
            }
        }
        else if (bossPoint == bossOOP.maxRound)
        {
            bgAudioDovus.Stop();
            loseAudio.Play();
            bgAudioch1.volume = 1f;
            bgAudioch2.volume = 1f;
            bgAudioch3.volume = 1f;

            bossAction = false;
            cB.mcAction = false;
            battleTus.SetActive(false);

            anim.SetBool("bossRPSAnimStart", false);
            animMC.SetBool("battleRPS", false);

            if (bossOOP.gameObject.name == "BanditBoss")
            {
                sC.battleloseAgainstBandit();
            }
            else if(bossOOP.gameObject.name == "King")
            {
                sC.kingLost();
            }
            balon.GetComponent<SpriteRenderer>().enabled = false;
            disableSymbols();
            phaseNumber = 0;
            bossPoint = 0;
            mcPoint = 0;
        }
        else
        {
            if (chapterNumber.name == "3")
            {
                if ((mcPoint + bossPoint + eksPoint) % 2 == 0)
                {
                    kingAraVer();
                }
                else
                {
                    cB.kingManupTas = false;
                    cB.kingManupMaks = false;
                    cB.kingManupKagt = false;
                }
            }
            StartCoroutine(waitForNewRound());
        }
    }
    public void bossFightStart()
    {
        if (chapterNumber.name == "1")
        {
            bgAudioch1.volume = 0.2f;
        }
        else if (chapterNumber.name == "2")
        {
            bgAudioch2.volume = 0.2f;
        }
        else if (chapterNumber.name == "3")
        {
            bgAudioch3.volume = 0.2f;
        }
        bgAudioDovus.Play();
        StartCoroutine(waitEffects());
    }
    IEnumerator waitForNewRound()
    {
        bossAction = false;
        cB.mcAction = false;
        battleTus.SetActive(false);
        anim.SetBool("bossRPSAnimStart", false);
        animMC.SetBool("battleRPS", false);
        balon.GetComponent<SpriteRenderer>().enabled = false;
        disableSymbols();

        yield return new WaitForSeconds(0.7f);
        phaseNumber = 0;
        phaseDuration = bossOOP.passPhaseTime;

        bossAction = true;
        cB.mcAction = true;
        battleTus.SetActive(true);
        anim.SetBool("bossRPSAnimStart", true);
        animMC.SetBool("battleRPS", true);
        balon.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator waitEffects()
    {
        //battleboss
        yield return new WaitForSeconds(0.7f);
        mcPoint = 0;
        bossPoint = 0;
        battleTus.SetActive(true);
        anim.SetBool("bossRPSAnimStart", true);
        animMC.SetBool("battleRPS", true);
        if (bossOOP.gameObject.name == "BossTeacher")
        {
            balon.transform.position = new Vector3(bossOOP.transform.position.x - 2.5f, bossOOP.transform.position.y + 1.25f);
        }
        else if (bossOOP.gameObject.name == "BanditBoss")
        {
            balon.transform.position = new Vector3(bossOOP.transform.position.x - 3.75f, bossOOP.transform.position.y + 3.75f);
        }
        else if(bossOOP.gameObject.name == "King")
        {
            balon.transform.position = new Vector3(bossOOP.transform.position.x - 5.75f, bossOOP.transform.position.y + 4.75f);
        }
        balon.GetComponent<SpriteRenderer>().enabled = true;
        battleMode = true;
        bossAction = true;
        cB.mcAction = true;
        phaseDuration = 0;
    }
    IEnumerator waitPhaseTime()
    {
        yield return new WaitForSeconds(bossOOP.passPhaseTime);
        duel(selectactionID, cB.mcSelectionID);
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
    public void kingAraVer()
    {
        battleMode = false;
        cB.mcAction = false;
        int a = Random.Range(1, 4);
        switch (a)
        {
            case 1:
                sC.initialKingSPEECH();
                sC.kingManupulationTas();
                break;
            case 2:
                sC.initialKingSPEECH();
                sC.kingManupulationKagt();
                break;
            case 3:
                sC.initialKingSPEECH();
                sC.kingManupulationMakas();
                break;
        }

    }
    public void setCBMCAction(bool temp)
    {
        cB.mcAction = temp;
    }
    public void manupTas()
    {
        cB.kingManupTas = true;
    }
    public void manupKagt()
    {
        cB.kingManupKagt = true;
    }
    public void manupMakas()
    {
        cB.kingManupMaks = true;
    }
}