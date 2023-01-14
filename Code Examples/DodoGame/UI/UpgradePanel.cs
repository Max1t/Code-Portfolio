using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UpgradePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _upgradeName;
    [SerializeField] private TMP_Text _activeText;
    [SerializeField] private TMP_Text _priceText;

    [SerializeField] private Image _upgradeSprite;

    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _activeButton;

    [SerializeField] private UiManager _ui;

    [SerializeField] private int _id;
    [SerializeField] private bool _active;


    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private TMP_Text _descriptionTextComponent;
    [SerializeField] private string _descText = "";

    void LateUpdate()
    {
        if (_descriptionPanel != null && _descriptionPanel.activeInHierarchy)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            _descriptionPanel.transform.position = new Vector3(mousePos.x + 170, mousePos.y - 50, 1);
        }
    }

    public void SetupUpgadePanels(string name, string price, bool owned, bool active, int id, UiManager ui, UpgradeType type)
    {
        _ui = ui;
        _id = id;
        _upgradeName.text = name;

        if (owned)
        {
            _buyButton.gameObject.SetActive(false);
            GetMaintenanceCost();
        }
        else
        {
            _priceText.text = price;
            _activeButton.gameObject.SetActive(false);
        }

        _descriptionPanel = GameManager._gameManager.uiManager.descriptionPanel;
        _descriptionTextComponent = _descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
        _descText = Descriptions.UpgradeDescriptions.Get(type);
        _upgradeSprite.sprite = _ui.GetUpgradeSprite(id);
        SetActive(active);
    }

    void GetMaintenanceCost()
    {
        _priceText.text = _ui.GetUpgradeMaintenanceCost(_id).ToString();
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            _activeButton.GetComponent<Image>().color = Color.green;
            _activeText.text = "Active";
        }
        else
        {
            _activeButton.GetComponent<Image>().color = Color.red;
            _activeText.text = "Inactive";
        }

        _active = active;
    }

    public void BuyButtonAction()
    {
        if (_ui.TryBuyUpgrade(_id))
        {
            _buyButton.gameObject.SetActive(false);
            _activeButton.gameObject.SetActive(true);
            GetMaintenanceCost();
            if (_ui.SetActive(_id))
            {
                SetActive(true);
            }

        }
    }

    public void ActiveButtonAction()
    {
        if (_ui.SetActive(_id))
        {
            SetActive(!_active);
        }
        else
        {
            Debug.Log("No moneys for upkeep");
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionTextComponent.text = _descText;
        _descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionPanel.SetActive(false);
    }
}
