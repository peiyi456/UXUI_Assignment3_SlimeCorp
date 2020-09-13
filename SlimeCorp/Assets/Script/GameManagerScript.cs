using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Internal Data")]
    public static long TotalSlimePower = 0;
    public static long TotalCash = 5000;
    private float GameTimer = 0;
    public static int[] SlimeTypeCount = { 0, 0, 0, 0 };
    public static int[] SlimeTypeForAttackRoom = { 0, 0, 0, 0 };
    public static bool[] UnlockLab = { true, true, false, false};
    public GameObject[] LockLabScreen;
    public static int[] LabLevel = { 1, 1, 1, 1};
    public static bool[] CountryUnlock = { true, false, false, false };
    public static bool[] CountryConquer = { false, false, false, false };

    [Header("Access GameObject")]
    public Text text_totalPower;
    public Text text_totalCash;
    public Slime[] slimeType;
    public CountryData[] CountryData_s;

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
            TotalSlimePower += SlimeTypeCount[i] * slimeType[i].Power;
        }
        GameTimer += Time.deltaTime;
        if(GameTimer >= 1f)
        {
            for(int i = 0; i < 3; i++)
            {
                if(CountryConquer[i] == true)
                {
                    TotalCash += CountryData_s[i].EarnPerSecond;
                }
            }
            GameTimer = 0;
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
