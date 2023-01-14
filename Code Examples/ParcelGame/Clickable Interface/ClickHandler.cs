using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    private LayerMask _clickableLayerMask;
    public bool CanClick = true;
    [SerializeField]
    private Camera mainCam;

    private void Start()
    {
        Gamemanager.Get.clickHandler = this;
    }

#if UNITY_EDITOR_WIN
    void Update()
    {
        if (CanClick)
        {
            if (!EventSystem.current.IsPointerOverGameObject(-1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickableLayerMask))
                    {
                        hit.transform.GetComponent<IClickable>().OnClick();
                    }
                }
            }
        }
    }
#else
    void Update()
    {
        if (CanClick)
        {
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickableLayerMask))
                    {
                        hit.transform.GetComponent<IClickable>().OnClick();
                    }
                }
            }
        }
    }

#endif

}
