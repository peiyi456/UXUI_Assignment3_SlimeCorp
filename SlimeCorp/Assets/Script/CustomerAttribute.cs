using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAttribute : MonoBehaviour
{
    Animator anim;

    public Vector3 destination;
    public float speed;
    Vector3 InsideShopLocation = new Vector3(3f, 7.8f);
    Vector3 LeavingShopLocation = new Vector3(2f, 7.8f);
    Vector3 deleteColliderLocation = new Vector3(20f, 7.8f);

    private bool alreadyPurchase = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        float distTemp = Vector3.Distance(destination, transform.position);
        anim.SetFloat("Speed", distTemp);

        if (transform.position == InsideShopLocation && alreadyPurchase == false)
        {
            speed = 0;
            destination = LeavingShopLocation;
            transform.position = LeavingShopLocation;
            StartCoroutine(Shopping());
        }

        if (transform.position == deleteColliderLocation)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Shopping()
    {
        yield return new WaitForSeconds(10f);
        //Buy function;
        alreadyPurchase = true;
        
        speed = 2f;
        destination = deleteColliderLocation;
    }
}
