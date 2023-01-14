using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBagInteractable : Interactable
{

    [SerializeField] private GameObject foodCarry;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override bool Interact(PlayerInteraction player)
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Take_Food, transform.position);
        _gameManager.FoodConsumed((int)World.FoodBag.FoodAmount);
        GameObject food = Instantiate(foodCarry, transform.position, Quaternion.identity);
        player.PickUpInteractable(food.transform);
        return true;
    }

}
