using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkInteractable : Interactable
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public override bool Interact(PlayerInteraction player)
    {
        if (player._interactableCarried != null)
        {
            if (gameManager.boosterManager.PlaceDodoInArk(player._interactableCarried.GetComponent<StatePatternDodo>()))
            {
                player._interactableCarried.GetComponent<StatePatternDodo>().dodoInStasis = true;
                player._interactableCarried.GetComponent<StatePatternDodo>().isCarried = false;
                player.DropInteractable(transform);
            }
            else
            {
                player._interactableCarried.GetComponent<StatePatternDodo>().isCarried = false;
                player._interactableCarried.GetComponent<StatePatternDodo>().currentState.EndState();
                player.DropInteractable();
            }
            
        }


        return true;
    }

}
