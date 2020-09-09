using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSlime : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("CheckSlimeAmount", 1, 2);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }

    void CheckSlimeAmount()
    {
        GameObject[][] TempArray;
        TempArray = new GameObject[4][];
        TempArray[0] = GameObject.FindGameObjectsWithTag("BasicSlime");
        TempArray[1] = GameObject.FindGameObjectsWithTag("WoodSlime");
        TempArray[2] = GameObject.FindGameObjectsWithTag("LavaSlime");
        TempArray[3] = GameObject.FindGameObjectsWithTag("TekSlime");

        for (int i = 0; i < 4; i++)
        {
            if (TempArray[i].Length >= 20)
            {
                int num = Random.Range(0, TempArray[i].Length);
                TempArray[i][num].GetComponent<SlimeBehaviour>().destination = new Vector3(20f, TempArray[i][num].transform.position.y);
            }
        }
    }
}
