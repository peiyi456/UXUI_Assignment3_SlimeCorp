using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSystemScript : MonoBehaviour
{
    public GameObject[] Cloud_A;
    public GameObject[] Cloud_B;
    public float speedA = 1f;
    public float speedB = 2f;
    public float distanceBack = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            Cloud_A[i].transform.position += new Vector3(speedA * Time.deltaTime, 0f, 0f); 
        }
        for (int i = 0; i < 3; i++)
        {
            Cloud_B[i].transform.position += new Vector3(speedB * Time.deltaTime, 0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Cloud")
        {
            col.gameObject.transform.position -= new Vector3(distanceBack, 0f, 0f);
        }
    }
}
