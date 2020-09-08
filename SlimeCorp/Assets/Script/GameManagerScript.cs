using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //public static int BasicSlimeCount;
    //public static int WoodSlimeCount;
    //public static int LavaSlimeCount;
    //public static int TekSlimeCount;
    public static int[] SlimeTypeCount = { 0, 0, 0, 0 };

    public int TotalSlimePower = 0;

    //public static bool unlockL1;
    //public static bool unlockL2;
    //public static bool unlockL3;
    //public static bool unlockL4;
    public static bool[] UnlockLevel = { true, false, false, false};

    // Start is called before the first frame update
    void Start()
    {
        //BasicSlimeCount = 0;
        //WoodSlimeCount = 0;
        //LavaSlimeCount = 0;
        //TekSlimeCount = 0;

        //unlockL1 = true;
        //unlockL2 = false;
        //unlockL3 = false;
        //unlockL4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        TotalSlimePower = SlimeTypeCount[0] + SlimeTypeCount[1] + SlimeTypeCount[2] + SlimeTypeCount[3];
        Debug.Log(TotalSlimePower);
    }
}
