using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : Interactable
{
    [SerializeField] private GameObject waterCarry;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public override bool Interact(PlayerInteraction player)
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Take_Water, transform.position);
        _gameManager.WaterConsumed((int)World.WaterBarrel.WaterAmount);
        GameObject water = Instantiate(waterCarry, transform.position, Quaternion.identity);
        player.PickUpInteractable(water.transform);
        return true;
    }


}
