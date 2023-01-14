using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUi : MonoBehaviour
{
    public GameObject shopPanel, shopPanelSell;
    //buy:
    public Button shopCloseButton, shopSellCloseButton;
    public Button shopSellPanelButton, shopBuyPanelButton;
    public Button ammoButton1, ammoButton2, gasButton;
    public Button repair1Button, repairAllButton;
    public Button buyRoom1, buyRoom2;
    public Button buyModule1, buyModule2;
    public Button buyMember;
    //sell:
    public Button ammoButton1Sell, ammoButton2Sell, gasButtonSell;
    public Button sellRoom1, sellRoom2;
    public Button sellModule1, sellModule2;

    public TextMeshProUGUI repairAllTMPText;
    public Text creditsShop;
    public Text creditsShopSell;

    public Transform posCrew1;
    public Transform posCrew2;
    public Transform posCrew3;
    public Transform posCrew4;
    private Dialogue dialogue;

    private int repairCost = 20; //the amount that a single repair costs
    private int repairAmount = 5; //the amount that a single repair repairs the airship
    private int repairAllAmountCost = 0;
    private int repairAllAmount = 0;
    private int ammo1Cost = 20, ammo2Cost = 20, gasCost = 30;

    public GameObject crewmemberred, crewmemberblue;


    // Start is called before the first frame update
    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
        InitializeButtons();
        UpdateRepairAllText();
    }

    // Update is called once per frame
    void Update()
    {
        creditsShop.text = "CREDITS: " + AirshipStats.credits;
        creditsShopSell.text = creditsShop.text;

        if (AirshipStats.airshipCurrentHealth < AirshipStats.airshipMaxHealth)
            UpdateRepairAllText();
    }

    private void UpdateRepairAllText()
    {
        repairAllAmountCost = (int)((AirshipStats.airshipMaxHealth - AirshipStats.airshipCurrentHealth) / repairAmount * repairCost);
        repairAllAmount = (int)((AirshipStats.airshipMaxHealth - AirshipStats.airshipCurrentHealth) / repairAmount);
        if (AirshipStats.credits < repairAllAmountCost) //not enough money to fully repair
        {
            repairAllAmount = AirshipStats.credits / repairCost;
            repairAllAmountCost = repairAllAmount * repairCost;
        }
        repairAllTMPText.text = "Repair all	       " + repairAllAmountCost;
    }

    private void InitializeButtons()
    {
        shopCloseButton.GetComponent<Button>().onClick.AddListener(() => CloseShopClicked());
        shopSellCloseButton.GetComponent<Button>().onClick.AddListener(() => CloseShopClicked());
        shopSellPanelButton.GetComponent<Button>().onClick.AddListener(() => ShopNextPageClicked(1));
        shopBuyPanelButton.GetComponent<Button>().onClick.AddListener(() => ShopNextPageClicked(2));

        //buy ammo
        ammoButton1.GetComponent<Button>().onClick.AddListener(() => BuyAmmoClicked(1));
        ammoButton2.GetComponent<Button>().onClick.AddListener(() => BuyAmmoClicked(2));
        gasButton.GetComponent<Button>().onClick.AddListener(() => BuyAmmoClicked(0));

        //buy repairs
        repair1Button.GetComponent<Button>().onClick.AddListener(() => BuyRepairsClicked(1));
        repairAllButton.GetComponent<Button>().onClick.AddListener(() => BuyRepairsClicked(2));

        //buy rooms
        buyRoom1.GetComponent<Button>().onClick.AddListener(() => BuyRoomsClicked(1));
        buyRoom2.GetComponent<Button>().onClick.AddListener(() => BuyRoomsClicked(2));

        //buy modules
        buyModule1.GetComponent<Button>().onClick.AddListener(() => BuyModulesClicked(1));
        buyModule2.GetComponent<Button>().onClick.AddListener(() => BuyModulesClicked(2));

        //buy CrewMember
        buyMember.GetComponent<Button>().onClick.AddListener(() => BuyCrewMemberClicked());

        //sell ammo
        ammoButton1Sell.GetComponent<Button>().onClick.AddListener(() => SellAmmoClicked(1));
        ammoButton2Sell.GetComponent<Button>().onClick.AddListener(() => SellAmmoClicked(2));
        gasButtonSell.GetComponent<Button>().onClick.AddListener(() => SellAmmoClicked(0));

        //sell rooms
        sellRoom1.GetComponent<Button>().onClick.AddListener(() => SellRoomsClicked(1));
        sellRoom2.GetComponent<Button>().onClick.AddListener(() => SellRoomsClicked(2));

        //sell modules
        sellModule1.GetComponent<Button>().onClick.AddListener(() => SellModulesClicked(1));
        sellModule2.GetComponent<Button>().onClick.AddListener(() => SellModulesClicked(2));
    }

    private void CloseShopClicked()
    {
        shopPanelSell.SetActive(false);
        shopPanel.SetActive(false);

        //check if we need to show ending text to the player
        if(AirshipStats.showSituationEndingText)
        {
            dialogue.ShowEndingDialogue();
        }
    }

    private void ShopNextPageClicked(int i)
    {
        if (i == 1) //buy shop
        {
            shopPanelSell.SetActive(true);
            //shopPanel.SetActive(false);
        }
        else //sell shop
        {
            shopPanelSell.SetActive(false);
            //shopPanel.SetActive(true);
        }
    }

    //buy button methods
    private void BuyAmmoClicked(int i)
    {
        if (i == 1) //ammo 1
        {
            if(AirshipStats.credits >= ammo1Cost)
            {
                AirshipStats.credits -= ammo1Cost;
                AirshipStats.ammo1++;
            }
        }
        else if(i == 2) //ammo 2
        {
            if (AirshipStats.credits >= ammo2Cost)
            {
                AirshipStats.credits -= ammo2Cost;
                AirshipStats.ammo2++;
            }
        }
        else //gas
        {
            if (AirshipStats.credits >= gasCost)
            {
                AirshipStats.credits -= gasCost;
                AirshipStats.gas++;
            }
        }
    }
    private void BuyRepairsClicked(int i)
    {
        if (i == 1) //repair 1
        {
            if (AirshipStats.credits >= repairCost)
            {
                if(AirshipStats.airshipCurrentHealth <= AirshipStats.airshipMaxHealth - repairAmount)
                {
                    AirshipStats.credits -= repairCost;
                    AirshipStats.airshipCurrentHealth += repairAmount;
                }
                else if (AirshipStats.airshipCurrentHealth < AirshipStats.airshipMaxHealth)
                {
                    AirshipStats.credits -= repairCost;
                    AirshipStats.airshipCurrentHealth = AirshipStats.airshipMaxHealth;
                }
            }
        }
        else //repair all
        {
            if (AirshipStats.credits >= repairAllAmountCost)
            {
                if (AirshipStats.airshipCurrentHealth <= AirshipStats.airshipMaxHealth - repairAmount)
                {
                    AirshipStats.credits -= repairAllAmountCost;
                    AirshipStats.airshipCurrentHealth += repairAllAmount * repairAmount;
                }
            }
        }
    }
    private void BuyRoomsClicked(int i)
    {
        if (i == 1) //room 1
        {
            //add room 1
        }
        else //room 2
        {
            //add room 2
        }
    }
    private void BuyModulesClicked(int i)
    {
        if (i == 1) //module 1
        {
            //add module 1
        }
        else //module 2
        {
            //add module 2
        }
    }

    //sell button methods
    private void SellAmmoClicked(int i)
    {
        if (i == 1) //ammo 1
        {
            if(AirshipStats.ammo1 > 0)
            {
                AirshipStats.credits += ammo1Cost / 2;
                AirshipStats.ammo1--;
            }
            
        }
        else if (i == 2) //ammo 2
        {
            if (AirshipStats.ammo2 > 0)
            {
                AirshipStats.credits += ammo2Cost / 2;
                AirshipStats.ammo2--;
            }
        }
        else //gas
        {
            if (AirshipStats.gas > 0)
            {
                AirshipStats.credits += gasCost / 2;
                AirshipStats.gas--;
            }
        }
    }
    private void SellRoomsClicked(int i)
    {
        if (i == 1) //room 1
        {
            //sell room 1
        }
        else //room 2
        {
            //sell room 2
        }
    }
    private void SellModulesClicked(int i)
    {
        if (i == 1) //module 1
        {
            //sell module 1
        }
        else //module 2
        {
            //sell module 2
        }
    }

    public void BuyCrewMemberClicked()
    {
        AirshipStats.howManyNewCrew += 1;
        ClickedOnBuyCrew();
    }
    
    private void ClickedOnBuyCrew()
    {
        if (AirshipStats.howManyNewCrew == 1)
        {
            //Vector3 pos = new Vector3(3f, 15f, 24.68f);
            Vector3 pos = posCrew1.position;
            GameObject Instance = (GameObject)Instantiate(crewmemberred, pos, transform.rotation);
        }
        else if (AirshipStats.howManyNewCrew == 2)
        {
            //Vector3 pos = new Vector3(3f, 16f, 24.68f);
            Vector3 pos = posCrew2.position;
            GameObject Instance = (GameObject)Instantiate(crewmemberblue, pos, transform.rotation);
        }
        else if (AirshipStats.howManyNewCrew == 3)
        {
            //Vector3 pos = new Vector3(3f, 17f, 24.68f);
            Vector3 pos = posCrew3.position;
            GameObject Instance = (GameObject)Instantiate(crewmemberred, pos, transform.rotation);
        }
        else if (AirshipStats.howManyNewCrew == 4)
        {
            //Vector3 pos = new Vector3(3f, 18f, 24.68f);
            Vector3 pos = posCrew4.position;
            GameObject Instance = (GameObject)Instantiate(crewmemberblue, pos, transform.rotation);
        }
    }
}
