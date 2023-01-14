using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BigMapInfo
{

    public bool _canGo;
    public bool _canMove;
    public bool _wentThere;


    public BigMapInfo(bool canGo, bool canMove, bool wentThere)
    {
        _canGo = canGo;
        _canMove = canMove;
        _wentThere = wentThere;
    }
    
}
