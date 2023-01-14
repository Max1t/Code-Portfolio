using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            base.OnPointerClick(eventData);
            Inventory.instance.EquipToCargo((GunBase)item);
        }
    }

}
