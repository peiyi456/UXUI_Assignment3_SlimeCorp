using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int TotalSlimePower = 0;
    public static int[] SlimeTypeCount = { 0, 0, 0, 0 };
    public static bool[] UnlockLab = { true, true, true, true};
    public static int[] LabLevel = { 1, 1, 1, 1};

    public Text text_totalPower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TotalSlimePower = SlimeTypeCount[0] + SlimeTypeCount[1] + SlimeTypeCount[2] + SlimeTypeCount[3];
        text_totalPower.text = "" + TotalSlimePower;
    }
}
