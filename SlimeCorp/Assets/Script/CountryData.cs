using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CountryData", menuName = "CountryData")]
public class CountryData : ScriptableObject
{
    [Header("Country Data")]

    [Header("Accessing GameObject")]
    public Sprite[] LoadingScreenImage;
}
