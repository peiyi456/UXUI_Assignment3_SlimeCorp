using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingSystem : MonoBehaviour
{
    [Header("Stock Button")]
    [SerializeField] Button[] ItemButton;
    [SerializeField] Image[] ItemButtonImage;

    [Header("Locked Lab Region")]
    [SerializeField] GameObject[] LockLabScreen;
    public bool[] LabUnlocked = { true, false, false, false };

    [Header("Have Stock")]
    [SerializeField] GameObject[] ItemDetails_HaveStock;
    [SerializeField] Slider[] ItemSellingBar;
    [SerializeField] Text[] RemainStockText;
    public int[] RemainStockNumber = { 0, 1500, 2000, 2500};
    int[] MaxStockNumber = { 1000, 1500, 2000, 2500 };
    [SerializeField] Text[] ItemSellingPriceText;
    [SerializeField] int[] ItemSellingPrice = { 10, 15, 35, 50 };

    [Header("Out Of Stock")]
    [SerializeField] GameObject[] ItemDetails_OutOfStock;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < 4; i++)
        {
            ItemButton[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckingLockedRegion();
        CheckingStockCondition();
        for(int i = 0; i < 4; i++)
        {
            ItemButton[i].onClick.AddListener(Restock);
        }
    }

    void CheckingLockedRegion()
    {
        for (int i = 0; i < 3; i++)
        {
            if (LockLabScreen[i] == false)
            {
                ItemButton[i+1].gameObject.SetActive(true);
                LabUnlocked[i + 1] = true;
            }
            else
            {
                ItemButton[i+1].gameObject.SetActive(false);
                LabUnlocked[i + 1] = false;
            }
        }
    }

    public void Restock()
    {
        for (int i = 0; i < 4; i++)
        {
            if(RemainStockNumber[i] <= 0)
            {
                RemainStockNumber[i] = MaxStockNumber[i];
            }
        }
    }

    public void CheckingStockCondition()
    {
        for (int i = 0; i < 4; i++)
        {
            if (LabUnlocked[i] == true)
            {
                if (RemainStockNumber[i] > 0)
                {
                    ItemButton[i].image.color = new Color(1, 0.9215f, 0.6039f);
                    //ItemButtonImage[i].color = new Color(255f, 193f, 70f, 255f);
                    ItemButton[i].interactable = false;
                    ItemDetails_HaveStock[i].SetActive(true);
                    ItemDetails_OutOfStock[i].SetActive(false);
                    Debug.Log("HaveStock");
                }
                else
                {
                    ItemButton[i].image.color = new Color(1, 0.7568f, 0.2745f);
                    //ItemButtonImage[i].color = new Color(255f, 235f, 154f, 255f);
                    ItemButton[i].interactable = true;
                    ItemDetails_HaveStock[i].SetActive(false);
                    ItemDetails_OutOfStock[i].SetActive(true);
                    Debug.Log("NoStock");
                }
            }
        }
    }

    public bool Buy (int item)
    {

        if(item > 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
