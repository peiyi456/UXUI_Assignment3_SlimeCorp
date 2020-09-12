using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject MarketButton, FactoryButton, AttackRoomButton, MainCamera;
    public GameObject AttackRoom_LabInfoPanel, Market_StockPanel;
    CameraMovementScript CameraScript;

    RectTransform attackRoomRectLocation, LeftRectLocation, RightRectLocation;

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
        }
        else if (CameraScript.CameraLocation == 2)
        {
            AttackRoomButton.SetActive(false);
        }
        else if (CameraScript.CameraLocation == 3)
        {
            AttackRoomButton.SetActive(true);
            attackRoomRectLocation.anchorMin = RightRectLocation.anchorMin;
            attackRoomRectLocation.anchorMax = RightRectLocation.anchorMax;
            attackRoomRectLocation.anchoredPosition = RightRectLocation.anchoredPosition;
        }
    }

    public void ChangingPanelFunction()
    {
        if (CameraScript.CameraLocation == 3)
        {
            AttackRoom_LabInfoPanel.SetActive(true);
        }

        else if (CameraScript.CameraLocation == 1)
        {
            Market_StockPanel.SetActive(true);
        }
    }
}
