using UnityEngine;
using UnityEngine.UI;

public class InvisbleButton : MonoBehaviour
{
    private RectTransform _transform;
    public Dialogue dialogue;

    void Awake()
    {
        _transform = transform as RectTransform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_transform, Input.mousePosition))
            {
                dialogue.clickedSkipButton = true;
            }
        }
    }
}