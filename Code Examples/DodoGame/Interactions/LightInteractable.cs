using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : Interactable
{
    [SerializeField] Light _light;

    public override bool Interact(PlayerInteraction player)
    {
        // Disable light buff from game manager?
        /*_light.enabled = !_light.enabled;
        return true;*/
        return false;
    }

}
