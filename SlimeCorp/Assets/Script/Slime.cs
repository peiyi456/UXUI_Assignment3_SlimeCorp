using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime", menuName = "Slimes")]
public class Slime : ScriptableObject
{
    public new string name;
    public Sprite image;
    public RuntimeAnimatorController animatorController;
    public string tag;
}
