using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IslandInfo
{
    public float _x;
    public float _y;
    public bool _canGo;
    public bool _searchNext;
    public bool _goneThere;
    public bool _exit;
    public bool _mapTarget; //target of a special mission from dialogue
    public string _name;
    public int _number; //order number in which islands are moved towards each other

    public IslandInfo(float x, float y, bool canGo, bool searchNext, bool goneThere, bool exit, string name, int number, bool mapTarget)
    {
        _x = x;
        _y = y;
        _canGo = canGo;
        _searchNext = searchNext;
        _goneThere = goneThere;
        _exit = exit;
        _name = name;
        _number = number;
        _mapTarget = mapTarget;
    }
}
