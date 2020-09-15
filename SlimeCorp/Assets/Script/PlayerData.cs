using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public long TotalSlimePower;
    public long TotalCash;
    public int[] SlimeTypeCount;
    public int[] SlimeTypeForAttackRoom;
    public bool[] UnlockLab;
    public int[] LabLevel;
    public bool[] CountryUnlock;
    public bool[] CountryConquer;
    public int[] StockSelling_Number;
    public bool[] StillHaveStock;

    
    public PlayerData() 
    {
        TotalSlimePower = GameManagerScript.TotalSlimePower;
        TotalCash = GameManagerScript.TotalCash;

        SlimeTypeCount = GameManagerScript.SlimeTypeCount;
        //SlimeTypeForAttackRoom = GameManagerScript.SlimeTypeForAttackRoom;
        UnlockLab = GameManagerScript.UnlockLab;
        LabLevel = GameManagerScript.LabLevel;
        CountryUnlock = GameManagerScript.CountryUnlock;
        CountryConquer = GameManagerScript.CountryConquer;
        StockSelling_Number = GameManagerScript.StockSelling_Number;
        StillHaveStock = GameManagerScript.StillHaveStock;
}
}
