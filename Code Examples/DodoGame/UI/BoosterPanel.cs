using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoosterPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _boosterNameText;
    [SerializeField] private TMP_Text _boughtPanelText;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _activeText;

    [SerializeField] private GameObject _amountPanel;
    [SerializeField] private GameObject _boughtPanel;
    [SerializeField] private Image _boosterSprite;
    [SerializeField] private Button _buyButton;

    [SerializeField] private UiManager _ui;
    [SerializeField] private int _id;
    [SerializeField] private GameManager _gm;
    [SerializeField] private bool _active;

    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private TMP_Text _descriptionTextComponent;
    [SerializeField] private string _descText = "";

    [SerializeField] private Transform _transform;

    private BoosterType _type;


    // Update is called once per frame
    void LateUpdate()
    {
        if (_descriptionPanel != null && _descriptionPanel.activeInHierarchy)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            _descriptionPanel.transform.position = new Vector3(mousePos.x + 170, mousePos.y - 50, 1);
        }
    }

    public void SetupBoosterPanels(string name, string price, int id, UiManager ui, GameManager gm, BoosterType type)
    {
        _gm = gm;
        _ui = ui;
        _id = id;
        _boosterNameText.text = name;
        _priceText.text = price;
        _amountPanel.SetActive(_gm.Player.Boosters[id].Stackable);
        _boughtPanel.SetActive(!_gm.Player.Boosters[id].Stackable);
        _descriptionPanel = GameManager._gameManager.uiManager.descriptionPanel;
        _descriptionTextComponent = _descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
        _descText = Descriptions.BoosterDescriptions.Get(type);
        UpdatePanel();
        _boosterSprite.sprite = _ui.GetBoosterSprite((int)type);
    }

    public void UpdatePanel()
    {
        if (_gm.Player.Boosters[_id].Active)
        {
            if (_gm.Player.Boosters[_id].Stackable)
            {
                _amountText.text = _gm.Player.Boosters[_id].Amount.ToString();
                if (_gm.Player.Boosters[_id].Amount == _gm.Player.Boosters[_id].MaxAmount)
                {
                    _buyButton.gameObject.SetActive(false);
                }
                else
                {
                    _buyButton.gameObject.SetActive(true);
                }
            }
            else
            {
                _activeText.text = "Active";
                _activeText.color = Color.green;
                _buyButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (_gm.Player.Boosters[_id].Stackable)
            {
                _amountText.text = "0";
            }
            else
            {
                _activeText.text = "Inactive";
                _activeText.color = Color.red;
                _buyButton.gameObject.SetActive(true);
            }
        }
    }

    public void BuyButtonAction()
    {
        if (_gm.Player.BuyBooster(_gm.Player.Boosters[_id].Type))
        {
            _ui.UpdatePlayerMoney();
            UpdatePanel();
        }
        else
        {
            Debug.Log("No money, No booster, Tough life");
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
