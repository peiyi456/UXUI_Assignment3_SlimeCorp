using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSlime : MonoBehaviour
{
    float startCountdown;
    float DelayedTime = 2.0f;
    public GameObject BasicSlime;

    // Start is called before the first frame update
    void Start()
    {
        startCountdown = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 theScale = transform.localScale;
        if (Time.time >= startCountdown + DelayedTime)
        {
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
