using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditPageManager : MonoBehaviour
{
    public void ExitCreditScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
    }
}
