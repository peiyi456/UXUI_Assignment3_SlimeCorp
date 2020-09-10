﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUpScript : MonoBehaviour
{
    [Header("GameOject Access")]
    [SerializeField] GameObject BlackPanel_buttongroup = null;
    [SerializeField] GameObject ButtonGroup = null;
    [SerializeField] GameObject WholeInfoPanel = null;
    [SerializeField] GameObject PanelGroup = null;
    [SerializeField] GameObject BlackScreen = null;
    [SerializeField] Button leftButton = null, rightButton = null;
    [SerializeField] Camera mainCamera = null;
    public GameObject[] LockPanel;

    [Header("Internal Data")]
    public Vector3 RayPosition = new Vector3(0.5f, 0.55f);
    Ray cameraRay;
    Vector2 positionRay;
    bool ButtonGroup_isAble = false;
    int currentPanelPosition = 0;
    Animator InfoPanelanim, ButtonGroupAnim;

    [Header("Panel Behaviour")]
    public float MovePageTime = .5f;
    Vector2[] labPanelPosition = { new Vector2(0f, 0f), new Vector2(-2000f, 0f), new Vector2(-4000f, 0f), new Vector2(-6000f, 0f) };

    [Header("Panel Data")]
    public Slime[] slimeType;
    public Text[] Name;
    public Text[] Power;
    public Text[] LabLevel;
    public Text[] MaxStorage;
    public Text[] UpgradeCost;
    public Text[] Amount;

    // Start is called before the first frame update
    void Start()
    {
        InfoPanelanim = WholeInfoPanel.GetComponent<Animator>();
        ButtonGroupAnim = ButtonGroup.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraRay = mainCamera.ViewportPointToRay(RayPosition);
        positionRay = cameraRay.origin;

        for(int i = 0; i < 4; i++)
        {
            Name[i].text = "" + slimeType[i].name;
            Power[i].text = "" + slimeType[i].Power;
            LabLevel[i].text = "" + GameManagerScript.LabLevel[i];
            MaxStorage[i].text = "" + slimeType[i].MaxStorage[GameManagerScript.LabLevel[i] - 1];
            if(GameManagerScript.LabLevel[i] == 3)
            {
                UpgradeCost[i].text = "MAX LEVEL";
            }
            else
            {
                UpgradeCost[i].text = "" + slimeType[i].UpgradeCost[GameManagerScript.LabLevel[i] - 1];
            }
            Amount[i].text = "" + GameManagerScript.SlimeTypeCount[i];
        }

        if(currentPanelPosition == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }

        if(currentPanelPosition == 3)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
    }

    public void OpenSlimeChoice()
    {
        if(ButtonGroup_isAble == false)
        {
            ButtonGroup_isAble = true;
            BlackPanel_buttongroup.SetActive(true);
            ButtonGroupAnim.SetTrigger("Open");
        }
        else
        {
            ButtonGroup_isAble = false;
            BlackPanel_buttongroup.SetActive(false);
            ButtonGroupAnim.SetTrigger("Close");
        }
    }

    public void OpenInfoPanel(int index)
    {
        WholeInfoPanel.SetActive(true);
        currentPanelPosition = 0;
        currentPanelPosition = index;
        PanelGroup.GetComponent<RectTransform>().anchoredPosition = labPanelPosition[index];
        InfoPanelanim.SetTrigger("Open");
        BlackScreen.SetActive(true);

        for(int i = 0; i < 4; i++)
        {
            if(GameManagerScript.UnlockLab[i] == false)
            {
                LockPanel[i].SetActive(true);
            }
            else
            {
                LockPanel[i].SetActive(false);
            }
        }
    }
    
    public void CloseInfoPanel()
    {
        InfoPanelanim.SetTrigger("Close");
        BlackScreen.SetActive(false);
        StartCoroutine(DiasblePanel());
    }

    IEnumerator DiasblePanel()
    {
        yield return new WaitForSeconds(0.25f);
        WholeInfoPanel.SetActive(false);
    }

    public void NextPanel()
    {
        currentPanelPosition++;
        Vector2 currentPosition = PanelGroup.GetComponent<RectTransform>().anchoredPosition;
        Vector2 nextPosition = labPanelPosition[currentPanelPosition];
        StartCoroutine(MovePanel(nextPosition, MovePageTime));
    }

    public void PreviousPanel()
    {
        currentPanelPosition--;
        Vector2 currentPosition = PanelGroup.GetComponent<RectTransform>().anchoredPosition;
        Vector2 nextPosition = labPanelPosition[currentPanelPosition];
        StartCoroutine(MovePanel(nextPosition, MovePageTime));
    }

    IEnumerator MovePanel(Vector2 nextPos, float time)
    {
        Vector2 currentPos = PanelGroup.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0;
        while(elapsedTime <= time)
        {
            PanelGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentPos, nextPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PanelGroup.GetComponent<RectTransform>().anchoredPosition = nextPos;
    }

    int CheckPosition()
    {
        if(positionRay.y > -8.25f)
        {
            return 0;
        }
        else if(positionRay.y > -11.565f && positionRay.y <= -8.25f)
        {
            return 1;
        }
        else if(positionRay.y > -14.875f && positionRay.y <= -11.565f)
        {
            return 2;
        }
        else if(positionRay.y <= -14.875f)
        {
            return 3;
        }

        return 0;
    }


}