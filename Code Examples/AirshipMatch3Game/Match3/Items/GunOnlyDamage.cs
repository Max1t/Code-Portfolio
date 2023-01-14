using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Gun/OnlyDamage")]
public class GunOnlyDamage : GunBase
{
    public override void OnButtonDown()
    {
        matches.playersTurn = true;
        swap.ResetClicks();
        if (blockStorage.useBlocksFromStorageMultipleColor(colorsToUse, amountOfColorsToUse))
        {
            enemyHealth.TakeDamage(dmg);
            AudioManager.instance.Play(soundClip);
        }
    }

}
