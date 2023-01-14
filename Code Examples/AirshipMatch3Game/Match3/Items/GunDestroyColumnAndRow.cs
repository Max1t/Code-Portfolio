using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun/DestroyColumnAndRow")]
public class GunDestroyColumnAndRow : GunBase
{
    public override void OnButtonDown()
    {
        matches.playersTurn = true;
        if (Coroutine.choosingBlock == true)
        {
            Coroutine.choosingBlock = false;
            return;
        }
        swap.ResetClicks();
        if (blockStorage.checkBlocksFromStorageMultipleColor(colorsToUse, amountOfColorsToUse))
        {
            Coroutine.choosingBlock = true;
            Coroutine.StartChoosing(this, ChooseBlockCoroutine.Ability.COLUMNROW,soundClip);
            //  audio.Play("Sarjatuli");

        }
    }
}
