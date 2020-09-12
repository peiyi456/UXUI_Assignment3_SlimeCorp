using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] GameObject FlagGroup = null;
    [SerializeField] GameObject ButtonGroup = null;
    public GameObject[] AttackButton_gameObject;
    [SerializeField] Button LeftButton = null;
    [SerializeField] Button RightButton = null;
    public Button[] AttackButton;
    public GameObject[] ConqueredImage;
    public GameObject[] LockCountry;
    public GameObject[] CountryFlag;
    public Text[] CountryPowerText;
    [SerializeField] GameObject WarningPanel = null;
    [SerializeField] GameObject DuringAttack_gameObject = null;
    [SerializeField] GameObject LoadingImageGroup = null;
    public GameObject[] LoadingImage;
    [SerializeField] Slider LoadingBar = null;
    [SerializeField] Text LoadingBarText = null;
    public Sprite[] LoadingSlimeImage;
    public GameObject[] LoadingTextGroup;
    public Text[] LoadingSlimeText;
    public CountryData[] CountryData_s;
    public AttackingText[] AttackingTexts;
    public GameObject[] Result_image;
    [SerializeField] GameObject ContinueButton = null;

    int currentFlagGroup = 0;
    public float loadingBarValue = 0;

    public float MovePageTime = .5f;
    Vector2[] FlagGroupPosition = { new Vector2(0f, 0f), new Vector2(-2000f, 0f), new Vector2(-4000f, 0f), new Vector2(-6000f, 0f) };

    // Start is called before the first frame update
    void Start()
    {
        currentFlagGroup = 0;
        loadingBarValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update country power text
        for(int i = 0; i < 3; i++)
        {
            CountryPowerText[i].text = "" + CountryData_s[i].CountryPower;
        }
        

        //Left & Right button interaction
        if (currentFlagGroup == 0)
        {
            LeftButton.interactable = false;
        }
        else
        {
            LeftButton.interactable = true;
        }

        if (currentFlagGroup == 3)
        {
            RightButton.interactable = false;
        }
        else
        {
            RightButton.interactable = true;
        }

        //Attack button interaction
        for (int i = 0; i < 4; i++)
        {
            if(GameManagerScript.CountryUnlock[i] == false)
            {
                if(currentFlagGroup == i)
                {
                    AttackButton[i].interactable = false;
                }
            }
            else
            {
                if (currentFlagGroup == i)
                {
                    AttackButton[i].interactable = true;
                }
            }
        }

        //Lock country setActive
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.CountryUnlock[i] == false)
            {
                LockCountry[i].SetActive(true);
                CountryFlag[i].SetActive(false);
            }
            else
            {
                LockCountry[i].SetActive(false);
                CountryFlag[i].SetActive(true);
            }
        }

        for(int i = 0; i < 4; i++)
        {
            if (GameManagerScript.CountryConquer[i] == true)
            {
                AttackButton_gameObject[i].SetActive(false);
                ConqueredImage[i].SetActive(true);
            }
            else
            {
                AttackButton_gameObject[i].SetActive(true);
                ConqueredImage[i].SetActive(false);
            }
        }

        

        //Loading Bar value update
        LoadingBarText.text = (int)(LoadingBar.value * 100) + "%";
    }

    public void NextFlagGroup()
    {
        Vector2 currentPosition = FlagGroupPosition[currentFlagGroup];
        Vector2 destination = currentPosition + new Vector2(-2000f, 0f);
        StartCoroutine(MoveFlagGroup(destination, MovePageTime));
        currentFlagGroup++;
    }

    public void PreviousFlagGroup()
    {
        Vector2 currentPosition = FlagGroupPosition[currentFlagGroup];
        Vector2 destination = currentPosition + new Vector2(2000f, 0f);
        StartCoroutine(MoveFlagGroup(destination, MovePageTime));
        currentFlagGroup--;
    }

    IEnumerator MoveFlagGroup(Vector3 destination, float time)
    {
        Vector2 currentPos = FlagGroup.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0;
        while (elapsedTime <= time)
        {
            FlagGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentPos, destination, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        FlagGroup.GetComponent<RectTransform>().anchoredPosition = destination;
    }

    public void ExitAttackSystem()
    {

    }

    public void AttackBeforeWarning()
    {
        WarningPanel.SetActive(true);
    }

    public void ConfirmAttack()
    {
        FlagGroup.SetActive(false);
        ButtonGroup.SetActive(false);
        WarningPanel.SetActive(false);
        DuringAttack_gameObject.SetActive(true);

        bool WinBattle = false;
        long currentSlimePower = GameManagerScript.TotalSlimePower;
        for(int i = 0; i < 4; i++)
        {
            GameManagerScript.SlimeTypeCount[i] = 0;
        }

        //Calculate result
        if (currentSlimePower > CountryData_s[currentFlagGroup].PowerNeeded[0])
        {
            WinBattle = true;
            GameManagerScript.CountryUnlock[currentFlagGroup + 1] = true;
        }

        //Special Win Condition
        if(currentSlimePower >= CountryData_s[currentFlagGroup].PowerNeeded[0])
        {
            for (int i = 0; i < 5; i++)
            {
                LoadingSlimeText[i].text = "" + AttackingTexts[0].Text[i];
            }
        }
        else if(currentSlimePower >= CountryData_s[currentFlagGroup].PowerNeeded[1])
        {
            for (int i = 0; i < 5; i++)
            {
                LoadingSlimeText[i].text = "" + AttackingTexts[1].Text[i];
            }
        }
        else if(currentSlimePower >= CountryData_s[currentFlagGroup].PowerNeeded[2])
        {
            for (int i = 0; i < 5; i++)
            {
                LoadingSlimeText[i].text = "" + AttackingTexts[2].Text[i];
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                LoadingSlimeText[i].text = "" + AttackingTexts[3].Text[i];
            }
        }

        //Country Loading Image
        for (int i = 0; i < 3; i++)
        {
            LoadingImage[i].GetComponent<Image>().sprite = CountryData_s[currentFlagGroup].LoadingScreenImage[i];
        }
        
        LoadingBar.value = 0; //reset bar value

        //Set random slime pic
        int[] indexArray = new int[5];
        int SlimeUnlock = 0;
        for (int i = 0; i < 4; i++)
        {
            if(GameManagerScript.UnlockLab[i] == true)
            {
                SlimeUnlock++;
            }
        }
        for(int i = 0; i < 5; i++)
        {
            indexArray[i] = Random.Range(0, SlimeUnlock);
        }
        //Start attacking
        StartCoroutine(AttackProcess(indexArray, WinBattle));
    }

    public void RejectAttack()
    {
        WarningPanel.SetActive(false);
    }

    IEnumerator AttackProcess(int[] indexArray, bool WinBattle)
    {
        //Text 1 < 10%
        LoadingTextGroup[0].GetComponent<Image>().sprite = LoadingSlimeImage[indexArray[0]];
        while(LoadingBar.value < 0.1)
        {
            LoadingBar.value += Time.deltaTime * 0.01f;
            yield return new WaitForEndOfFrame();
        }

        LoadingTextGroup[0].SetActive(false);
        LoadingTextGroup[1].SetActive(true);

        //Text 2 < 30%
        LoadingTextGroup[1].GetComponent<Image>().sprite = LoadingSlimeImage[indexArray[1]];
        while (LoadingBar.value < 0.4)
        {
            LoadingBar.value += Time.deltaTime * 0.03f;
            yield return new WaitForEndOfFrame();
        }

        LoadingTextGroup[1].SetActive(false);
        LoadingTextGroup[2].SetActive(true);

        //Text 3 < 70%
        LoadingTextGroup[2].GetComponent<Image>().sprite = LoadingSlimeImage[indexArray[2]];
        while (LoadingBar.value < 0.8)
        {
            LoadingBar.value += Time.deltaTime * 0.04f;
            yield return new WaitForEndOfFrame();
        }

        LoadingTextGroup[2].SetActive(false);
        LoadingTextGroup[3].SetActive(true);

        //Text 4 < 100%
        LoadingTextGroup[3].GetComponent<Image>().sprite = LoadingSlimeImage[indexArray[3]];
        while (LoadingBar.value < 0.99f)
        {
            LoadingBar.value += Time.deltaTime * 0.015f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(3f);

        //Text 5 = 100%
        LoadingBar.value = 1;
        LoadingTextGroup[3].SetActive(false);
        LoadingTextGroup[4].SetActive(true);
        LoadingTextGroup[4].GetComponent<Image>().sprite = LoadingSlimeImage[indexArray[4]];
        LoadingImageGroup.SetActive(false);
        ContinueButton.SetActive(true);

        if(WinBattle)
        {
            Result_image[0].SetActive(true);
            GameManagerScript.CountryConquer[currentFlagGroup] = true;
        }
        else
        {
            Result_image[1].SetActive(true);
        }
    }

    public void AfterAttacking()
    {
        ContinueButton.SetActive(false);
        for(int i = 0; i < 2; i++)
        {
            if(Result_image[i].activeInHierarchy)
            {
                Result_image[i].SetActive(false);
            }
        }
        LoadingImageGroup.SetActive(true);
        LoadingTextGroup[4].SetActive(false);
        LoadingTextGroup[0].SetActive(true);
        FlagGroup.SetActive(true);
        ButtonGroup.SetActive(true);
        DuringAttack_gameObject.SetActive(false);
    }
}
