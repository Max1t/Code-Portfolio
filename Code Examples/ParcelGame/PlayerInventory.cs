using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private float _currency;
    public float Currency { get => _currency; set => _currency = value; }

    private PlayerGadgetInventory _gadgetInventory;
    public PlayerGadgetInventory GadgetInventory { get => _gadgetInventory; set => _gadgetInventory = value; }


    [SerializeField]
    private List<Parcel> _parcelInventory = new List<Parcel>();
    public List<Parcel> ParcelInventory { get => _parcelInventory; }

    private bool _snatchatron;
    public bool Snatchatron { get => _snatchatron; set => _snatchatron = value; }

    private int _parcelInventorySize = 10;

    [SerializeField]
    private PlayerInventoryUI _playerInventoryUI;
    public PlayerInventoryUI UI { get => _playerInventoryUI; }

    [SerializeField]
    private GiantInventoryUI _giantInventoryUI;

    [SerializeField]
    private GameObject _parcelPrefab;

    //database
    private DatabaseController databaseController;


    private static PlayerInventory instance;



    private void Awake()
    {
        instance = this;
        if (Gamemanager.Get == null) // 
        {
            return;
        }
        if (Gamemanager.Get.PlayerInventory == null)
        {
            Debug.Log("hai");
            Gamemanager.Get.PlayerInventory = instance;
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneSystem.Get.InventoryParent = this.gameObject;
    }

    /// <summary>
    /// Add a parcel to the inventory
    /// </summary>
    /// <param name="parcel"></param>
    /// <returns>
    /// <para>True when there is enough space to add a parcel </para>
    /// <para>False when there is no room in the inventory  </para>
    /// </returns>
    public bool AddParcel(Parcel parcel)
    {
        if (_parcelInventory.Count < _parcelInventorySize)
        {
            _parcelInventory.Add(parcel);
            parcel.transform.position = Vector3.zero;
            return true;
        }
        return false;   // Need some kind of failstate for full inventory
    }

    public bool AddParcel(Rarity rarity)
    {
        if (_parcelInventory.Count < _parcelInventorySize)
        {
            Parcel newParcel = Instantiate(_parcelPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Parcel>();
            newParcel.rarity = rarity;
            newParcel.specialParcel = true;
            _parcelInventory.Add(newParcel);
            newParcel.gameObject.SetActive(false);
            return true;
        }
        return false;   // Need some kind of failstate for full inventory
    }


    /// <summary>
    /// Used when depositing to the parcel giant
    /// </summary>
    /// <param name="parcel">Parcel gameobject deposited to the parcel giant</param>
    public void DepositParcel(Parcel parcel)
    {
        _parcelInventory.Remove(parcel);
        AddParcelDepositPointsToDatabase(parcel.rarity);
        Destroy(parcel.gameObject);
        UI.RefreshInventoryUI();
        _giantInventoryUI.RefreshGiantInventoryUI();
        if (PlayerVariables.dbConnectionFailed == false)
        {
            _giantInventoryUI.RefreshDatabaseInfo();
        }

        if (!PlayerVariables.playerParticipated)
        {
            Debug.Log("participated: " + PlayerVariables.playerParticipated + " Today: " + System.DateTime.Today + " Last Day participated: " + PlayerVariables.lastDayParticipated + "Online data last part: " + PlayerPrefs.GetString("date"));
            if (PlayerVariables.lastDayParticipated != System.DateTime.Today)
            {
                PlayerVariables.playerParticipated = true;
                PlayerVariables.lastDayParticipated = System.DateTime.Today;
                Debug.Log("Player part: " + PlayerVariables.playerParticipated);
                PlayerPrefs.SetString("date", System.DateTime.Today.ToString());
            }

        }
    }

    void OnGUI()
    {
        //Show participated on screen mobile debug
        //GUI.Label(new Rect(200, 400, 200, 40), "playerParticipated : " + PlayerVariables.playerParticipated);
    }

    public void DepositAllParcels()
    {
        List<Parcel> temp = new List<Parcel>();
        foreach (var parcel in _parcelInventory)
        {
            if (parcel.specialParcel)
            {
                temp.Add(parcel);
                AddParcelDepositPointsToDatabase(parcel.rarity);
            }
        }
        for (int i = 0; i < temp.Count; ++i)
        {
            Parcel toDestroy = temp[i];
            temp[i] = null;
            _parcelInventory.Remove(toDestroy);
            Destroy(toDestroy);
        }

        UI.RefreshInventoryUI();
        _giantInventoryUI.RefreshGiantInventoryUI();

        if (PlayerVariables.dbConnectionFailed == false)
        {
            _giantInventoryUI.RefreshDatabaseInfo();
        }

        if (!PlayerVariables.playerParticipated)
        {
            Debug.Log("opened legendary: " + PlayerVariables.playerParticipated + " Today: " + System.DateTime.Today + " Last Day participated: " + PlayerVariables.lastDayParticipated + "Online data last part: " + PlayerPrefs.GetString("date"));
            if (PlayerVariables.lastDayParticipated != System.DateTime.Today)
            {
                PlayerVariables.playerParticipated = false;
                PlayerVariables.lastDayParticipated = System.DateTime.Today;
                Debug.Log("Player part offline: " + PlayerVariables.playerParticipated);
                PlayerPrefs.SetString("date", System.DateTime.Today.ToString());
                PlayerPrefs.SetInt("playerPart", 0);
                Debug.Log("Player part database: " + PlayerPrefs.GetInt("playerPart"));
            }
        }
    }



    public void OpenNormalParcel(Parcel parcel)
    {
        _parcelInventory.Remove(parcel);
        Destroy(parcel.gameObject);
        UI.RefreshInventoryUI();
        _giantInventoryUI.RefreshGiantInventoryUI();

        // Add currency here
        if (PlayerVariables.dbConnectionFailed == false)
        {
            PlayerModifier moneyGained = new PlayerModifier();
            moneyGained.Money = +200;
            databaseController.PlayerModifierSubmit((moneyGained), player => OpenNormalParcelCallback(player));
        }
    }

    private void OpenNormalParcelCallback(PlayerInfo playerInfo)
    {
        _currency = playerInfo.Money;
        UI.RefreshCurrencyText();
    }

    public void GetPlayerCurrency()
    {
        if (PlayerVariables.dbConnectionFailed == false)
            databaseController.GetPlayerMoney((money) => GetPlayerCurrencyCallback(money));
    }

    private void GetPlayerCurrencyCallback(int fetchedCurrency)
    {
        _currency = fetchedCurrency;
        UI.RefreshCurrencyText();
    }


    //add points to the database when a parcel is deposited
    public void AddParcelDepositPointsToDatabase(Rarity rarity)
    {
        if (PlayerVariables.dbConnectionFailed == false)
        {
            Debug.Log("Database connection on. Add points from depositing a special parcel.");
            switch (rarity)
            {
                case Rarity.Common:
                    databaseController?.PointsSubmit((points) => RemoveOneParcelCallback(points), PlayerVariables.pointsCommonParcel, Rarity.Common);
                    Debug.Log($"added {PlayerVariables.pointsCommonParcel} points to the database");
                    break;
                case Rarity.Uncommon:
                    databaseController?.PointsSubmit((points) => RemoveOneParcelCallback(points), PlayerVariables.pointsUncommonParcel, Rarity.Uncommon);
                    Debug.Log($"added {PlayerVariables.pointsUncommonParcel} points to the database");
                    break;
                case Rarity.Rare:
                    databaseController?.PointsSubmit((points) => RemoveOneParcelCallback(points), PlayerVariables.pointsRareParcel, Rarity.Rare);
                    Debug.Log($"added {PlayerVariables.pointsRareParcel} points to the database");
                    break;
            }
        }
    }

    /// <summary>
    /// After a point is added to the database from parcel removal do this
    /// </summary>
    /// <param name="points"></param>
    private void RemoveOneParcelCallback(int points)
    {
        Debug.Log($"Total amount of points the player has {points}");
        UI.RefreshInventoryUI();
        _giantInventoryUI.RefreshDatabaseInfo();
    }

    private void PopulateInventory() // For testing
    {
        for (int i = 0; i < 11; ++i)
        {
            Parcel temp = Instantiate(_parcelPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Parcel>();
            temp.gameObject.SetActive(false);
            temp.gameObject.name = $"Parcel {i}";
            int rand = Random.Range(0, 4);
            if (rand < 3)
            {
                temp.rarity = (Rarity)Random.Range(0, 3);
                temp.specialParcel = true;
            }
            AddParcel(temp);
        }
    }


    private void Start()
    {
        //PopulateInventory();
        databaseController = GameObject.Find("Managers")?.GetComponent<DatabaseInitializer>()?.dbc;
        GetPlayerCurrency();
    }
}
