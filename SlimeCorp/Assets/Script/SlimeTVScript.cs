using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTVScript : MonoBehaviour
{
    Animator anim;

    [SerializeField] GameObject mainCamera = null;
    public float speed = 0.5f;
    int speedMultiplier = 1;
    Vector3 destination;
    public bool ShowingTV = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        destination = new Vector3(mainCamera.transform.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, destination, speedMultiplier * speed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x - destination.x) <= 0.1f)
        {
            speedMultiplier = 0;
        }
        else
        {
            speedMultiplier = 1;
        }

        anim.SetFloat("Speed", speedMultiplier);
        anim.SetBool("ShowingTV", ShowingTV);
    }
}
