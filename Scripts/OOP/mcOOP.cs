using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mcOOP : MonoBehaviour
{
    public static int currentChapter = 0;
    [HideInInspector] public int wonNumber = 0;
    public GameObject chapterNumber;

    private void Start()
    {
        wonNumber = 0;
    }
}
