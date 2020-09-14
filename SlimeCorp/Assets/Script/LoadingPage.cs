using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPage : MonoBehaviour
{
    private Slider slider;
    public Text progressPercentage;
    private float percentageNumber;
    //public ParticleSystem particleSys;

    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        //particleSys = GameObject.Find("ProgressBarParticle").GetComponent<ParticleSystem>();

    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
            percentageNumber = slider.value * 100.0f;
        }
        progressPercentage.text = "Loading...  " + percentageNumber.ToString("F0") + "%";

        if(percentageNumber >= 100)
        {
            StartCoroutine(LoadScene());
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
