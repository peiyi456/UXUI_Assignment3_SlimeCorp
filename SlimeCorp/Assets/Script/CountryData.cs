using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CountryData", menuName = "CountryData")]
public class CountryData : ScriptableObject
{
    [Header("Country Data")]
    public long CountryPower;
    public long[] PowerNeeded;
    public long EarnPerSecond;

    [Header("Accessing GameObject")]
    public Sprite[] LoadingScreenImage;

}
