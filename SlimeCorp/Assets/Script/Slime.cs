using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime", menuName = "Slimes")]
public class Slime : ScriptableObject
{
    [Header("Unity Attribute")]
    public RuntimeAnimatorController animatorController;
    public string tag;

    [Header("Slime Data")]
    public new string name;
    public int Power;
    public int[] MaxStorage;
    public int[] UpgradeCost;
}
