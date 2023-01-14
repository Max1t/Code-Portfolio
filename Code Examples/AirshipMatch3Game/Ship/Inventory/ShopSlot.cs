using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : Slot
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            base.OnPointerClick(eventData);
            Inventory.instance.BuyItem((GunBase)item);
        }
    }

}
