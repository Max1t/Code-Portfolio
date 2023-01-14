using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private Animator _animator;
    private float _cooldown = 2f;
    void Start()
    {
        _animator = _uiPanel.GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("ShowUI");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("HideUI");
    }

}
