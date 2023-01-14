using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SnapScrollRect : ScrollRect
{

    public bool _snapScrollbar;
    [SerializeField]
    private SnapToValue _snap;
    [SerializeField]
    private int _steps;

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_snapScrollbar)
            StartCoroutine(_snap.SnapTo(this, 0.2f, _steps));

    }

}
