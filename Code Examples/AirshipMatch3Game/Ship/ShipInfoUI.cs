using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipInfoUI : MonoBehaviour
{
    public GameObject shipInfoPanel, shipInfoPanelPage2;

    public Button shipInfoButton;
    public Button shipInfoButtonNextPanel, shipInfoButtonNextPanelPage2;
    public Button flyButton;
    public Button gunLevel;
    public Button gunSteamPlus;
    public Button gunSteamMinus;
    public Button cockpitLevel;
    public Button cockpitSteamPlus;
    public Button cockpitSteamMinus;
    public Button thrustLevel;
    public Button thrustSteamPlus;
    public Button thrustSteamMinus;
    public Button generatorLevel;
    public Button generatorSteamPlus;
    public Button generatorSteamMinus;

    public Text creditsText, creditsTextPage2;
    public Text gunRoomLevelText;
    public Text gunRoomSteamText;
    public Text gunRoomOnlineText;
    public Text cockpitRoomLevelText;
    public Text cockpitRoomSteamText;
    public Text cockpitRoomOnlineText;
    public Text thrustRoomLevelText;
    public Text thrustRoomSteamText;
    public Text thrustRoomOnlineText;
    public Text generatorRoomLevelText;
    public Text generatorRoomSteamText;
    public Text generatorRoomOnlineText;

    private bool shipInfoShowing = false;
    private bool shipInfoShowingPage2 = false;

    //private bool fightScene = false;
    private bool readyToFly = false;
    private float flyTimerMax;
    private float flyTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUiText();
        FillFlyButton();
    }

    public void FlyButtonFill()
    {
        flyTimer = flyTimerMax;
        flyButton.GetComponent<Image>().fillAmount = flyTimer / flyTimerMax;
    }

    private void FillFlyButton()
    {
        if(!readyToFly)
        {
            if(AirshipStats.cockpitRoomCurrentSteam <= 0 || AirshipStats.thrustRoomCurrentSteam <= 0 || AirshipStats.crewInCockpitRoom == false)
            {
                //do nothing if cockpit/thrust not working or if no pilot
            }
            else if (flyTimer < flyTimerMax && AirshipStats.cockpitRoomCurrentSteam > 0 && AirshipStats.crewInCockpitRoom)
            {
                flyTimer += (Time.deltaTime + AirshipStats.thrustRoomCurrentSteam/50);
                flyButton.GetComponent<Image>().fillAmount = flyTimer / flyTimerMax;
            }
            else //flight timer done
            {
                readyToFly = true;
                flyButton.GetComponent<Button>().interactable = true;
                flyButton.GetComponentInChildren<Text>().text = "FLY";
            }
        }
    }

    private void ShipInfoButtonClicked()
    {
        if (shipInfoShowing || shipInfoShowingPage2)
        {
            shipInfoPanel.SetActive(false);
            shipInfoPanelPage2.SetActive(false);
            shipInfoShowing = false;
            shipInfoShowingPage2 = false;
            AirshipStats.battlePause = false;
        }
        else
        {
            shipInfoPanel.SetActive(true);
            shipInfoShowing = true;
            AirshipStats.battlePause = true;
        }
    }

    private void GunButtonClicked(string button)
    {
        if(button == "plus")
        {
            if(AirshipStats.generatorRoomSteam >= 10 && (AirshipStats.gunRoomSteam < AirshipStats.gunRoomLevel * 10))
            {
                AirshipStats.gunRoomSteam += 10;
                AirshipStats.generatorRoomSteam -= 10;
            }
        }
        else if(button == "minus")
        {
            if(AirshipStats.gunRoomSteam > 0)
            {
                AirshipStats.gunRoomSteam -= 10;
                AirshipStats.generatorRoomSteam += 10;
            }
        }
        else //add level
        {
            if(AirshipStats.credits >= 100)
            {
                AirshipStats.gunRoomLevel++;
                AirshipStats.credits -= 100;
            }
        }
    }

    private void CockpitButtonClicked(string button)
    {
        if (button == "plus")
        {
            if (AirshipStats.generatorRoomSteam >= 10 && (AirshipStats.cockpitRoomSteam < AirshipStats.cockpitRoomLevel*10))
            {
                AirshipStats.cockpitRoomSteam += 10;
                AirshipStats.generatorRoomSteam -= 10;
            }
        }
        else if (button == "minus")
        {
            if (AirshipStats.cockpitRoomSteam > 0)
            {
                AirshipStats.cockpitRoomSteam -= 10;
                AirshipStats.generatorRoomSteam += 10;
            }
        }
        else //add level
        {
            if (AirshipStats.credits >= 100)
            {
                AirshipStats.cockpitRoomLevel++;
                AirshipStats.credits -= 100;
            }
        }
    }

    private void ThrustButtonClicked(string button)
    {
        if (button == "plus")
        {
            if (AirshipStats.generatorRoomSteam >= 10 && (AirshipStats.thrustRoomSteam < AirshipStats.thrustRoomLevel * 10))
            {
                AirshipStats.thrustRoomSteam += 10;
                AirshipStats.generatorRoomSteam -= 10;
            }
        }
        else if (button == "minus")
        {
            if (AirshipStats.thrustRoomSteam > 0)
            {
                AirshipStats.thrustRoomSteam -= 10;
                AirshipStats.generatorRoomSteam += 10;
            }
        }
        else //add level
        {
            if (AirshipStats.credits >= 100)
            {
                AirshipStats.thrustRoomLevel++;
                AirshipStats.credits -= 100;
            }
        }
    }

    private void GeneratorButtonClicked(string button)
    {
        if (button == "plus")
        {
            AirshipStats.generatorRoomSteam += 10;
        }
        else if (button == "minus")
        {
            AirshipStats.generatorRoomSteam -= 10;
        }
        else //add level
        {
            if (AirshipStats.credits >= 100)
            {
                AirshipStats.generatorRoomLevel += 10;
                AirshipStats.generatorRoomSteam += 10;
                AirshipStats.credits -= 100;
            }
        }
    }

    private void NextPageButtonClicked(string page)
    {
        if(page == "page1")
        {
            shipInfoPanel.SetActive(false);
            shipInfoPanelPage2.SetActive(true);
            shipInfoShowingPage2 = true;
            shipInfoShowing = false;
        }
        else if(page == "page2")
        {
            shipInfoPanel.SetActive(true);
            shipInfoPanelPage2.SetActive(false);
            shipInfoShowingPage2 = false;
            shipInfoShowing = true;
        }
    }

    private void FlyButtonClicked()
    {
        if (AirshipStats.battleMusicOn)
        {

            AirshipStats.battleMusicOn = false;
            AudioManager.instance.turnMusicOff();
        }
        SceneManager.LoadScene("Map");
    }

    private void InitializeButtons()
    {
        shipInfoPanel.SetActive(false);
        shipInfoPanelPage2.SetActive(false);
        shipInfoButton.GetComponent<Button>().onClick.AddListener(() => ShipInfoButtonClicked());
        shipInfoButtonNextPanel.GetComponent<Button>().onClick.AddListener(() => NextPageButtonClicked("page1"));
        shipInfoButtonNextPanelPage2.GetComponent<Button>().onClick.AddListener(() => NextPageButtonClicked("page2"));

        flyButton.GetComponent<Button>().onClick.AddListener(() => FlyButtonClicked());
        flyButton.GetComponent<Button>().interactable = false;
        flyButton.GetComponentInChildren<Text>().text = "";

        gunLevel.GetComponent<Button>().onClick.AddListener(() => GunButtonClicked("level"));
        gunSteamPlus.GetComponent<Button>().onClick.AddListener(() => GunButtonClicked("plus"));
        gunSteamMinus.GetComponent<Button>().onClick.AddListener(() => GunButtonClicked("minus"));

        cockpitLevel.GetComponent<Button>().onClick.AddListener(() => CockpitButtonClicked("level"));
        cockpitSteamPlus.GetComponent<Button>().onClick.AddListener(() => CockpitButtonClicked("plus"));
        cockpitSteamMinus.GetComponent<Button>().onClick.AddListener(() => CockpitButtonClicked("minus"));

        thrustLevel.GetComponent<Button>().onClick.AddListener(() => ThrustButtonClicked("level"));
        thrustSteamPlus.GetComponent<Button>().onClick.AddListener(() => ThrustButtonClicked("plus"));
        thrustSteamMinus.GetComponent<Button>().onClick.AddListener(() => ThrustButtonClicked("minus"));

        generatorLevel.GetComponent<Button>().onClick.AddListener(() => GeneratorButtonClicked("level"));
        generatorSteamPlus.GetComponent<Button>().onClick.AddListener(() => GeneratorButtonClicked("plus"));
        generatorSteamMinus.GetComponent<Button>().onClick.AddListener(() => GeneratorButtonClicked("minus"));

        //check if the fly button needs to be filled
        if (SceneManager.GetActiveScene().name == "Match3")
        {
            //fightScene = true;
            if (AirshipStats.thrustRoomLevel == 0)
            {
                flyTimerMax = 30f;
            }
            else
            {
                flyTimerMax = 30f / AirshipStats.thrustRoomLevel;
            }
        }
        else
        {
            //flyButton.GetComponent<Button>().interactable = true;
            //flyButton.GetComponentInChildren<Text>().text = "FLY";
            flyTimerMax = 1f;
        }
        flyButton.GetComponent<Image>().fillAmount = flyTimer / flyTimerMax;
    }

    private void UpdateUiText()
    {
        creditsText.text = "CREDITS: " + AirshipStats.credits.ToString();
        creditsTextPage2.text = "CREDITS: " + AirshipStats.credits.ToString();
        gunRoomLevelText.text = AirshipStats.gunRoomLevel.ToString();
        gunRoomSteamText.text = AirshipStats.gunRoomCurrentSteam + "/" + (AirshipStats.gunRoomLevel * 10);
        cockpitRoomLevelText.text = AirshipStats.cockpitRoomLevel.ToString();
        cockpitRoomSteamText.text = AirshipStats.cockpitRoomCurrentSteam + "/" + (AirshipStats.cockpitRoomLevel * 10);
        thrustRoomLevelText.text = AirshipStats.thrustRoomLevel.ToString();
        thrustRoomSteamText.text = AirshipStats.thrustRoomCurrentSteam + "/" + (AirshipStats.thrustRoomLevel * 10);
        generatorRoomLevelText.text = AirshipStats.generatorRoomLevel.ToString();
        generatorRoomSteamText.text = AirshipStats.generatorRoomCurrentSteam + "/" + (AirshipStats.generatorRoomLevel);

        if(AirshipStats.gunRoomHp <= 0)
        {
            gunRoomOnlineText.text = "Guns\nbroken!";
            gunRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.gunRoomHp < 5)
        {
            gunRoomOnlineText.text = "Guns\nheavily\ndamaged!";
            gunRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.gunRoomHp < 10)
        {
            gunRoomOnlineText.text = "Guns\ndamaged!";
            gunRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.gunRoomCurrentSteam > 0)
        {
            if(AirshipStats.crewInGunRoom)
            {
                gunRoomOnlineText.text = "Guns\nonline\n\nCrew+";
                gunRoomOnlineText.color = Color.green;
            }
            else
            {
                gunRoomOnlineText.text = "Guns\nonline";
                gunRoomOnlineText.color = Color.green;
            }
        }
        else
        {
            gunRoomOnlineText.text = "Guns\noffline";
            gunRoomOnlineText.color = Color.red;
        }
        
        if (AirshipStats.cockpitRoomHp <= 0)
        {
            cockpitRoomOnlineText.text = "Cockpit\nbroken!";
            cockpitRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.cockpitRoomHp < 5)
        {
            cockpitRoomOnlineText.text = "Cockpit\nheavily\ndamaged!";
            cockpitRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.cockpitRoomHp < 10)
        {
            cockpitRoomOnlineText.text = "Cockpit\ndamaged!";
            cockpitRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.cockpitRoomCurrentSteam > 0)
        {
            if (AirshipStats.crewInCockpitRoom)
            {
                cockpitRoomOnlineText.text = "Cockpit\nonline\nPilot+";
                cockpitRoomOnlineText.color = Color.green;
            }
            else
            {
                cockpitRoomOnlineText.text = "No\npilot";
                cockpitRoomOnlineText.color = Color.red;
            }
        }
        else
        {
            cockpitRoomOnlineText.text = "Cockpit\noffline";
            cockpitRoomOnlineText.color = Color.red;
        }

        if (AirshipStats.thrustRoomHp <= 0)
        {
            thrustRoomOnlineText.text = "Thrust\nbroken!";
            thrustRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.thrustRoomHp < 5)
        {
            thrustRoomOnlineText.text = "Thrust\nheavily\ndamaged!";
            thrustRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.thrustRoomHp < 10)
        {
            thrustRoomOnlineText.text = "Thrust\ndamaged!";
            thrustRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.thrustRoomCurrentSteam > 0)
        {
            if (AirshipStats.crewInThrustRoom)
            {
                thrustRoomOnlineText.text = "Thrust\nonline\n\nCrew+";
                thrustRoomOnlineText.color = Color.green;
            }
            else
            {
                thrustRoomOnlineText.text = "Thrust\nonline";
                thrustRoomOnlineText.color = Color.green;
            }
        }
        else
        {
            thrustRoomOnlineText.text = "Thrust\noffline";
            thrustRoomOnlineText.color = Color.red;
        }

        if (AirshipStats.generatorRoomHp <= 0)
        {
            generatorRoomOnlineText.text = "Generator\nbroken!";
            generatorRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.generatorRoomHp < 5)
        {
            generatorRoomOnlineText.text = "Generator\nheavily\ndamaged!"; 
            generatorRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.generatorRoomHp < 10)
        {
            generatorRoomOnlineText.text = "Generator\ndamaged!";
            generatorRoomOnlineText.color = Color.red;
        }
        else if (AirshipStats.crewInGeneratorRoom)
        {
            generatorRoomOnlineText.text = "Crew+";
            generatorRoomOnlineText.color = Color.green;
        }
        else
        {
            generatorRoomOnlineText.text = "";
        }
    }
}
