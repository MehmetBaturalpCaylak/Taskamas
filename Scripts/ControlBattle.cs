using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBattle : MonoBehaviour
{
    GameController gC;
    AIBattle aB;

    public bool mcAction;
    [HideInInspector]public int mcSelectionID;

    public SpriteRenderer zTusu;
    public SpriteRenderer xTusu;
    public SpriteRenderer cTusu;
    public SpriteRenderer tas;
    public SpriteRenderer kagýt;
    public SpriteRenderer makas;

    public bool kingManupTas;
    public bool kingManupKagt;
    public bool kingManupMaks;

    public void Awake()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        aB = gameObject.GetComponent<AIBattle>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mcSelectionID = 0;
        kingManupTas = false;
        kingManupKagt = false;
        kingManupMaks = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gC.gamePaused)
        {
            return;
        }
        if (mcAction)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !kingManupTas)
            {
                zTusu.color = new Color(zTusu.color.r, zTusu.color.g, zTusu.color.b, 1f);
                tas.color = new Color(tas.color.r, tas.color.g, tas.color.b, 1f);
                xTusu.color = new Color(xTusu.color.r, xTusu.color.g, xTusu.color.b, 0.7f);
                kagýt.color = new Color(kagýt.color.r, kagýt.color.g, kagýt.color.b, 0.7f);
                cTusu.color = new Color(cTusu.color.r, cTusu.color.g, cTusu.color.b, 0.7f);
                makas.color = new Color(makas.color.r, makas.color.g, makas.color.b, 0.7f);

                mcSelectionID = 1;
            }
            else if (Input.GetKeyDown(KeyCode.X) && !kingManupKagt)
            {
                zTusu.color = new Color(zTusu.color.r, zTusu.color.g, zTusu.color.b, 0.7f);
                tas.color = new Color(tas.color.r, tas.color.g, tas.color.b, 0.7f);
                xTusu.color = new Color(xTusu.color.r, xTusu.color.g, xTusu.color.b, 1f);
                kagýt.color = new Color(kagýt.color.r, kagýt.color.g, kagýt.color.b, 1f);
                cTusu.color = new Color(cTusu.color.r, cTusu.color.g, cTusu.color.b, 0.7f);
                makas.color = new Color(makas.color.r, makas.color.g, makas.color.b, 0.7f);
                
                mcSelectionID = 2;
            }
            else if (Input.GetKeyDown(KeyCode.C) && !kingManupMaks)
            {
                zTusu.color = new Color(zTusu.color.r, zTusu.color.g, zTusu.color.b, 0.7f);
                tas.color = new Color(tas.color.r, tas.color.g, tas.color.b, 0.7f);
                xTusu.color = new Color(xTusu.color.r, xTusu.color.g, xTusu.color.b, 0.7f);
                kagýt.color = new Color(kagýt.color.r, kagýt.color.g, kagýt.color.b, 0.7f);
                cTusu.color = new Color(cTusu.color.r, cTusu.color.g, cTusu.color.b, 1f);
                makas.color = new Color(makas.color.r, makas.color.g, makas.color.b, 1f);
                
                mcSelectionID = 3;
            }
        }
        else
        {
            zTusu.color = new Color(zTusu.color.r, zTusu.color.g, zTusu.color.b, 1f);
            tas.color = new Color(tas.color.r, tas.color.g, tas.color.b, 1f);
            xTusu.color = new Color(xTusu.color.r, xTusu.color.g, xTusu.color.b, 1f);
            kagýt.color = new Color(kagýt.color.r, kagýt.color.g, kagýt.color.b, 1f);
            cTusu.color = new Color(cTusu.color.r, cTusu.color.g, cTusu.color.b, 1f);
            makas.color = new Color(makas.color.r, makas.color.g, makas.color.b, 1f);
            mcSelectionID = 0;
        }
        if (kingManupTas)
        {
            zTusu.gameObject.SetActive(false);
        }
        else
        {
            zTusu.gameObject.SetActive(true);
        }
        if (kingManupKagt)
        {
            xTusu.gameObject.SetActive(false);
        }
        else
        {
            xTusu.gameObject.SetActive(true);
        }
        if (kingManupMaks)
        {
            cTusu.gameObject.SetActive(false);
        }
        else
        {
            cTusu.gameObject.SetActive(true);
        }
    }
}
