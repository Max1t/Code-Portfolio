using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationToyInteractable : Interactable
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override bool Interact(PlayerInteraction player)
    {
        gameManager.boosterManager.ActivateActivationToy();
        return true;
    }

}
