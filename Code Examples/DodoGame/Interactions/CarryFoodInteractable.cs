using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarryFoodInteractable : Interactable
{

    [SerializeField] private LayerMask _mask;

    public override bool Interact(PlayerInteraction player)
    {
        player.DropInteractable();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f, _mask, QueryTriggerInteraction.Collide);
        if (hitColliders.Any())
        {
            foreach (Collider collider in hitColliders)
            {
                FoodTrough_Container _Container = collider.GetComponent<FoodTrough_Container>();
                if (_Container != null)
                {
                    AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Pour_Food, _Container.transform.position);
                    _Container.AddFood(World.FoodBag.FoodAmount);
                    break;
                }
            }
        }
        Destroy(gameObject); // Pool later
        return true;
    }


}
