using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class ItemBase : ScriptableObject
{
    public int itemCode;
    public string itemName;

    [Multiline]
    public string tooltip;

    public Sprite image;

    public int buyValue;
    public int sellValue;


}
