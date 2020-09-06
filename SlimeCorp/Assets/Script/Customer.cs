using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customers")]
public class Customer : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int preferChoice;
    public RuntimeAnimatorController animatorController;

    public void MoveTo(Vector3 position, float speed)
    {
        //transform.position = Vector2.MoveTowards(transform.position, position, speed);
    }
}
