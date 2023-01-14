using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{

    public Inventory Inventory;
    public Transform inventorySlotParent;
    public Transform equipSlotParent;
    public Transform shopSlotsParent;

    public TextMeshProUGUI crewText;

    public CargoSlot[] cargoSlots;
    public EquipmentSlot[] equipSlots;
    public ShopSlot[] shopSlots;

    void Start()
    {
        Inventory = Inventory.instance;
        Inventory.instance.PopulateShop();
        Inventory.OnInventoryChangedCallback += UpdateCargoUI;
        Inventory.OnInventoryChangedCallback += UpdateEquipUI;
        Inventory.OnInventoryChangedCallback += UpdateShopUI;
        Inventory.OnInventoryChangedCallback += UpdateCrewText;
        cargoSlots = inventorySlotParent.GetComponentsInChildren<CargoSlot>();
        equipSlots = equipSlotParent.GetComponentsInChildren<EquipmentSlot>();
        shopSlots = shopSlotsParent.GetComponentsInChildren<ShopSlot>();
        UpdateCargoUI();
        UpdateEquipUI();
        UpdateShopUI();
        UpdateCrewText();

    }

    private void OnDestroy()
    {
        Inventory.OnInventoryChangedCallback -= UpdateCargoUI;
        Inventory.OnInventoryChangedCallback -= UpdateEquipUI;
        Inventory.OnInventoryChangedCallback -= UpdateShopUI;
        Inventory.OnInventoryChangedCallback -= UpdateCrewText;
    }


    public void UpdateCargoUI()
    {
        for (int i = 0; i < cargoSlots.Length; i++)
        {
            if (i < Inventory.instance.cargoItems.Count)
            {
                cargoSlots[i].Add(Inventory.instance.cargoItems[i]);
            }
            else
            {
                cargoSlots[i].Clear();
            }
        }
        crewText.text = "Crew: " + AirshipStats.howManyNewCrew;
    }

    public void UpdateEquipUI()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            if (i < Inventory.instance.equippedItems.Count)
            {
                equipSlots[i].Add(Inventory.instance.equippedItems[i]);
            }
            else
            {
                equipSlots[i].Clear();
            }
        }
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < Inventory.instance.shopItems.Count)
            {
                shopSlots[i].Add(Inventory.instance.shopItems[i]);
            }
            else
            {
                shopSlots[i].Clear();
            }
        }
    }

    public void UpdateCrewText()
    {
        crewText.text = "Crew: " + AirshipStats.howManyNewCrew;
    }
}
