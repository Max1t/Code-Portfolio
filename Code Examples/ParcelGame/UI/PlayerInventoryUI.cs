using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{

    public Color Common;
    public Color Uncommon;
    public Color Rare;
    public Color[] colorArray;

    [SerializeField]
    private PlayerInventory _playerInventory = null;

    [SerializeField]
    private GameObject _parcelUIPrefab = null;

    [SerializeField]
    private GameObject _emptyUIPrefab = null;

    [SerializeField]
    private Transform _inventoryPanelContentParent = null;

    [SerializeField]
    private Transform _inventoryPanelUI = null;

    [SerializeField]
    private Transform _inventoryPanelButtonOff;

    [SerializeField]
    private Transform _inventoryPanelButtonOn;

    [SerializeField]
    private List<Transform> _uiItems = new List<Transform>();

    [SerializeField]
    private TextMeshProUGUI _currencyText;

    public GameObject SnatchatronRequiredPopup;

    [SerializeField]
    private Scrollbar _inventoryScrollbar;

    private void Start()
    {
        colorArray = new Color[] { Common, Uncommon, Rare };

        if (Gamemanager.Get == null)
        {
            return;
        }
        _playerInventory = Gamemanager.Get.PlayerInventory;

    }

    public void RefreshInventoryUI()
    {

        foreach (var item in _uiItems)
        {
            Destroy(item.gameObject);
        }
        _uiItems.Clear();


        if (_playerInventory.ParcelInventory.Count == 0)
        {
            var uiItem = Instantiate(_emptyUIPrefab, _inventoryPanelContentParent);
            uiItem.name = $"Empty Inventory";
            _uiItems.Add(uiItem.transform);
            _playerInventory.GetPlayerCurrency();
            return;
        }

        int i = 0;

        foreach (var item in _playerInventory.ParcelInventory)
        {
            var uiItem = Instantiate(_parcelUIPrefab, _inventoryPanelContentParent);
            uiItem.name = $"UIItem {i} Rarity:{item.rarity}";
            _uiItems.Add(uiItem.transform);
            if (!item.specialParcel)
            {
                Button openbutton = uiItem.transform.GetChild(2).GetComponent<Button>();
                openbutton.onClick.AddListener(() => _playerInventory.OpenNormalParcel(item));
                openbutton.GetComponentInChildren<TextMeshProUGUI>().text = "Open";
                openbutton.gameObject.SetActive(true);
            }
            if (item.specialParcel)
                uiItem.GetComponentInChildren<TextMeshProUGUI>().text = $"{item.rarity} Parcel";
            else
                uiItem.GetComponentInChildren<TextMeshProUGUI>().text = "Parcel";
            if (item.specialParcel)
            {
                Color uiColor = colorArray[(int)item.rarity];
                uiItem.transform.GetChild(3).gameObject.SetActive(true);
                uiItem.transform.GetChild(3).GetComponent<Image>().color = uiColor;
            }
            ++i;
        }
        _playerInventory.GetPlayerCurrency();
    }

    public void RefreshCurrencyText()
    {
        _currencyText.text = $"{_playerInventory.Currency}";
    }

    public void RefreshCurrencyText(float newCurrency)
    {
        _currencyText.text = $"{newCurrency}";
    }

    public void ToggleInventoryUI()
    {
        if (_inventoryPanelUI.gameObject.activeInHierarchy)
        {
            PlayerVariables.playerMove = true;
            _inventoryPanelUI.gameObject.SetActive(false);
            _inventoryPanelButtonOn.gameObject.SetActive(true);
            _inventoryPanelButtonOff.gameObject.SetActive(false);

        }
        else
        {
            _inventoryPanelUI.gameObject.SetActive(true);
            _inventoryPanelButtonOn.gameObject.SetActive(false);
            _inventoryPanelButtonOff.gameObject.SetActive(true);
            PlayerVariables.playerMove = false;
            _inventoryScrollbar.value = 0;

        }
        RefreshInventoryUI();
        Debug.Log("REfresh player inv playermove: " + PlayerVariables.playerMove);
    }

}
