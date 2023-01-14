using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarryWaterBucketInteractable : Interactable
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
                WaterTrough_Container _Container = collider.GetComponent<WaterTrough_Container>();
                if (_Container != null)
                {
                    AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Pour_Water, _Container.transform.position);
                    _Container.AddWater(World.WaterBarrel.WaterAmount);
                    break;
                }
            }
        }
        Destroy(gameObject); // Pool later
        return true;
    }

}
