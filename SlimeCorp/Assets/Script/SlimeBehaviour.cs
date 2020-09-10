using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float speed = 1f;

    public bool SpawnAtLab = true;
    public Vector3 destination;
    float distTemp;
    bool OnGround = false;
    float coolDownTime = 0;
    float waitingTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnGround == true)
        {
            float distTemp = Vector3.Distance(destination, transform.position);
            anim.SetFloat("Speed", distTemp);
            anim.SetBool("OnGround", OnGround);

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(transform.position == destination)
            {
                waitingTime += Time.deltaTime;
                if(waitingTime >= coolDownTime)
                {
                    ChangeDestination();
                }
            }
        }
    }

    void ChangeDestination()
    {
        float RandomLocationX = Random.Range(0f, 13f);
        float RandomWaitingTime = Random.Range(0f, 6f);
        destination = new Vector3(RandomLocationX, transform.position.y, 0);
        coolDownTime = RandomWaitingTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        OnGround = true;
        rb.velocity = Vector2.zero;

        if(SpawnAtLab)
        {
            if (this.gameObject.transform.parent.transform.childCount >= 20)
            {
                destination = new Vector3(20f, transform.position.y);
            }
            else
            {
                destination = new Vector3(transform.position.x, transform.position.y);
            }
        }
        else
        {
            destination = new Vector3(transform.position.x, transform.position.y);
        }
        
    }
}
