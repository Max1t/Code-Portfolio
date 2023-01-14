using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Gun/DestroyAllOfOneColor")]
public class GunDestroyAllOfOneColor : GunBase
{
    [Header("Pick a color to destroy")]
    public string colorToDestroy;
    [Header("Damage per block destroyed")]
    public int damageMultiplier = 0;


    public override void OnButtonDown()
    {
        matches.playersTurn = true;
        swap.ResetClicks();
        if (blockStorage.useBlocksFromStorageMultipleColor(colorsToUse, amountOfColorsToUse))
        {
            if (damageMultiplier > 0) enemyHealth.TakeDamage(dmg + abilities.DestroyAllOfColor(colorToDestroy) * damageMultiplier);
            else abilities.DestroyAllOfColor(colorToDestroy);
            AudioManager.instance.Play(soundClip);
        }
    }

}
