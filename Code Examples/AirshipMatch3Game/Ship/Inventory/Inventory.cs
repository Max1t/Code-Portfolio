using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;


    public GunBase defaultGun1;
    public GunBase defaultGun2;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged OnInventoryChangedCallback;

    public List<GunBase> equippedItems = new List<GunBase>();
    public List<GunBase> cargoItems = new List<GunBase>();
    public int cargoSpace = 12;
    public int equipmentSpace = 4;

    public InstanceShipScene crew;


    public List<GunBase> allItems = new List<GunBase>();
    public List<GunBase> shopItems = new List<GunBase>();

    void Awake()
    {
        crew = FindObjectOfType<InstanceShipScene>();
        if (instance != null) return;
        instance = this;

        int[] testArray = new int[allItems.Count];
        for (int i = 0; i < allItems.Count; i++)
        {
            testArray[i] = allItems[i].itemCode;
        }
        if (testArray.GroupBy(x => x).Any(g => g.Count() > 1)) // Test for duplicate item codes
            throw new Exception("Duplicate item codes");       // Saving/loading depends on no duplicates

        testArray = new int[0];

        EquipItem(defaultGun1);
        AddItemToCargo(defaultGun2);



    }

    public void addCrew()
    {
        if (AirshipStats.credits >= 500f)
        {
            if (AirshipStats.howManyNewCrew < 4)
            {
                AudioManager.instance.Play("Osto");
                AirshipStats.howManyNewCrew++;
                crew.InstanceTheCrewFirst();
            }
            if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
        }
    }


    public bool CargoToEquip(GunBase item)
    {
        if (EquipItem(item))
        {
            if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
            RemoveItemFromCargo(item);
            return true;
        }
        else return false;
    }

    public bool EquipToCargo(GunBase item)
    {
        if (AddItemToCargo(item))
        {
            if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
            UnequipItem(item);
            return true;
        }
        else return false;

    }

    public bool AddItemToCargo(GunBase item)
    {
        if (cargoItems.Count >= cargoSpace) return false;
        cargoItems.Add(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
        return true;
    }

    public void RemoveItemFromCargo(GunBase item)
    {
        cargoItems.Remove(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
    }

    public bool EquipItem(GunBase item)
    {
        if (equippedItems.Count >= equipmentSpace) return false;
        equippedItems.Add(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
        return true;
    }

    public void UnequipItem(GunBase item)
    {
        equippedItems.Remove(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
    }


    public void PopulateShop()
    {
        foreach (var item in allItems)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
                shopItems.Add(item);
        }
    }

    public void AddToShop(GunBase item)
    {
        shopItems.Add(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
    }

    public void RemoveFromShop(GunBase item)
    {
        shopItems.Remove(item);
        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();
    }

    public void SellItem(GunBase item)
    {

        RemoveItemFromCargo(item);
        AddToShop(item);
        AirshipStats.credits += item.sellValue;
        Debug.Log(AirshipStats.credits);
    }

    public bool BuyItem(GunBase item)
    {
        if (AirshipStats.credits > item.buyValue)
        {
            AddItemToCargo(item);
            RemoveFromShop(item);
            AudioManager.instance.Play("Osto");
            AirshipStats.credits -= item.buyValue;
            Debug.Log(AirshipStats.credits);
            return true;
        }
        return false;
    }


    public int[] SaveCargoData()
    {
        int[] temp = new int[cargoItems.Count];
        for (int i = 0; i < cargoItems.Count; i++)
        {
            temp[i] = cargoItems[i].itemCode;
        }
        return temp;
    }

    public void LoadCargoData(int[] cargoData)
    {
        cargoItems.Remove(defaultGun2);
        foreach (int itemCode in cargoData)
        {
            GunBase tempItem = null;
            while (tempItem == null)
            {
                foreach (GunBase item in allItems)
                {
                    if (item.itemCode == itemCode)
                    {
                        tempItem = item;
                    }
                }
            }
            cargoItems.Add(tempItem);
        }
    }

    public int[] SaveEquippedData()
    {
        int[] temp = new int[equippedItems.Count];
        for (int i = 0; i < equippedItems.Count; i++)
        {
            temp[i] = equippedItems[i].itemCode;
        }
        return temp;
    }

    public void LoadEquippedData(int[] equippedData)
    {
        equippedItems.Remove(defaultGun1);
        foreach (int itemCode in equippedData)
        {
            GunBase tempItem = null;
            while (tempItem == null)
            {
                foreach (GunBase item in allItems)
                {
                    if (item.itemCode == itemCode)
                    {
                        tempItem = item;
                    }
                }
            }
            equippedItems.Add(tempItem);
        }
    }

}
