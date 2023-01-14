using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxInteractable : Interactable
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override bool Interact(PlayerInteraction player)
    {
        gameManager.worldManager.TurnIncubationLightOn();
        return true;
    }

}
