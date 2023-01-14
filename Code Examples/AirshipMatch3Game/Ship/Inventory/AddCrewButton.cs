using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCrewButton : MonoBehaviour
{
    public void OnClick()
    {
        Inventory.instance.addCrew();
    }
}
