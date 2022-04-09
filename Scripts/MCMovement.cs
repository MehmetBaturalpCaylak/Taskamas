using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MCMovement : MonoBehaviour
{
    GameController gC;
    SpeakController sC;
    mcOOP mcOOP;

    public GameObject chapternumber;

    bool stop1 = true;
    bool stop2 = true;
    bool stop3 = true;
    bool stop4 = true;

    public Transform f1Start;
    public Transform f1FromStair;
    public Transform f2FromF1;
    public Transform f2FromF3;
    public Transform f2FromClass;
    public Transform classObject;
    public Transform f3FromF2;
    public Transform battlePosF1;
    public Transform battlePosF2;
    public Transform battlePosF3;
    //ch3
    public Transform ch3ToBossRoom;
    public Transform ch3ToHall;
    public Transform ch3LockCam;

    private AIBattle aiBattle;

    private GameObject target;
    private Vector3 initialTargetPos;
    private Vector3 initialTargetScale;
    private float movementTimeBeetwenTransition;
    public CinemachineVirtualCamera cinemachine;

    [HideInInspector]public bool movable;
    [HideInInspector]public bool interactable;
    [HideInInspector]public bool battleModeOpenned;
    [HideInInspector] public bool battlebossModeOpenned;
    [HideInInspector] public bool speakModeOpenned;
    [SerializeField]private float speed;
    private Animator anim;

    public AudioSource walk;

    public AudioSource bgAudioch1;
    public AudioSource bgAudioch2;
    public AudioSource bgAudioch3;
    public AudioSource bgAudioHub;

    public AudioSource pillarmanAudio;
    private bool doitonceAudio;

    private bool whydontyouworkinstart;
    // Start is called before the first frame update
    void Start()
    {
        target = null;
        movable = true;
        interactable = false;
        anim = gameObject.GetComponentInChildren<Animator>();
        aiBattle = GameObject.Find("BattleMode").GetComponent<AIBattle>();
        sC = GameObject.Find("SpeakController").GetComponent<SpeakController>();
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        mcOOP = gameObject.GetComponent<mcOOP>();
        battlebossModeOpenned = false;
        speakModeOpenned = false;
        whydontyouworkinstart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (whydontyouworkinstart)
        {
            if (chapternumber.name == "1")
            {
                bgAudioch1.Play();
                sC.speakHimself();
                sC.schoolStarter();
            }
            else if (chapternumber.name == "2")
            {
                bgAudioch2.Play();
                sC.speakHimself();
                sC.streetStarter();
            }
            else if (chapternumber.name == "3")
            {
                bgAudioch3.Play();
                sC.speakHimself();
                sC.palaceStarter();
            }
            else if (chapternumber.name == "0")
            {
                bgAudioHub.Play();
                sC.speakHimself();
                sC.hubStarter();
            }
            whydontyouworkinstart = false;
        }
        if (gC.gamePaused)
        {
            return;
        }
        if (movable)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                stop1 = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("running", true);
                if (doitonceAudio)
                {
                    walk.Play();
                    doitonceAudio = false;
                }
            }
            else
            {
                stop1 = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                stop2 = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("running", true);
                if (doitonceAudio)
                {
                    walk.Play();
                    doitonceAudio = false;
                }
            }
            else
            {
                stop2 = true;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                stop3 = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
                anim.SetBool("running", true);
                if (doitonceAudio)
                {
                    walk.Play();
                    doitonceAudio = false;
                }
            }
            else
            {
                stop3 = true;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                stop4 = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
                anim.SetBool("running", true);
                if (doitonceAudio)
                {
                    walk.Play();
                    doitonceAudio = false;
                }
            }
            else
            {
                stop4 = true;
            }
        }
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
        if (!speakModeOpenned)//worst code ever 
        {
            if (!battlebossModeOpenned)
            {
                if (battleModeOpenned)
                {
                    if (movementTimeBeetwenTransition <= 0)
                    {
                        if (!aiBattle.getBattleMode())//If the game is not in battle
                        {
                            movable = true;
                            if (target != null)
                            {
                                if (!target.CompareTag("interactableDOORSTAIR"))
                                {
                                    resetTarget();
                                }
                                battleModeOpenned = false;
                            }
                        }
                    }
                    else
                    {
                        movementTimeBeetwenTransition -= Time.deltaTime;
                    }
                }
                else
                {
                    if (movementTimeBeetwenTransition <= 0)
                    {
                        if (chapternumber.name != "0")
                        {
                            cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 2f;
                            gC.animEnd();
                        }
                        movable = true;
                    }
                    else
                    {
                        movementTimeBeetwenTransition -= Time.deltaTime;
                    }
                }
            }
        }
        if ((stop1 && stop2 && stop3 && stop4) || speakModeOpenned || battlebossModeOpenned || battleModeOpenned)
        {
            doitonceAudio = true;
            walk.Stop();
            anim.SetBool("running", false);
        }
    }
    private void FixedUpdate()
    {
    }
    private void interact()
    {
        if (target.name == "MirrorCollider")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - 1, gameObject.transform.position.z);
            sC.speakHimself();
            sC.training();
        }
        else if (target.name == "GoChapter")
        {
            gC.sceneChange(true);
        }
        else if (target.name == "StairTo2rdFloorFrom1stFloor")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("2. Kat", f2FromF1.position, 0, 3.5f));
        }
        else if (target.name == "DoorTo1B")
        {
            sC.speakHimself();
            sC.doorTo1B();
        }
        else if (target.name == "StairTo3rdFloorFrom2ndFloor")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("3. Kat", f3FromF2.position, 0, 3.5f));
        }
        else if (target.name == "DoorTo2B")
        {
            if (mcOOP.wonNumber < 5)
            {
                sC.speakHimself();
                sC.enoghToGetInClass();
            }
            else
            {
                movementTimeBeetwenTransition = 1.5f;
                movable = false;
                StartCoroutine(Action("Sýnýf", classObject.position, 0, 0));
            }
        }
        else if (target.name == "StairsTo1stFloorFrom2ndFloor")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("1. Kat", f1FromStair.position, 0, 0));
        }
        else if (target.name == "StairToRoof")
        {
            sC.speakHimself();
            sC.stairToRoof();
        }
        else if (target.name == "DoorTo3B")
        {
            sC.speakHimself();
            sC.doorTo3B();
        }
        else if (target.name == "StairsTo2ndFloorFrom3rdFloor")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("2. Kat", f2FromF3.position, 0, 0));
        }
        else if (target.name == "ToTheHall")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("2. Kat", f2FromClass.position, 0, 0));
        }
        else if(target.name == "KingRoom")
        {
            if (mcOOP.wonNumber < 7)
            {
                sC.speakHimself();
                sC.enoghToGetInClass();
            }
            else
            {
                Debug.Log(mcOOP.wonNumber);
                movementTimeBeetwenTransition = 1.5f;
                StartCoroutine(pillarmanSound());
                movable = false;
                StartCoroutine(Action("Kralin Odasi", ch3ToBossRoom.position, 0, 0));
            }
        }
        else if(target.name == "Hall")
        {
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            StartCoroutine(Action("Saray", ch3ToHall.position, 0, 0));
        }
        else if (target.name == "Portre")
        {
            sC.speakHimself();
            sC.portreSpeak();
        }
        else if (target.CompareTag("interactableBOSS"))
        {
            speakModeOpenned = true;
            battlebossModeOpenned = true;
            if (target.name == "BossTeacher")
            {
                //Birbirlerine Döndürecek
                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                }
                gameObject.transform.position = new Vector3(target.transform.position.x - 5, target.transform.position.y, gameObject.transform.position.z);
                sC.preparetionToTeacherBoss();
                sC.conversationWithTeacher();
            }
            else if (target.name == "BanditBoss")
            {
                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                }
                gameObject.transform.position = new Vector3(target.transform.position.x - 5, target.transform.position.y, gameObject.transform.position.z);
                sC.preparetionToBanditBoss();
                sC.battleStartwithBandit();
            }
            else if(target.name == "King")
            {
                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                }
                gameObject.transform.position = new Vector3(target.transform.position.x - 7, target.transform.position.y, gameObject.transform.position.z);
                sC.initialKingSPEECH();
                sC.kingSpeech(target);
            }
        }
        else if (target.CompareTag("interactableNPC"))
        {
            Debug.Log(target.name);
            movementTimeBeetwenTransition = 1.5f;
            movable = false;
            //Birbirlerine Döndürecek
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            if (target.transform.localScale.x > 0)
            {
                target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (!target.GetComponent<Enemy>().isLostToday)//O gün kaybetmediyse
            {
                //BattleModePositionAyarlama
                //y<5=f1 y<30=f2 y<55=f3
                if (target.transform.position.y < 5)
                {
                    target.transform.position = new Vector3(gameObject.transform.position.x+5, battlePosF1.transform.position.y, target.transform.position.z);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, battlePosF1.transform.position.y, gameObject.transform.position.z);
                }
                else if (target.transform.position.y < 30)
                {
                    target.transform.position = new Vector3(gameObject.transform.position.x + 5, battlePosF2.transform.position.y, target.transform.position.z);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, battlePosF2.transform.position.y, gameObject.transform.position.z);
                }
                else if (target.transform.position.y < 55)
                {
                    target.transform.position = new Vector3(gameObject.transform.position.x + 5, battlePosF3.transform.position.y, target.transform.position.z);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, battlePosF3.transform.position.y, gameObject.transform.position.z);
                }
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                battleModeOpenned = true;
                aiBattle.startBattle(target);
            }
            else
            {
                sC.speakBetweentTwo(target);
                sC.afterAlreadyWon();
            }
        }
    }
    public void resetTarget()
    {
        target.transform.position = new Vector3(initialTargetPos.x, initialTargetPos.y, initialTargetPos.z);
        target.transform.localScale = new Vector3(initialTargetScale.x, initialTargetScale.y, initialTargetScale.z);
    }
    //*********************************************************************************************************************************************\\
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("interactableNPC"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
            interactable = true;
            initialTargetPos = target.transform.position;
            initialTargetScale = target.transform.localScale;
        }
        else if (collision.gameObject.CompareTag("interactableBOSS"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
            interactable = true;
        }
        else if (collision.gameObject.CompareTag("interactableDOORSTAIR"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
            interactable = true;
        }
    }
    //*********************************************************************************************************************************************\\
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("interactableNPC"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("interactableBOSS"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
            interactable = true;
        }
        else if (collision.gameObject.CompareTag("interactableDOORSTAIR"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = true;
            target = collision.gameObject;
        }
    }
    //*********************************************************************************************************************************************\\
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("interactableNPC"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = false;
            interactable = false;
        }
        else if (collision.gameObject.CompareTag("interactableBOSS"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = false;
            interactable = false;
        }
        else if (collision.gameObject.CompareTag("interactableDOORSTAIR"))
        {
            SpriteRenderer[] sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            while (c < sprite.Length)
            {
                if (sprite[c].gameObject.name == "interact")
                {
                    break;
                }
                c++;
            }
            sprite[c].enabled = false;
            interactable = false;
        }
    }
    //*********************************************************************************************************************************************\\
    IEnumerator Action(string text,Vector3 pos,float offsetx,float offsety)
    {
        gC.animStart(text);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.position = pos;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
        cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(offsetx, offsety, 0);
        if (chapternumber.name == "3")
        {
            if (cinemachine.Follow.gameObject.CompareTag("mc"))
            {
                cinemachine.Follow = ch3LockCam;
            }
            else
            {
                cinemachine.Follow = gameObject.transform;
            }
        }
    }
    IEnumerator pillarmanSound()
    {
        yield return new WaitForSeconds(1.5f);
        pillarmanAudio.Play();
    }
}