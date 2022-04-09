using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [HideInInspector] public bool battlehasbeenstarted;
    public GameObject chapterOneLocalChange;

    [HideInInspector]public bool gamePaused;
    [Space(10)]
    public GameObject pausePanel;
    private int pausePanelID;
    public GameObject[] pausePanelButtons;
    public GameObject cursorPause;
    [Space(10)]
    public GameObject optionPanel;
    private bool optionPanelBool;
    public GameObject[] optionPanelButtons;
    public GameObject optionCursor;
    private int optionPanelID;
    public Animator optionAnim;
    public Toggle toggleFullScreen;
    public Toggle toggleAudio;
    private bool animationTime;
    [Space(10)]
    public GameObject quitPanel;
    private bool quitPanelBool;
    private int quitPanelID;
    public GameObject[] quitPanelButtons;
    public GameObject quitCursor;
    [Space(10)]
    public AudioSource click;
    public AudioSource getover;
    [Space(10)]

    public GameObject chapterStartObject;
    private float startAnimDuration;
    private bool doitonceStartAnimation;

    // Start is called before the first frame update
    void Start()
    {

        //PausePanel starts
        gamePaused = false;
        pausePanelID = 0;
        optionPanelBool = false;
        optionPanelID = 0;
        quitPanelBool = false;
        pausePanel.SetActive(false);
        animationTime = false;
        //PausePanelEnds
        chapterStartObject.GetComponent<Animator>().SetBool("startAnimationbegin", true);
        startAnimDuration = 0.21f;
        doitonceStartAnimation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doitonceStartAnimation)
        {
            if (startAnimDuration > 0)
            {
                startAnimDuration -= Time.deltaTime;
            }
            else
            {
                chapterStartObject.GetComponent<Animator>().SetBool("startAnimationbegin", false);
                chapterStartObject.SetActive(false);
                doitonceStartAnimation = false;
            }
        }
        if (animationTime)
        {
            return;
        }
        if (gamePaused)
        {
            if (optionPanelBool)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    getover.Play();
                    optionPanelID -= 1;
                    if (optionPanelID < 0)
                    {
                        optionPanelID = optionPanelButtons.Length - 1;
                    }
                    optionCursor.transform.position = new Vector3(optionCursor.transform.position.x, optionPanelButtons[optionPanelID].transform.position.y);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    getover.Play();
                    optionPanelID += 1;
                    if (optionPanelID >= optionPanelButtons.Length)
                    {
                        optionPanelID = 0;
                    }
                    optionCursor.transform.position = new Vector3(optionCursor.transform.position.x, optionPanelButtons[optionPanelID].transform.position.y);
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    click.Play();
                    switch (optionPanelID)
                    {
                        case 0:
                            toggleFullScreen.isOn = !toggleFullScreen.isOn;
                            break;
                        case 1:
                            toggleAudio.isOn = !toggleAudio.isOn;
                            break;
                        default:
                            Debug.LogWarning("IDmissmatch");
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    optionsMenu(true);
                }
                return;
            }
            if (quitPanelBool)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    getover.Play();
                    quitPanelID += 1;
                    if (quitPanelID >= quitPanelButtons.Length)
                    {
                        quitPanelID = 0;
                    }
                    quitCursor.transform.position = quitPanelButtons[quitPanelID].transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    getover.Play();
                    quitPanelID -= 1;
                    if (quitPanelID < 0)
                    {
                        quitPanelID = quitPanelButtons.Length - 1;
                    }
                    quitCursor.transform.position = quitPanelButtons[quitPanelID].transform.position;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    click.Play();
                    switch (quitPanelID)
                    {
                        case 0:
                            quitMenu();
                            break;
                        case 1:
                            SceneManager.LoadScene("MainMenu");
                            break;
                        default:
                            Debug.LogWarning("IDmissmatch");
                            break;
                    }
                }
                return;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                getover.Play();
                pausePanelID -= 1;
                if (pausePanelID < 0)
                {
                    pausePanelID = pausePanelButtons.Length - 1;
                }
                cursorPause.transform.position = pausePanelButtons[pausePanelID].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                getover.Play();
                pausePanelID += 1;
                if (pausePanelID >= pausePanelButtons.Length)
                {
                    pausePanelID = 0;
                }
                cursorPause.transform.position = pausePanelButtons[pausePanelID].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                click.Play();
                switch (pausePanelID)
                {
                    case 0:
                        continueGame();
                        break;
                    case 1:
                        optionsMenu(false);
                        return;
                    case 2:
                        quitMenu();
                        break;
                    default:
                        Debug.LogWarning("IDmissmatch");
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                continueGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseGame();
            }
        }
    }
    public void animStart(string text)
    {
        chapterOneLocalChange.SetActive(true);
        chapterOneLocalChange.GetComponentInChildren<Text>().text = text;
    }
    public void animEnd()
    {
        chapterOneLocalChange.SetActive(false);
    }
    public void sceneChange(bool gochapter)
    {
        if (!gochapter)
        {
            PlayerPrefs.SetInt("saveData", PlayerPrefs.GetInt("saveData") + 1);
            SceneManager.LoadScene("Hub");
            return;
        }
        switch (PlayerPrefs.GetInt("saveData"))
        {
            case 0://class
                SceneManager.LoadScene("Classroom");
                break;
            case 1://street
                SceneManager.LoadScene("Street");
                break;
            case 2://castle
                SceneManager.LoadScene("Palace");
                break;
            case 3://endChpater
                SceneManager.LoadScene("YouhaveWon");
                break;
            default:
                Debug.LogError("saveData MissMatch");
                break;
        }
    }

    //PauseMenus
    private void pauseGame()
    {
        gamePaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 1f;
    }
    private void continueGame()
    {
        gamePaused = false;
        pausePanelID = 0;
        cursorPause.transform.position = pausePanelButtons[pausePanelID].transform.position;
        optionPanelID = 0;
        optionCursor.transform.position = new Vector3(optionCursor.transform.position.x, optionPanelButtons[optionPanelID].transform.position.y);
        quitPanelID = 0;
        optionCursor.transform.position = optionPanelButtons[optionPanelID].transform.position;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    private void optionsMenu(bool isOpen)
    {
        StartCoroutine(waitForAnim(isOpen));
    }
    IEnumerator waitForAnim(bool isOpen)
    {
        animationTime = true;
        if (!isOpen)//if option is close
        {
            optionPanel.SetActive(true);
            optionAnim.SetBool("OptionsActivated", true);
        }
        else
        {
            optionAnim.SetBool("OptionsActivated", false);
        }
        yield return new WaitForSeconds(0.5f);
        if (isOpen)//if option is open
        {
            optionPanel.SetActive(false);
        }
        optionPanelBool = !optionPanelBool;
        animationTime = false;
    }
    private void quitMenu()
    {
        quitPanelBool = !quitPanelBool;
        quitPanel.SetActive(!quitPanel.activeInHierarchy);
    }
    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void audioT()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
