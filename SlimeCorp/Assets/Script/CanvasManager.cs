using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainCamera, AttackSceneCamera, SlimeTVScreen, SlimeTVGameObject;
    public GameObject AttackRoom_LabInfoPanel, Market_StockPanel, BottomBar, AttackSystem;
    public GameObject InfoButton, StockButton, SlimeTVButton;
    CameraMovementScript CameraScript;

    RectTransform attackRoomRectLocation, LeftRectLocation, RightRectLocation;

    public GameObject InfoPanelButtonGroup, BlackScene;

    public float ZoomingSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CameraScript = MainCamera.GetComponent<CameraMovementScript>();
        Market_StockPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraScript.CameraLocation == 1)
        {
            SlimeTVButton.SetActive(false);
            InfoButton.SetActive(false);
            StockButton.SetActive(true);
        }
        else if (CameraScript.CameraLocation == 2)
        {
            SlimeTVButton.SetActive(true);
            InfoButton.SetActive(false);
            StockButton.SetActive(false);
        }
        else if (CameraScript.CameraLocation == 3)
        {
            SlimeTVButton.SetActive(false);
            InfoButton.SetActive(true);
            StockButton.SetActive(false);
        }
    }

    public void OpenStockPanel()
    {

        if (CameraScript.CameraLocation == 1)
        {
            Market_StockPanel.SetActive(true);
        }
    }

    public void OpenAttackSystem()
    {
        CameraScript.CameraLocation = 4;
        BottomBar.SetActive(false);
        SlimeTVScreen.SetActive(true);
        SlimeTVGameObject.GetComponent<SlimeTVScript>().ShowingTV = true;
        StartCoroutine(ZoomingToTV());
    }

    public void CloseAttackSystem()
    {
        AttackSystem.SetActive(false);
        AttackSceneCamera.GetComponent<Camera>().enabled = false;
        MainCamera.GetComponent<Camera>().enabled = true;
        SoundManager.playBGmusic();
        StartCoroutine(ZoomOutFromTV());
    }

    IEnumerator ZoomingToTV()
    {
        float tempElapseTime = 0;
        Vector3 destination = new Vector3(SlimeTVScreen.transform.position.x, SlimeTVScreen.transform.position.y, -10f);
        while (tempElapseTime <= ZoomingSpeed)
        {
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, destination, (tempElapseTime / ZoomingSpeed));
            tempElapseTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        MainCamera.transform.position = destination;

        while (MainCamera.GetComponent<Camera>().orthographicSize > 0.9)
        {
            MainCamera.GetComponent<Camera>().orthographicSize -= Time.deltaTime * 4f;
            yield return new WaitForEndOfFrame();
        }

        MainCamera.GetComponent<Camera>().enabled = false;
        AttackSceneCamera.GetComponent<Camera>().enabled = true;
        AttackSystem.SetActive(true);
        SoundManager.playAttackMusic();
    }

    IEnumerator ZoomOutFromTV()
    {
        while (MainCamera.GetComponent<Camera>().orthographicSize < 5)
        {
            MainCamera.GetComponent<Camera>().orthographicSize += Time.deltaTime * 4f;
            yield return new WaitForEndOfFrame();
        }
        MainCamera.GetComponent<Camera>().orthographicSize = 5;

        float tempElapseTime = 0;
        Vector3 destination = new Vector3(MainCamera.transform.position.x, 0, -10f);
        while (tempElapseTime <= ZoomingSpeed)
        {
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, destination, (tempElapseTime / ZoomingSpeed));
            tempElapseTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        MainCamera.transform.position = destination;

        BottomBar.SetActive(true);
        SlimeTVScreen.SetActive(false);
        SlimeTVGameObject.GetComponent<SlimeTVScript>().ShowingTV = false;
        CameraScript.CameraLocation = 2;
    }
}
