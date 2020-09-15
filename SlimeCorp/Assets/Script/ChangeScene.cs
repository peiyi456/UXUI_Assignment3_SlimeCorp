using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ChangeScene : MonoBehaviour
{
    AsyncOperation operation;
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
        
    }

    void OnMouseDown()
    {
        if (startLoading == false)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                startLoading = true;
                timeline.Play();
                StartCoroutine(LoadScene());
                ChangeSceneSoundSource.PlayOneShot(ChangeSceneSound);
            }
            

            //if (!EventSystem.current.IsPointerOverGameObject() || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //{

            //}
        }
    }

    IEnumerator LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(1);
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

    public void OpenCreditScene()
    {
        operation = SceneManager.LoadSceneAsync(3);
    }
}
