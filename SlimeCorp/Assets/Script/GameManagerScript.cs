﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Internal Data")]
    public long TotalSlimePower = 0;
    public static long TotalCash = 0;
    public static int[] SlimeTypeCount = { 0, 0, 0, 0 };
    public static bool[] UnlockLab = { true, false, false, false};
    public GameObject[] LockLabScreen;
    public static int[] LabLevel = { 1, 1, 1, 1};

    [Header("Access GameObject")]
    public Text text_totalPower;
    public Text text_totalCash;
    public Slime[] slimeType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TotalSlimePower = 0;
        for(int i = 0; i < 4; i++)
        {
            TotalSlimePower += SlimeTypeCount[i] * slimeType[i].Power * LabLevel[i];
        }
        text_totalPower.text = "" + TotalSlimePower;
        text_totalCash.text = "" + TotalCash;

        for(int i = 0; i < 3; i++)
        {
            if(UnlockLab[i+1] == false)
            {
                LockLabScreen[i].SetActive(true);
            }
            else
            {
                LockLabScreen[i].SetActive(false);
            }
        }
    }
}
