using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingSystem : MonoBehaviour
{
    [Header("Stock Button")]
    public Button[] StockButton;

    [Header("Access GameObject")]
    public GameObject warningSign;
    public GameObject warningSignTextBox;

    [Header("SlimeTypeQuantity")]
    public Text[] SlimeTypeQuantity_Text;

    [Header("Have Stock")]
    public GameObject[] StockDetails_HaveStock;
    public Slider[] StockSelling_Bar;
    public Text[] StockSelling_Text;


    //[SerializeField] int[] StockSelling_Number = { 0, 0, 0, 0 }; // save
    //[SerializeField] bool[] StillHaveStock = { false, false, false, false }; // save


    [SerializeField] int[] MaxStock_Number = { 500, 1000, 1500, 2000 };
    public Text[] StockSellingPrice_Text;
    [SerializeField] int[] StockSellingPrice_Number = { 10, 15, 35, 50 };

    [Header("Out Of Stock")]
    public GameObject[] StockDetails_OutOfStock;
    [SerializeField] int[] RestockCost_SlimeTypeCount = { 30, 45, 60, 80 };
    [SerializeField] string[] SlimeType = { "Basic Slime", "Wood Slime", "Lava Slime", "Tek Slime" };
    public Text[] RestockCost_Text;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("StockPopup").SetActive(false);
        for (int i = 1; i < 4; i++)
        {
            StockButton[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckingUnlockLab();
        CheckingStockCondition();
        CheckingSlimeTypeQuantity();

        for (int i = 0; i < StockButton.Length; i++)
        {
            int closureIndex = i;
            StockButton[closureIndex].onClick.AddListener(() => Restock(closureIndex));
        }


        for (int i = 0; i < 4; i++)
        {
            StockSelling_Text[i].text = GameManagerScript.StockSelling_Number[i].ToString("n0");
            StockSellingPrice_Text[i].text = StockSellingPrice_Number[i].ToString("n0") + " " + "G";
            RestockCost_Text[i].text = RestockCost_SlimeTypeCount[i].ToString("n0") + " " + SlimeType[i];
        }
    }

    /// <summary>
    /// Checking which lab is unlocked
    /// and the related items will be unlocked also
    /// </summary>
    void CheckingUnlockLab()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.UnlockLab[i] == true)
            {
                StockButton[i].gameObject.SetActive(true);
            }
            else
            {
                StockButton[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Checking whether the stock is still available.
    /// Whether yes or no, the related details will be shown.
    /// </summary>
    public void CheckingStockCondition()
    {
        int StockThatRunOut = 0;
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.UnlockLab[i] == true)
            {
                if (GameManagerScript.StockSelling_Number[i] > 0)
                {
                    GameManagerScript.StillHaveStock[i] = true;
                    StockButton[i].image.color = new Color(1, 0.9215f, 0.6039f);
                    StockButton[i].interactable = false;
                    StockDetails_HaveStock[i].SetActive(true);
                    StockDetails_OutOfStock[i].SetActive(false);
                    //Debug.Log("HaveStock");
                }
                else
                {
                    GameManagerScript.StillHaveStock[i] = false;
                    StockButton[i].image.color = new Color(1, 0.7568f, 0.2745f);
                    StockButton[i].interactable = true;
                    StockDetails_HaveStock[i].SetActive(false);
                    StockDetails_OutOfStock[i].SetActive(true);
                    StockThatRunOut++;
                    //Debug.Log("NoStock");
                }
            }
            else
            {
                StockThatRunOut++;
            }
        }

        if(StockThatRunOut >= 4)
        {
            warningSign.SetActive(true);
        }
        else
        {
            warningSign.SetActive(false);
            warningSignTextBox.SetActive(false);
        }
    }

    /// <summary>
    /// Use this function to restock the items if it is out of stock
    /// </summary>
    public void Restock(int buttonIndex)
    {
        if(GameManagerScript.StillHaveStock[buttonIndex] == false)
        {
            if (GameManagerScript.StockSelling_Number[buttonIndex] <= 0)
            {
                if (GameManagerScript.SlimeTypeCount[buttonIndex] >= RestockCost_SlimeTypeCount[buttonIndex])
                {
                    GameManagerScript.SlimeTypeCount[buttonIndex] -= RestockCost_SlimeTypeCount[buttonIndex];
                    GameManagerScript.StockSelling_Number[buttonIndex] = MaxStock_Number[buttonIndex];
                    StockSelling_Bar[buttonIndex].maxValue = MaxStock_Number[buttonIndex];
                    StockSelling_Bar[buttonIndex].value = MaxStock_Number[buttonIndex];
                    GameManagerScript.StillHaveStock[buttonIndex] = true;
                }
            }
        }
    }

    /// <summary>
    /// Checking the Slime Type Quantity
    /// and show in the market panel
    /// </summary>
    public void CheckingSlimeTypeQuantity()
    {
        for (int i = 0; i < 4; i++)
        {
            SlimeTypeQuantity_Text[i].text = GameManagerScript.SlimeTypeCount[i].ToString("n0");
        }
    }

    /// <summary>
    /// Use this function to purchase the item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Buy(int item)
    {
        for(int i = 0; i < 4; i++)
        {
            if (StockDetails_HaveStock[item - i] == true)
            {
                if(GameManagerScript.StockSelling_Number[item - i] > 0)
                {
                    GameManagerScript.StockSelling_Number[item - i]--;
                    StockSelling_Bar[item - i].value--;
                    GameManagerScript.TotalCash += StockSellingPrice_Number[item - i];
                    i = 4;
                    return true;
                }

                if((item - i) == 0)
                {
                    i = 4;
                }
            }
        }

        return false;
    }

    public void WarningSignTextBox()
    {
        warningSignTextBox.SetActive(!warningSignTextBox.activeSelf);
    }
}
