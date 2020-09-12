using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackingText", menuName = "AttackingText")]
public class AttackingText : ScriptableObject
{
    [Header("Attack Text Data")]
    public string[] Text;
}
