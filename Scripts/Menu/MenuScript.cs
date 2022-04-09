using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public Animator optionAnim;
    public Animator playMenuAnim;

    public RectTransform[] buts;
    public RectTransform[] butsAreUSure;
    public RectTransform[] optionsMenu;
    public RectTransform[] playgameMenu;

    public RectTransform cursor;
    public RectTransform cursorAreUSure;
    public RectTransform cursorOptions;
    public RectTransform cursorPlayMenu;

    private bool areUSure;
    private bool playBool;
    private bool optionBool;
    private bool animationTime;

    private int menuID;
    private int playgameID;
    private int areUSureID;
    private int optionsID;

    public GameObject areUSurePanel;
    [Space(15)]
    public AudioSource click;
    public AudioSource getover;
    [Space(10)]
    public AudioSource bgAudio;

    // Start is called before the first frame update
    void Start()
    {
        bgAudio.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (PlayerPrefs.GetInt("saveData") == 0)
        {
            playgameMenu[1].gameObject.SetActive(false);
        }
        
        
        animationTime = false;
        optionBool = false;
        areUSure = false;
        playBool = false;

        menuID = 0;
        optionsID = 0;
        areUSureID = 1;
        areUSurePanel.SetActive(false);

        playMenuAnim.gameObject.SetActive(true);
        changepos();
        
    }   

    // Update is called once per frame
    void Update()
    {
        if (animationTime)
        {
            return;
        }
        if (areUSure)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                areUSureID -= 1;
                if (areUSureID < 0)
                {
                    areUSureID = butsAreUSure.Length - 1;
                }
                quitIte2ChangePos();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                areUSureID += 1;
                if (areUSureID >= butsAreUSure.Length)
                {
                    areUSureID = 0;
                }
                quitIte2ChangePos();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                areUSureID = butsAreUSure.Length - 1;
                quitIte2ChangePos();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (areUSureID)
                {
                    case 0:
                        quitIte2(true);
                        break;
                    case 1:
                        quitIte2(false);
                        break;
                    default:
                        Debug.LogError("IdMissmatch");
                        break;
                }
            }
            return;
        }
        else if (optionBool)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                optionsID += 1;
                if (optionsID >= optionsMenu.Length)
                {
                    optionsID = 0;
                }
                optionsChangePos();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                optionsID -= 1;
                if (optionsID < 0)
                {
                    optionsID = optionsMenu.Length - 1;
                }
                optionsChangePos();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionBut();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (optionsID)
                {
                    case 0:
                        optionToggleChange();
                        break;
                    case 1:
                        optionToggleChange();
                        break;
                    default:
                        Debug.LogError("IdMissmatch");
                        break;
                }
            }
            return;
        }
        else if (playBool)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!playgameMenu[1].gameObject.activeInHierarchy)
                {
                    return;
                }
                else
                {
                    playgameChangePos(false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!playgameMenu[1].gameObject.activeInHierarchy)
                {
                    return;
                }
                else
                {
                    playgameChangePos(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                playBut(true);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (playgameID)
                {
                    case 0:
                        startGame(true);
                        break;
                    case 1:
                        startGame(false);
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuID += 1;
            if (menuID >= buts.Length)
            {
                menuID = 0;
            }
            changepos();
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuID -= 1;
            if(menuID < 0)
            {
                menuID = buts.Length - 1;
            }
            changepos();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuID = buts.Length - 1;
            changepos();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (menuID)
            {
                case 0:
                    playBut(false);
                    break;
                case 1:
                    optionBut();
                    break;
                case 2:
                    quitBut();
                    break;
                default:
                    Debug.LogError("IdMissmatch");
                    break;
            }
        }
    }
    private void changepos()
    {
        cursor.position = new Vector3(cursor.position.x, buts[menuID].position.y, cursor.position.z);
        getover.Play();
    }
    public void playBut(bool isOpen)
    {
        playBool = !playBool;
        playMenuAnim.gameObject.SetActive(true);
        click.Play();
        StartCoroutine(playButRoutine(isOpen));
    }
    IEnumerator playButRoutine(bool isOpen)
    {
        animationTime = true;
        if (isOpen)
        {
            playMenuAnim.SetBool("open", false);
            playMenuAnim.SetBool("close", true);
        }
        else
        {
            playMenuAnim.SetBool("open", true);
            playMenuAnim.SetBool("close", false);
        }
        yield return new WaitForSeconds(0.5f);
        if (!isOpen)
        {
            playMenuAnim.gameObject.SetActive(false);
        }
        animationTime = false;
    }
    private void startGame(bool newGame)
    {
        click.Play();
        if (newGame)
        {
            PlayerPrefs.SetInt("saveData", 0);
        }
        Debug.Log(PlayerPrefs.GetInt("saveData"));
        SceneManager.LoadScene("Hub");

    }
    private void playgameChangePos(bool toRight)
    {
        getover.Play();
        float distance = Mathf.Abs(Mathf.Abs(cursorPlayMenu.position.x) - Mathf.Abs(playgameMenu[playgameID].position.x));
        if (toRight)
        {
            playgameID += 1;
            if (playgameID >= playgameMenu.Length)
            {
                playgameID = 0;
            }
        }
        else
        {
            playgameID -= 1;
            if (playgameID < 0)
            {
                playgameID = playgameMenu.Length - 1;
            }
        }
        cursorPlayMenu.position = new Vector3(playgameMenu[playgameID].position.x - distance, cursorPlayMenu.position.y, cursorPlayMenu.position.z);
    }
    public void optionBut()
    {
        click.Play();
        StartCoroutine(optionAnimationRoutine());
        optionBool = !optionBool;
    }
    IEnumerator optionAnimationRoutine()
    {
        animationTime = true;
        if (optionAnim.GetBool("come"))
        {
            optionAnim.SetBool("come", false);
            optionAnim.SetBool("go", true);
        }
        else
        {
            optionAnim.SetBool("come", true);
            optionAnim.SetBool("go", false);
        }
        yield return new WaitForSeconds(0.5f);
        animationTime = false;
    }
    public void optionsChangePos()
    {
        getover.Play();
        cursorOptions.position = new Vector3(cursorOptions.position.x, optionsMenu[optionsID].position.y, cursor.position.z);
    }
    private void optionToggleChange()
    {
        click.Play();
        optionsMenu[optionsID].GetComponent<Toggle>().isOn = !optionsMenu[optionsID].GetComponent<Toggle>().isOn;
    }
    public void toggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void toggleAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void quitBut()
    {
        click.Play();
        areUSurePanel.SetActive(true);
        areUSure = true;
    }
    public void quitIte2ChangePos()
    {
        getover.Play();
        float distance = Mathf.Abs(Mathf.Abs(cursorAreUSure.position.x) - Mathf.Abs(butsAreUSure[areUSureID].position.x));
        cursorAreUSure.position = new Vector3(butsAreUSure[areUSureID].position.x - distance, butsAreUSure[areUSureID].position.y, cursorAreUSure.position.z);
    }
    public void quitIte2(bool quit)
    {
        click.Play();
        if (quit)
        {
            Application.Quit();
        }
        else
        {
            areUSurePanel.SetActive(false);
            areUSure = false;
        }
    }
}
