using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Touchable : Interactable
{

    public abstract bool Touch(PlayerInteraction player);

}
