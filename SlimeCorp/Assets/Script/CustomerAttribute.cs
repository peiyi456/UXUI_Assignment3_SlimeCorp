using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAttribute : MonoBehaviour
{
    Animator anim;
    public GameObject ShoppingSystem_gameObject;

    public Vector3 destination;
    public float speed;
    public int preferChoice = 0;
    Vector3 InsideShopLocation = new Vector3(3f, 7.8f);
    Vector3 LeavingShopLocation = new Vector3(3.1f, 7.8f);
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
        GetComponent<SpriteRenderer>().sortingOrder = 4;
        yield return new WaitForSeconds(3.5f);

        //Buy function;
        int index = preferChoice;
        ShoppingSystem_gameObject = GameObject.Find("ShoppingSystem (1)");
        bool successBuy = ShoppingSystem_gameObject.GetComponent<ShoppingSystem>().Buy(index - 1);

        //Out of the shop
        alreadyPurchase = true;
        GetComponent<SpriteRenderer>().sortingOrder = 7;
        if(successBuy == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
        speed = 2f;
        destination = deleteColliderLocation;
    }
}
