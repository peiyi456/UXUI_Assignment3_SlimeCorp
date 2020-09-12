using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject MarketButton, FactoryButton, AttackRoomButton, MainCamera, AttackSceneCamera, SlimeTVScreen;
    public GameObject AttackRoom_LabInfoPanel, Market_StockPanel, BottomBar, AttackSystem;
    public GameObject InfoButton, StockButton;
    CameraMovementScript CameraScript;

    RectTransform attackRoomRectLocation, LeftRectLocation, RightRectLocation;

    public GameObject InfoPanelButtonGroup, BlackScene;

    public float ZoomingSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        attackRoomRectLocation = AttackRoomButton.GetComponent<RectTransform>();
        LeftRectLocation = MarketButton.GetComponent<RectTransform>();
        RightRectLocation = FactoryButton.GetComponent<RectTransform>();
        CameraScript = MainCamera.GetComponent<CameraMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraScript.CameraLocation == 1)
        {
            AttackRoomButton.SetActive(true);
            attackRoomRectLocation.anchorMin = LeftRectLocation.anchorMin;
            attackRoomRectLocation.anchorMax = LeftRectLocation.anchorMax;
            attackRoomRectLocation.anchoredPosition = LeftRectLocation.anchoredPosition;

            InfoButton.SetActive(false);
            StockButton.SetActive(true);
        }
        else if (CameraScript.CameraLocation == 2)
        {
            AttackRoomButton.SetActive(false);

            InfoButton.SetActive(true);
            StockButton.SetActive(false);
        }
        else if (CameraScript.CameraLocation == 3)
        {
            AttackRoomButton.SetActive(true);
            attackRoomRectLocation.anchorMin = RightRectLocation.anchorMin;
            attackRoomRectLocation.anchorMax = RightRectLocation.anchorMax;
            attackRoomRectLocation.anchoredPosition = RightRectLocation.anchoredPosition;

            InfoButton.SetActive(true);
            StockButton.SetActive(false);
        }
    }

    public void OpenStockPanel()
    {

        if (CameraScript.CameraLocation == 1)
        {
            InfoPanelButtonGroup.GetComponent<Animator>().SetTrigger("Close");
            BlackScene.SetActive(false);
            Market_StockPanel.SetActive(true);
        }
    }

    public void OpenAttackSystem()
    {
        CameraScript.CameraLocation = 4;
        BottomBar.SetActive(false);
        SlimeTVScreen.SetActive(true);
        StartCoroutine(ZoomingToTV());
    }

    public void CloseAttackSystem()
    {
        AttackSystem.SetActive(false);
        AttackSceneCamera.GetComponent<Camera>().enabled = false;
        MainCamera.GetComponent<Camera>().enabled = true;
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
        CameraScript.CameraLocation = 2;
    }
}
