using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemBase item;

    public Transform ImageHolder;
    public Image icon;
    public GameObject tooltip;
    public GameObject tooltipText;
    public GameObject pivot;
    public RectTransform backGroundTransform;
    public RectTransform textBoxTransform;
    public TextMeshProUGUI description;
    IEnumerator coroutine;
    float textPadding = 4f;

    public bool interactable;

    void Start()
    {
        coroutine = tooltipFollow();
        backGroundTransform = tooltip.GetComponent<RectTransform>();
        textBoxTransform = tooltipText.GetComponent<RectTransform>();
    }


    public void Add(ItemBase newItem)
    {
        item = newItem;
        ImageHolder = transform.GetChild(0);
        icon = ImageHolder.GetComponent<Image>();
        icon.sprite = item.image;
        icon.enabled = true;
        interactable = true;
    }

    public void Clear()
    {
        item = null;
        ImageHolder = transform.GetChild(0);
        icon = ImageHolder.GetComponent<Image>();
        icon.sprite = null;
        icon.enabled = false;
        interactable = false;
    }

    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            description.text = item.itemName;
            description.text += "\n" + item.tooltip;
            description.text += "\nBuy:" + item.buyValue;
            description.text += "\nSell:" + item.sellValue;
            Vector2 dynamicSize = new Vector2(description.preferredHeight+ 60f, description.preferredHeight + 40f);
            backGroundTransform.sizeDelta = dynamicSize;
            tooltip.SetActive(true);
            StartCoroutine(coroutine);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            tooltip.SetActive(false);
            StopCoroutine(coroutine);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            tooltip.SetActive(false);
            StopCoroutine(coroutine);
        }
    }

    public void EnableInteractable()
    {
        interactable = true;
    }

    public void DisableInteractable()
    {
        interactable = false;
    }

    IEnumerator tooltipFollow()
    {
        while (true)
        {
            pivot.transform.position = Input.mousePosition;
            yield return null;
        }
    }
}
