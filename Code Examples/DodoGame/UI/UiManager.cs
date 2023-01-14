using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanelPrefab;
    [SerializeField] private GameObject _boosterPanelPrefab;
    [SerializeField] private GameObject _shopContent;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private List<GameObject> _upgradePanels;
    [SerializeField] private List<GameObject> _boosterPanels;
    [SerializeField] private int _playerPendingMaintenanceCost;
    [SerializeField] private TMP_Text _playerMoneyText;
    [SerializeField] private TMP_Text _maintenanceMoney;

    [SerializeField] private GameManager _gm;
    [SerializeField] private AchievementManager _am;

    [SerializeField] private float _achievementPopupTimer = 2f;
    [SerializeField] private GameObject _achievementPopup;
    [SerializeField] private TMP_Text _achievementPopupTitle;
    [SerializeField] private TMP_Text _achievementPopupDescription;

    [SerializeField] private GameObject _uiPanel;

    [SerializeField] private List<GameObject> _devSettingsPanels;
    [SerializeField] private GameObject _devSettingsPanelPrefab;
    [SerializeField] private GameObject _devSettingsParent;
    [SerializeField] private TMP_InputField _devSettingsImportField;
    [SerializeField] private TMP_InputField _devSettingsExportField;
    private Dictionary<string, float> _devSettingsUnsaved;

    [SerializeField] private TMP_InputField _dodoSpawnerNameField;
    [SerializeField] private TMP_Dropdown _dodoSpawnerGenderDropdown;
    [SerializeField] private TMP_Dropdown _dodoSpawnerTypeDropdown;
    [SerializeField] private TMP_Dropdown _dodoSpawnerGrowthDropdown;
    [SerializeField] private Slider _dodoSpawnerHealthSlider;
    [SerializeField] private Slider _dodoSpawnerHungerSlider;
    [SerializeField] private Slider _dodoSpawnerThirstSlider;
    [SerializeField] private Slider _dodoSpawnerHappinessSlider;
    [SerializeField] private TMP_Text _dodoSpawnerHealthValue;
    [SerializeField] private TMP_Text _dodoSpawnerHungerValue;
    [SerializeField] private TMP_Text _dodoSpawnerThirstValue;
    [SerializeField] private TMP_Text _dodoSpawnerHappinessValue;
    [SerializeField] private Toggle _dodoSpawnerFreezeStatsToggle;
    [SerializeField] private DodoSpawner _dodoSpawner;


    // Player input selections
    private Dictionary<int, int> _playerInputSelections;
    private Dictionary<int, int> _localPlayerProfiles;

    private List<int> _playersToCreate;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _startMenuPlayPanel;
    [SerializeField] private GameObject _startMenuSettingsPanel;
    [SerializeField] private GameObject _startMenuCreditsPanel;
    [SerializeField] private GameObject _startMenuIntroPanel;

    [SerializeField] private Sprite[] _boosterSprites;
    [SerializeField] private Sprite[] _upgradeSprites;


    // Pause Menu
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Button _firstPauseMenuButton;

    // Dev Menu
    [SerializeField] private GameObject _devMenuPanel;
    [SerializeField] private Button _firstDevMenuButton;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    [SerializeField] private List<PlayerData> _players;
    [SerializeField] private TMP_Dropdown _playerSelection;
    [SerializeField] private TMP_InputField _playerNameField;
    [SerializeField] private TMP_Text _playerInfoText;

    [SerializeField] private TMP_Dropdown _levelsDropdown;
    public GameObject descriptionPanel;
    public int PlayerMoneyReward;
    [SerializeField] private TMP_Text _winScreenPlayerMoney;

    [SerializeField] private bool _multiPlayer;
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private GameObject _startRegisterButtonMultiplayer;
    [SerializeField] private GameObject _startGameMultiplayerButton;

    public TextMeshProUGUI multiPlayerPanelText1;
    public TextMeshProUGUI multiPlayerPanelText2;


    private List<Button> _buttonList = new List<Button>();
    private void Awake()
    {

        //_am = GetComponent<AchievementManager>();
        DataManager.InitSettingsFromStaticSettings();

    }

    // Start is called before the first frame update
    void Start()
    {
        //_am.AchievementUnlocked += OnAchievementUnlocked;
        InitDevSettings();
        SelectDefaultButton();

    }

    private void FixedUpdate()
    {
        if (EventSystem.current != null)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.currentSelectedGameObject.activeInHierarchy != true)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }

        _buttonList = UnityEngine.Object.FindObjectsOfType<Button>().ToList();
        if (EventSystem.current?.currentSelectedGameObject == null || !_buttonList.Find(x => x.gameObject.activeInHierarchy == true))
        {
            SelectDefaultButton();
        }
    }

    private void InitDevSettings()
    {
        if (DataManager.SettingsData == null) return;

        _devSettingsPanels = new List<GameObject>();
        _devSettingsUnsaved = DataManager.SettingsData.GetSettingsToDictionary();
        foreach (KeyValuePair<string, float> value in _devSettingsUnsaved)
        {
            _devSettingsPanels.Add(Instantiate(_devSettingsPanelPrefab, _devSettingsParent.transform));
            _devSettingsPanels[_devSettingsPanels.Count - 1].GetComponent<DevSettingsPanel>().Text.text = value.Key;
            _devSettingsPanels[_devSettingsPanels.Count - 1].GetComponent<DevSettingsPanel>().Value.text = value.Value.ToString();
        }
    }

    public void SelectDefaultButton()
    {

        Button buttonToSelect = _buttonList.Find(x => x.gameObject.activeInHierarchy == true);
        buttonToSelect?.Select();
    }

    public void OnSkipMenus(DevTestingEnum state)
    {
        switch (state)
        {
            case DevTestingEnum.SinglePlayerKeyboard:
                OnSinglePlayerStartEndless(1);
                break;
            case DevTestingEnum.SinglePlayerController:
                OnSinglePlayerStartEndless(0);
                break;
            case DevTestingEnum.TwoPlayerController:
                OnMultiPlayerStartEndlessSkipMenu();
                break;
            case DevTestingEnum.TwoPlayerKeyboard:
                OnMultiPlayerStartEndlessSkipMenu();
                break;
            case DevTestingEnum.TwoPlayerKeyboardController:
                OnMultiPlayerStartEndlessSkipMenu();
                break;
        }
    }

    #region Settings
    public void OnSettingsMasterSliderValueChanged()
    {
        // TODO adjust sound
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsMasterMuteValueChanged()
    {
        // TODO mute sounds
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsSfxSliderValueChanged()
    {
        // TODO  adjust sound
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsSfxMuteValueChanged()
    {
        // TODO mute sounds
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsMusicSliderValueChanged()
    {
        // TODO adjust sound
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsMusicMuteValueChanged()
    {
        // TODO mute sounds
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsAddPlayers()
    {
        // TODO functionality
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnSettingsMapKeys()
    {
        // TODO functionality
        Debug.Log("This does nothing yet but feel free to fill in the functionality");
    }

    public void OnStartMenuQuitButton()
    {
        OnUIEventPlaySound(2);
        Application.Quit();
    }
    #endregion

    public void OnUIEventPlaySound(int i)
    {
        switch (i)
        {
            case 1:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Select, transform.position);
                break;
            case 2:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Back, transform.position);
                break;
            case 3:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Buy, transform.position);
                break;
            case 4:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Failure, transform.position);
                break;
            case 5:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Success, transform.position);
                break;
            case 6:
                AudioManager.manager.PlayOneShot(AudioManager.manager._data.UI.Achievement, transform.position);
                break;
        }
    }

    public void OnQuitButton()
    {
        OnUIEventPlaySound(1);
        Application.Quit();
    }
    public void OnSinglePlayerStartEndless(int i)
    {
        _gm.inputManager.SpawnSinglePlayer((DeviceType)i);
    }

    public void PlayersRegistered()
    {
        _startGameMultiplayerButton.SetActive(true);
    }

    public void OnMultiPlayerStartEndless()
    {
        _gm.inputManager.StartMultiplayer();
    }

    public void OnMultiPlayerSpawnEndless()
    {
        _gm.inputManager.SpawnMultiplayer();
    }

    public void OnMultiPlayerStartEndlessSkipMenu()
    {
        _gm.inputManager.StartMultiplayer();
    }

    public void OnMultiPlayerStartStory(int[] args)
    {


    }

    public void OnSinglePlayerStartStory(int i)
    {
        Debug.Log("Start sp story");
        return;
        _gm.inputManager.SpawnSinglePlayer((DeviceType)i);
    }


    #region DeveloperMenu's
    public void ToggleDevMenu()
    {
        if (!_devMenuPanel.activeSelf)
        {
            _devMenuPanel.SetActive(true);
            _firstDevMenuButton.Select();
        }
        else
        {
            _devMenuPanel.SetActive(false);
        }
    }
    public void InitSpawner(DodoSpawner spawner)
    {
        _dodoSpawner = spawner;
        OnDodoSpawnerHappinessSliderValueChanged();
        OnDodoSpawnerHealthSliderValueChanged();
        OnDodoSpawnerHungerSliderValueChanged();
        OnDodoSpawnerThirstSliderValueChanged();
    }
    public void OnSaveDevSettings()
    {
        foreach (var go in _devSettingsPanels)
        {
            var panel = go.GetComponent<DevSettingsPanel>();
            float f;
            if (float.TryParse(panel.Value.text, out f))
            {
                _devSettingsUnsaved[panel.Text.text] = float.Parse(panel.Value.text);
            }
        }
        DataManager.SettingsData.DevConsoleSendSettings(_devSettingsUnsaved);
        DataManager.SaveData(new SettingsWrapper() { SettingsData = DataManager.SettingsData });
    }

    public void OnLoadDevSettings()
    {
        DataManager.SettingsData = DataManager.LoadData<SettingsWrapper>().SettingsData;
        DataManager.SettingsData.SetSettings();
        UpdateDevSettingsData();
    }
    private void UpdateDevSettingsData()
    {
        _devSettingsUnsaved = DataManager.SettingsData.GetSettingsToDictionary();
        for (int i = 0; i < _devSettingsPanels.Count; i++)
        {
            _devSettingsPanels[i].GetComponent<DevSettingsPanel>().Text.text = _devSettingsUnsaved.ElementAt(i).Key;
            _devSettingsPanels[i].GetComponent<DevSettingsPanel>().Value.text = _devSettingsUnsaved.ElementAt(i).Value.ToString("0.00");
        }
    }
    public void OnImportDevSettings()
    {
        DataManager.SettingsData = JsonUtility.FromJson<SettingsWrapper>(_devSettingsImportField.text).SettingsData;
        DataManager.SettingsData.SetSettings();
        UpdateDevSettingsData();
    }

    public void OnExportDevSettings()
    {
        _devSettingsExportField.text = JsonUtility.ToJson(new SettingsWrapper() { SettingsData = DataManager.SettingsData }, true);
    }
    public void OnDodoSpawnerSpawnButton()
    {
        //_dodoSpawner.SpawnDodo(_dodoSpawnerNameField.text, _dodoSpawnerGenderDropdown.value, (BaseType)_dodoSpawnerGenderDropdown.value, _dodoSpawnerGrowthDropdown.value);
        _dodoSpawner.SpawnDodo(_dodoSpawnerNameField.text, _dodoSpawnerGenderDropdown.value,
            (BaseType)_dodoSpawnerTypeDropdown.value, _dodoSpawnerGrowthDropdown.value,
            (int)_dodoSpawnerHealthSlider.value, (int)_dodoSpawnerHungerSlider.value,
            (int)_dodoSpawnerThirstSlider.value, (int)_dodoSpawnerHappinessSlider.value, _dodoSpawnerFreezeStatsToggle.isOn);
    }
    public void OnDodoSpawnerRandomNameButton()
    {
        if (_dodoSpawnerGenderDropdown.value == 1)
        {
            _dodoSpawnerNameField.text = NameGenerator.GetFemaleName();
        }
        else if (_dodoSpawnerGenderDropdown.value == 0)
        {
            _dodoSpawnerNameField.text = NameGenerator.GetMaleName();
        }
    }
    public void OnDodoSpawnerHealthSliderValueChanged()
    {
        _dodoSpawnerHealthValue.text = _dodoSpawnerHealthSlider.value.ToString();
    }

    public void OnDodoSpawnerHungerSliderValueChanged()
    {
        _dodoSpawnerHungerValue.text = _dodoSpawnerHungerSlider.value.ToString();
    }

    public void OnDodoSpawnerThirstSliderValueChanged()
    {
        _dodoSpawnerThirstValue.text = _dodoSpawnerThirstSlider.value.ToString();
    }

    public void OnDodoSpawnerHappinessSliderValueChanged()
    {
        _dodoSpawnerHappinessValue.text = _dodoSpawnerHappinessSlider.value.ToString();
    }
    #endregion DeveloperMenu's

    public void OnAchievementUnlocked(object source, PlayerAchievementEventArgs e)
    {
        //_gm.Player
        StartCoroutine("AchievementPopup", e.Achievement);
    }

    #region Shop
    public Sprite GetBoosterSprite(int i)
    {
        return _boosterSprites[i];
    }

    public Sprite GetUpgradeSprite(int i)
    {
        return _upgradeSprites[i];
    }

    public void ShopButtonAction()
    {
        OnUIEventPlaySound(1);
        _shopPanel.SetActive(true);
        _shopContent.SetActive(true);
        _loseScreen.SetActive(false);
        _winScreen.SetActive(false);
        if (_upgradePanels == null || _upgradePanels.Count < 1)
        {
            List<UpgradeData> _data = _gm.Player.Upgrades;
            _upgradePanels = new List<GameObject>();
            for (int i = 0; i < _data.Count; i++)
            {
                _upgradePanels.Add(Instantiate(_upgradePanelPrefab, _shopContent.transform));
                _upgradePanels[i].GetComponent<UpgradePanel>().
                    SetupUpgadePanels(_data[i].Name, _data[i].Price.ToString(), _data[i].Unlocked, _data[i].Active, i, this, _data[i].Type);
            }
        }

        foreach (GameObject go in _upgradePanels)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in _boosterPanels)
        {
            go.SetActive(false);
        }
        UpdatePlayerMoney();
        UpdateMaintenanceMoney();
    }

    public void BoosterButtonAction()
    {
        if (_boosterPanels == null || _boosterPanels.Count < 1)
        {
            List<BoosterData> _data = _gm.Player.Boosters;
            _boosterPanels = new List<GameObject>();
            for (int i = 0; i < _data.Count; i++)
            {
                _boosterPanels.Add(Instantiate(_boosterPanelPrefab, _shopContent.transform));
                _boosterPanels[i].GetComponent<BoosterPanel>().SetupBoosterPanels(_data[i].Name, _data[i].Price.ToString(), i, this, _gm, _data[i].Type);
            }
        }
        foreach (GameObject go in _boosterPanels)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in _upgradePanels)
        {
            go.SetActive(false);
        }
        UpdatePlayerMoney();
    }

    public void SetUpgradeActive(int id)
    {
        _gm.Player.Upgrades[id].Active = !_gm.Player.Upgrades[id].Active;
    }

    public bool TryBuyUpgrade(int id)
    {
        if (_gm.Player.BuyUpgrade(_gm.Player.Upgrades[id].Type))
        {
            UpdatePlayerMoney();
            return true;
        }
        return false;

    }

    public float GetUpgradeMaintenanceCost(int id)
    {
        return _gm.Player.Upgrades[id].MaintenancePrice;
    }

    public void UpdatePlayerMoney()
    {
        _playerMoneyText.text = _gm.Player.Money.ToString();
    }

    public bool SetActive(int id)
    {
        if (_gm.Player.Upgrades[id].Active)
        {
            _playerPendingMaintenanceCost -= _gm.Player.Upgrades[id].MaintenancePrice;
            SetUpgradeActive(id);
            UpdateMaintenanceMoney();
            return true;
        }
        if (CheckMoneyForMaintenanceFees(id))
        {
            _playerPendingMaintenanceCost += _gm.Player.Upgrades[id].MaintenancePrice;
            SetUpgradeActive(id);
            UpdateMaintenanceMoney();
            return true;
        }
        return false;
    }

    public bool CheckMoneyForMaintenanceFees(int id)
    {
        return _gm.Player.Money > _playerPendingMaintenanceCost + _gm.Player.Upgrades[id].MaintenancePrice;
    }

    public void UpdateMaintenanceMoney()
    {
        if (_playerPendingMaintenanceCost == 0)
        {
            _maintenanceMoney.text = "0";
            _maintenanceMoney.color = Color.green;
        }
        else
        {
            _maintenanceMoney.text = "-" + _playerPendingMaintenanceCost.ToString();
            _maintenanceMoney.color = Color.red;
        }

    }

    public void CloseShopButtonAction()
    {
        OnUIEventPlaySound(2);
        _gm.upgradeManager.ActivateUpgrades();
        _gm.boosterManager.ActivateBoosters();
        if (_gm._levelFailed)
        {
            _loseScreen.SetActive(true);
        }

        if (_gm._levelCompleted)
        {
            _winScreen.SetActive(true);
        }
        DataManager.SavePlayerData();
    }
    #endregion

    public void TogglePauseMenu()
    {
        if (!_pauseMenuPanel.activeSelf)
        {
            _pauseMenuPanel.SetActive(true);
            _firstPauseMenuButton.Select();
        }
        else
        {
            _pauseMenuPanel.SetActive(false);
        }
    }

    IEnumerator AchievementPopup(PlayerAchievementData data)
    {
        //_gm.Player.Achievements.Find(achievement => achievement.Type == data.Type).UnlockAchievement(data.UnlockedTime);
        _achievementPopupTitle.text = data.Name;
        _achievementPopupDescription.text = data.Description;
        _achievementPopup.SetActive(true);
        yield return new WaitForSeconds(_achievementPopupTimer);
        _achievementPopup.SetActive(false);
    }

    public void OnLevelWon()
    {
        OnUIEventPlaySound(5);
        _winScreenPlayerMoney.text = PlayerMoneyReward.ToString();
        _winScreen.SetActive(true);
        AudioManager.manager.OnStopGame();
    }

    public void OnLevelLost()
    {
        OnUIEventPlaySound(4);
        _loseScreen.SetActive(true);
        AudioManager.manager.OnStopGame();
    }

    public void OnQuitToMenuButton()
    {
        OnUIEventPlaySound(2);
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        _startMenuPlayPanel.SetActive(true);
        _mainMenu.SetActive(true);
        AudioManager.manager.OnStopGame();
        GameManager._gameManager.inputManager.ResetPlayers();

        foreach (var dodo in _gm.DodoPool)
        {
            if (dodo.activeInHierarchy)
            {
                dodo.GetComponent<StatePatternDodo>().DisableInstant();
            }
        }
    }

    public void OnReplayButton()
    {
        OnUIEventPlaySound(2);
        _gm.levelManager.ChangeLevel(_gm.levelManager.CurrentLevel.Id);
        _loseScreen.SetActive(false);
        _winScreen.SetActive(false);
        _gm.PreLevelSetup();
        AudioManager.manager.OnStartGame();
    }

    public void OnLoadNextLevelButton()
    {
        _winScreen.SetActive(false);
        OnUIEventPlaySound(1);
        if (_gm.levelManager.ChangeLevel(_gm.levelManager.CurrentLevel.Id + 1))
        {
            _gm.PreLevelSetup();
            AudioManager.manager.OnStartGame();
        }
    }

    public void OnStart()
    {
        _mainMenu.SetActive(true);
    }

    public void OnPlayPanelClose()
    {

    }

    public void OnMainMenuPlayButton()
    {
        OnUIEventPlaySound(1);
        _startMenuSettingsPanel.SetActive(false);
        _startMenuCreditsPanel.SetActive(false);
        _startMenuPlayPanel.SetActive(true);
        _players = DataManager.LoadPlayerData();
        Debug.Log(_players.Count);
        if (_players?.Count > 0)
        {
            List<string> _playerNames = new List<string>();
            for (int i = 0; i < _players.Count; i++)
            {
                _playerNames.Add(_players[i].Name);
            }
            foreach (var name in _playerNames)
            {
                Debug.Log(name);
            }
            _playerSelection.AddOptions(_playerNames);
        }
        _playerInfoText.gameObject.SetActive(false);
        _playerNameField.gameObject.SetActive(true);
    }

    public void OnPlayerSelectionDropdownValueChanged()
    {
        if (_playerSelection.value != 0)
        {
            var player = _players[_playerSelection.value - 1];
            _playerInfoText.gameObject.SetActive(true);
            _playerNameField.gameObject.SetActive(false);
            _playerInfoText.text = player.Name + "\nMoney: " + player.Money + "\n" + "Dodos collected: " + player.Dodos.TotalCount + " / " + _gm.collectionManager.GetTotalDodoCount();
        }
        else
        {
            _playerInfoText.gameObject.SetActive(false);
            _playerNameField.gameObject.SetActive(true);
        }
    }

    public void OnPlayerPanelOkButton()
    {
        if (_playerSelection.value == 0)
        {
            _gm.Player = new PlayerData(Guid.NewGuid().ToString(), _playerNameField.text, 0, null);
            DataManager.SavePlayerData();
        }
        else
        {
            _gm.Player = _players[_playerSelection.value - 1];
        }
        List<string> levels = new List<string>();
        var l = _gm.levelManager.GetLevels();
        Debug.Log("Found " + l.Count + " levels");
        _levelsDropdown.options.Clear();
        try
        {
            for (int i = 0; i < _players[_playerSelection.value - 1].Levels.Count; i++)
            {
                if (_players[_playerSelection.value - 1].Levels[i].CompletedLevel)
                {
                    levels.Add(l.Find(item => item.Id == _players[_playerSelection.value - 1].Levels[i].Id).Name);
                }
                else if (_players[_playerSelection.value - 1].Levels[i - 1].CompletedLevel)
                {
                    levels.Add(l.Find(item => item.Id == _players[_playerSelection.value - 1].Levels[i].Id).Name);
                }
            }
            _levelsDropdown.AddOptions(levels);
        }
        catch (Exception)
        {
            levels.Add("Level 1");
            _levelsDropdown.AddOptions(levels);
        }
    }

    public void OnStartGameButton()
    {
        OnUIEventPlaySound(5);
        if (_gm.levelManager.ChangeLevel(_levelsDropdown.value))
        {
            _gm.inputManager.SetPlayerSpawn(GameObject.FindGameObjectWithTag("PlayerSpawn").transform);
            if (_multiPlayer)
            {
                AudioManager.manager.OnStartGame();
                OnMultiPlayerSpawnEndless();
                _gm.PreLevelSetup();
            }
            else
            {
                OnSinglePlayerStartEndless(0);
                AudioManager.manager.OnStartGame();
                _gm.PreLevelSetup();
            }
        }
        else
        {
            Debug.Log("Failed to load level");
        }
    }

    public void OnStartMenuMultiplayerToggleButton(bool value)
    {
        _multiPlayer = value;
        _startGameButton.SetActive(!value);
        _startRegisterButtonMultiplayer.SetActive(value);

    }

    public void OnMainMenuSettingsButton()
    {
        OnUIEventPlaySound(1);
        _startMenuSettingsPanel.SetActive(true);
        _startMenuCreditsPanel.SetActive(false);
        _startMenuPlayPanel.SetActive(false);
    }

    public void OnMainMenuCreditsButton()
    {
        OnUIEventPlaySound(1);
        _startMenuSettingsPanel.SetActive(false);
        _startMenuCreditsPanel.SetActive(true);
        _startMenuPlayPanel.SetActive(false);
    }
}
