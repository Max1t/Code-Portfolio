using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GiantInventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _giantParcelUIPrefab = null;


    [SerializeField]
    private GameObject _emptyUIPrefab = null;


    [SerializeField]
    private TextMeshProUGUI _inventoryPanelDatabaseText;

    private DatabaseController databaseController;

    public Color Common;
    public Color Uncommon;
    public Color Rare;
    public Color[] colorArray;

    private PlayerInventory _playerInventory = null;

    [SerializeField]
    private Transform _giantInventoryPanelContentParent = null;

    [SerializeField]
    private Transform _giantInventoryPanelUI = null;

    [SerializeField]
    private Transform _giantInventoryPanelButtonOff;

    [SerializeField]
    private Transform _inventoryButtonOn;

    [SerializeField]
    private List<Transform> _uiItems = new List<Transform>();

    private int playersPoints = 0;
    private int totalPoints = 0;
    private string highestScoringPlayersName = "";


    private void Start()
    {
        colorArray = new Color[] { Common, Uncommon, Rare };

        if (Gamemanager.Get)
            _playerInventory = Gamemanager.Get.PlayerInventory;

        _inventoryPanelDatabaseText.gameObject.SetActive(false);
        databaseController = GameObject.Find("Managers")?.GetComponent<DatabaseInitializer>()?.dbc;
    }

    public void RefreshGiantInventoryUI()
    {
        foreach (var item in _uiItems)
        {
            Destroy(item.gameObject);
        }
        _uiItems.Clear();

        if (_playerInventory.ParcelInventory.Count == 0)
        {
            var uiItem = Instantiate(_emptyUIPrefab, _giantInventoryPanelContentParent);
            uiItem.name = $"Empty Inventory";
            _uiItems.Add(uiItem.transform);
            return;
        }

        int i = 0;
        foreach (var item in _playerInventory.ParcelInventory)
        {
            var uiItem = Instantiate(_giantParcelUIPrefab, _giantInventoryPanelContentParent);
            uiItem.name = $"UIItem {i} Rarity:{item.rarity}";
            _uiItems.Add(uiItem.transform);
            uiItem.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => _playerInventory.DepositParcel(item));
            if (item.specialParcel)
            {
                uiItem.GetComponentInChildren<TextMeshProUGUI>().text = $"{item.rarity} Parcel";
                Color uiColor = colorArray[(int)item.rarity];
                uiItem.transform.GetChild(3).gameObject.SetActive(true);
                uiItem.transform.GetChild(3).GetComponent<Image>().color = uiColor;
            }
            else
            {
                uiItem.GetComponentInChildren<TextMeshProUGUI>().text = "Parcel";
                uiItem.transform.GetChild(2).gameObject.SetActive(false);
            }
            ++i;
        }
    }

    public void ToggleGiantInventoryUI()
    {
        if (PlayerVariables.dbConnectionFailed == false)
        {
            Debug.Log("Getting points from database");
            databaseController?.GetMyPoints((points) => DatabaseMyPointsCallback(points));
        }
        else
        {
            _inventoryPanelDatabaseText.gameObject.SetActive(false);
        }
        if (_giantInventoryPanelUI.gameObject.activeInHierarchy)
        {
            PlayerVariables.playerMove = true;
            _giantInventoryPanelUI.gameObject.SetActive(false);
            _giantInventoryPanelButtonOff.gameObject.SetActive(false);
            _inventoryPanelDatabaseText.gameObject.SetActive(false);
            _inventoryButtonOn.gameObject.SetActive(true);

        }
        else
        {
            _giantInventoryPanelUI.gameObject.SetActive(true);
            _giantInventoryPanelButtonOff.gameObject.SetActive(true);
            _inventoryPanelDatabaseText.gameObject.SetActive(true);
            _inventoryButtonOn.gameObject.SetActive(false);
            PlayerVariables.playerMove = false;
        }
        Debug.Log("Refresh giant inv playermove: " + PlayerVariables.playerMove);
        RefreshGiantInventoryUI();
    }

    public void RefreshDatabaseInfo()
    {
        if (PlayerVariables.dbConnectionFailed == false)
        {
            Debug.Log("Getting points from database");
            databaseController?.GetMyPoints((points) => DatabaseMyPointsCallback(points));
        }
    }

    public void DatabaseMyPointsCallback(int myPoints)
    {
        databaseController.GetTotalPoints((totalPoints) => DatabaseTotalPointsCallback(myPoints, totalPoints));
        databaseController.GetBestScoringPlayer((bestPlayer) => DatabaseBestPlayerCallback(bestPlayer));
    }

    private void DatabaseTotalPointsCallback(int myPoints, int totalPoints)
    {
        Debug.Log($"Player's points: {myPoints}   Total points: {totalPoints}");
        playersPoints = myPoints;
        this.totalPoints = totalPoints;
        UpdatingScoringTextAndRefreshUI();
    }

    private void DatabaseBestPlayerCallback(PlayerInfo player)
    {
        if (player == null)
            return;
        Debug.Log($"Best player's points: {player.Points} name: {player.Name}");
        highestScoringPlayersName = player.Name;
        UpdatingScoringTextAndRefreshUI();
    }

    private void UpdatingScoringTextAndRefreshUI()
    {
        _inventoryPanelDatabaseText.text = $"My points: {playersPoints}{Environment.NewLine}Total points: {totalPoints}/100{Environment.NewLine}Highest scorer: {highestScoringPlayersName}";
        RefreshGiantInventoryUI();
    }

    public void LoadStackScene()
    {
        SceneSystem.Get.LoadMinigame(7);
    }
}
