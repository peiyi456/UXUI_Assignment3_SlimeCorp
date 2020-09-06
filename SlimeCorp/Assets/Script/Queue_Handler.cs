using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue_Handler : MonoBehaviour
{
    List<GameObject> guestList = new List<GameObject>();
    private List<Vector3> positionList = new List<Vector3>();
    Vector3 firstPosition = new Vector3(1f, 7.8f);
    float positionSize = .5f;

    public GameObject CustomerGameObject;
    public Customer[] customerType;
    Vector3 SpawnedLocation = new Vector3(-7f, 7.8f);
    Vector3 InsideShop = new Vector3(3f, 7.8f);

    bool generatingCustomer = false;
    float coolDownQueue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            positionList.Add(firstPosition + new Vector3(-1, 0) * positionSize * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coolDownQueue += Time.deltaTime;

        if (guestList.Count != 0 && guestList[0].gameObject.transform.position == positionList[0])
        {
            if (coolDownQueue >= 10f)
            {
                RemovingGuest();
            }
        }

        if (guestList.Count < 3)
        {
            if (generatingCustomer == false)
            {
                generatingCustomer = true;
                StartCoroutine(AddGuest());
            }
        }
    }

    IEnumerator AddGuest()
    {
        yield return new WaitForSeconds(1f);
        GameObject spawnedCustomer = Instantiate(CustomerGameObject, SpawnedLocation, Quaternion.identity) as GameObject;
        guestList.Add(spawnedCustomer);
        DecideCustomerType(spawnedCustomer);
        spawnedCustomer.GetComponent<CustomerAttribute>().destination = positionList[guestList.IndexOf(spawnedCustomer)];
        spawnedCustomer.GetComponent<CustomerAttribute>().speed = 1f;
        generatingCustomer = false;
    }

    void DecideCustomerType(GameObject customer)
    {
        int RandomNum = Random.Range(0, 2);
        Debug.Log(RandomNum);
        Customer selectedCustomer = customerType[RandomNum];

        customer.name = selectedCustomer.name;
        customer.GetComponent<SpriteRenderer>().sprite = selectedCustomer.image;
        customer.GetComponent<Animator>().runtimeAnimatorController = selectedCustomer.animatorController;
    }

    void RemovingGuest()
    {
        if (guestList.Count == 0)
        {
            return;
        }
        else
        {
            guestList[0].GetComponent<CustomerAttribute>().destination = InsideShop;
            guestList[0].GetComponent<CustomerAttribute>().speed = 1f;
            guestList.RemoveAt(0);
            RelocateAllGuest();
            coolDownQueue = 0f;
        }
    }

    void RelocateAllGuest()
    {
        for (int i = 0; i < guestList.Count; i++)
        {
            guestList[i].GetComponent<CustomerAttribute>().destination = positionList[i];
        }
    }
}
