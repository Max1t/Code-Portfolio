using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CargoSlot : Slot
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (interactable && eventData.button == PointerEventData.InputButton.Right)
        {
            base.OnPointerClick(eventData);
            Inventory.instance.SellItem((GunBase)item);
        }

        if (interactable && eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerClick(eventData);
            Inventory.instance.CargoToEquip((GunBase)item);
        }

    }


}