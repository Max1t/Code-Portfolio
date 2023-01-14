using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeehiveInteractable : Interactable
{
    [SerializeField] private bool isCarried = false;
    [SerializeField] private Collider _collider;
    [SerializeField] private LayerMask _mask;
    private GameManager _manager;

    private void Awake()
    {
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override bool Interact(PlayerInteraction player)
    {
        if (isCarried == false)
        {
            player.PickUpInteractable(this.transform);
            transform.GetChild(0).localPosition = new Vector3(0, 0.3f, 0);
            _collider.enabled = false;
            isCarried = true;
            _manager.foodPlaces[gameObject] = true;
        }
        else
        {
            player.DropInteractable();
            transform.GetChild(0).localPosition = new Vector3(0, 0.8f, 0);
            isCarried = false;
            _collider.enabled = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f, _mask, QueryTriggerInteraction.Collide);
            _manager.foodPlaces[gameObject] = false;
            foreach (Collider collider in hitColliders)
            {
                if (collider.GetComponent<Compost_Container>() != null)
                {
                    AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Trashcan, collider.transform.position);
                    _manager.worldManager.Bees.DisableBeehive(gameObject);
                    break;
                }
            }
        }
        return true;
    }


}
