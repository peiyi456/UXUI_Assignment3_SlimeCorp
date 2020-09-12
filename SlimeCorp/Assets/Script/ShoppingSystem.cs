using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingSystem : MonoBehaviour
{
    [Header("Stock Button")]
    [SerializeField] Button[] ItemButton;
    [SerializeField] Image[] ItemButtonImage;

    [Header("SlimeTypeQuantity")]
    [SerializeField] Text[] SlimeTypeQuantityText;

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
        CheckingUnlockLab();
        CheckingStockCondition();
        for(int i = 0; i < 4; i++)
        {
            ItemButton[i].onClick.AddListener(Restock);
        }
    }

    void CheckingUnlockLab()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.UnlockLab[i] == true)
            {
                ItemButton[i].gameObject.SetActive(true);
            }
            else
            {
                ItemButton[i].gameObject.SetActive(false);
            }
        }
    }

    public void CheckingStockCondition()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.UnlockLab[i] == true)
            {
                if (RemainStockNumber[i] > 0)
                {
                    ItemButton[i].image.color = new Color(1, 0.9215f, 0.6039f);
                    ItemButton[i].interactable = false;
                    ItemDetails_HaveStock[i].SetActive(true);
                    ItemDetails_OutOfStock[i].SetActive(false);
                    Debug.Log("HaveStock");
                }
                else
                {
                    ItemButton[i].image.color = new Color(1, 0.7568f, 0.2745f);
                    ItemButton[i].interactable = true;
                    ItemDetails_HaveStock[i].SetActive(false);
                    ItemDetails_OutOfStock[i].SetActive(true);
                    Debug.Log("NoStock");
                }
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

    public void CheckingSlimeTypeQuantity()
    {
        for(int i = 0; i < 4; i++)
        {
            SlimeTypeQuantityText[i].text = GameManagerScript.SlimeTypeCount[i].ToString();
        }
    }

    public bool Buy (int item)
    {

        if (RemainStockNumber[item] > 0)
        {
            RemainStockNumber[item]--;
            return true;
        }

        else
        {
            return false;
        }
        
    }
}
