using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Repair")]
public class ItemRepair : GunBase
{

    public float repairValue;

    public override void OnButtonDown()
    {
        matches.playersTurn = true;
        swap.ResetClicks();
        if (blockStorage.useBlocksFromStorageMultipleColor(colorsToUse, amountOfColorsToUse))
        {
            AirshipStats.airshipCurrentHealth += repairValue;
            if (AirshipStats.airshipCurrentHealth > AirshipStats.airshipMaxHealth)
                AirshipStats.airshipCurrentHealth = AirshipStats.airshipMaxHealth;
            GameObject.Find("GameControl").GetComponent<ResourcesScriptUI>().UpdateUI();
        }
    }
}
