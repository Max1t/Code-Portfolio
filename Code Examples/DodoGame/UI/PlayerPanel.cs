using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _playerSelectionDropdown;
    [SerializeField] private TMP_Dropdown _inputSelectionDropdown;
    [SerializeField] private TMP_Text _playerText;

    private int _playerSelection = -1;
    private int _inputSelection = -1;

    public int InputSelection => _inputSelection;
    public int PlayerSelection => _playerSelection;

    public void OnValueChangedPlayerSelection()
    {
        _playerSelection = _inputSelectionDropdown.value;
    }

    public void OnValueChangedInputSelection()
    {
        _inputSelection = _inputSelectionDropdown.value;
    }

    public void SetText(string text)
    {
        _playerText.text = text;
    }
}
