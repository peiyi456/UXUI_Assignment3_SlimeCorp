using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ChangeScene : MonoBehaviour
{
    public AudioSource ChangeSceneSoundSource;
    public AudioSource BackgroundMusic;
    public AudioClip ChangeSceneSound;
    public PlayableDirector timeline;

    bool startLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && startLoading == false)
        {
            startLoading = true;
            timeline.Play();
            StartCoroutine(LoadScene());
            ChangeSceneSoundSource.PlayOneShot(ChangeSceneSound);
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f && timeline.state == PlayState.Paused)
            {
                Debug.Log("YES");
                BackgroundMusic.Stop();
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
