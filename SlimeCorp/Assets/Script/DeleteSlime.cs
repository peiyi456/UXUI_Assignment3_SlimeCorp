using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSlime : MonoBehaviour
{
    GameObject[] BasicSlime_arr, WoodSlime_arr, LavaSlime_arr, TekSlime_arr;

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("CheckSlimeAmount", 1, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }

    void CheckSlimeAmount()
    {
        BasicSlime_arr = GameObject.FindGameObjectsWithTag("BasicSlime");
        if (BasicSlime_arr.Length >= 20)
        {
            int num = Random.Range(0, BasicSlime_arr.Length);
            BasicSlime_arr[num].GetComponent<SlimeBehaviour>().destination = new Vector3(20f, BasicSlime_arr[num].transform.position.y);
        }
    }
}
