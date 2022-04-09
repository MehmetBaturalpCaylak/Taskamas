using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lastSceneManager : MonoBehaviour
{
    private bool ft;
    // Start is called before the first frame update
    void Start()
    {
        ft = true;
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ft)
            {
                StartCoroutine(wait());
            }
        }
    }
    IEnumerator wait()
    {
        ft = false;
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }
}
