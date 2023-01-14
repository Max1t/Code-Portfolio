
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public enum Gadget { Snatchatron, Magnifier, Freeze, Magnet };

public class PlayerGadgetInventory : MonoBehaviour
{
    private DatabaseController databaseController;

    [SerializeField]
    private SnatchatronUI _snatchatronUIController;
    [SerializeField]
    private float _snatchatronTimerInSeconds;

    private int _snatchatronAmount = 3;         // TODO Server side
    private int _magnifierAmount = 3;
    private int _freezeAmount = 3;
    private int _magnetAmount = 3;


    [SerializeField] private int _snatchatronCost;     // TODO Server side
    [SerializeField] private int _magnifierCost;
    [SerializeField] private int _freezeCost;
    [SerializeField] private int _magnetCost;


    [SerializeField] private Button _snatchatronButton;
    [SerializeField] private Button _magnifierButton;
    [SerializeField] private Button _freezeButton;
    [SerializeField] private Button _magnetButton;

    [SerializeField] private TextMeshProUGUI _snatchatronAmountText;
    [SerializeField] private TextMeshProUGUI _magnifierAmountText;
    [SerializeField] private TextMeshProUGUI _freezeAmountText;
    [SerializeField] private TextMeshProUGUI _magnetAmountText;

    public int SnatchatronAmount { get => _snatchatronAmount; }
    public int MagnifierAmount { get => _magnifierAmount; }
    public int FreezeAmount { get => _freezeAmount; }
    public int MagnetAmount { get => _magnetAmount; }


    // Start is called before the first frame update
    void Start()
    {
        databaseController = GameObject.Find("Managers")?.GetComponent<DatabaseInitializer>()?.dbc;
        Gamemanager.Get.PlayerInventory.GadgetInventory = this;
        InitializeButtons();
        RefreshGadgetUI();
    }


    public void OnBuyItem(int cost, Gadget gadget)
    {
        //click buy button here
        databaseController.GetPlayerMoney((money) => OnBuyItemCallBack(cost, money, gadget));
    }

    private void OnBuyItemCallBack(int cost, int money, Gadget gadget)
    {
        if (money >= cost)
        {
            //have enough money to buy a new item
            PlayerModifier moneyUsed = new PlayerModifier();
            moneyUsed.Money = -cost;
            databaseController.PlayerModifierSubmit((moneyUsed), player => MoneyUsedCallback(player));
            // Add amount to database
            switch (gadget)
            {
                case Gadget.Snatchatron:
                    ++_snatchatronAmount;
                    break;
                case Gadget.Magnifier:
                    ++_magnifierAmount;
                    break;
                case Gadget.Magnet:
                    ++_magnetAmount;
                    break;
                case Gadget.Freeze:
                    ++_freezeAmount;
                    break;
            }
            RefreshGadgetUI();

        }
        else
        {
            //not enough money to buy a new item
            Debug.Log("Not enuf moni");
        }
    }

    private void MoneyUsedCallback(PlayerInfo playerInfo)
    {
        Gamemanager.Get.PlayerInventory.UI.RefreshCurrencyText(playerInfo.Money);
    }

    private void InitializeButtons()
    {
        _snatchatronButton.onClick.AddListener(() => OnBuyItem(_snatchatronCost, Gadget.Snatchatron));
        _magnifierButton.onClick.AddListener(() => OnBuyItem(_magnifierCost, Gadget.Magnifier));
        _freezeButton.onClick.AddListener(() => OnBuyItem(_freezeCost, Gadget.Freeze));
        _magnetButton.onClick.AddListener(() => OnBuyItem(_magnetCost, Gadget.Magnet));
    }

    private void RefreshGadgetUI()
    {
        _snatchatronAmountText.text = $"{SnatchatronAmount}";
        _magnifierAmountText.text = $"{MagnifierAmount}";
        _magnetAmountText.text = $"{MagnetAmount}";
        _freezeAmountText.text = $"{FreezeAmount}";
    }

    public void UseGadget(Gadget gadget)
    {
        switch (gadget)
        {
            case Gadget.Snatchatron:
                --_snatchatronAmount;
                break;
            case Gadget.Magnifier:
                --_magnifierAmount;
                break;
            case Gadget.Magnet:
                --_magnetAmount;
                break;
            case Gadget.Freeze:
                --_freezeAmount;
                break;
        }
        RefreshGadgetUI();
    }

    public void ActivateSnatchatron()
    {
        if (_snatchatronAmount > 0)
        {
            _snatchatronUIController.StartTimer(_snatchatronTimerInSeconds);
            --_snatchatronAmount;
            RefreshGadgetUI();
            Gamemanager.Get.PlayerInventory.UI.ToggleInventoryUI();
        }
    }
}
