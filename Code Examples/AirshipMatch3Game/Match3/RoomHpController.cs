using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomHpController : MonoBehaviour
{
    private Text gunRoomText;
    private Text cockpitRoomText;
    private Text generatorRoomText;
    private Text thrustRoomText;
    private Text mainRoomText;

    private string gunRoomDisabled;
    private string cockpitRoomDisabled;
    private string generatorRoomDisabled;
    private string thrustRoomDisabled;
    private string mainRoomDisabled;

    private Color lightRed = new Color32(255, 155, 155, 255);

    private bool mainRoomExists = false;
    private bool roomTextExist = false;
    public bool repairingGunRoom = false;
    public bool repairingCockpitRoom = false;
    public bool repairingGeneratorRoom = false;
    public bool repairingThrustRoom = false;
    public bool repairingMainRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("TextGunRoom") != null) //we are in a scene where the text boxes actually exist
        {
            roomTextExist = true;

            gunRoomText = GameObject.Find("TextGunRoom").GetComponent<Text>();
            cockpitRoomText = GameObject.Find("TextCockpitRoom").GetComponent<Text>();
            generatorRoomText = GameObject.Find("TextGeneratorRoom").GetComponent<Text>();
            thrustRoomText = GameObject.Find("TextThrustRoom").GetComponent<Text>();
            if (GameObject.Find("TextMainRoom") != null)
            {
                mainRoomExists = true;
                mainRoomText = GameObject.Find("TextMainRoom").GetComponent<Text>();
                mainRoomDisabled = StrikeThrough(mainRoomText.text);
            }

            gunRoomDisabled = StrikeThrough(gunRoomText.text);
            cockpitRoomDisabled = StrikeThrough(cockpitRoomText.text);
            generatorRoomDisabled = StrikeThrough(generatorRoomText.text);
            thrustRoomDisabled = StrikeThrough(thrustRoomText.text);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if(roomTextExist) //no need to loop this if the text doesn't actually exist
        {
            RoomOnFire();
            DamageLowersRoomLevel();
        }
    }

    private void DamageLowersRoomLevel()
    {
        //gun
        //hp = 0 => steam = 0
        if (AirshipStats.gunRoomHp <= 0)
        {
            AirshipStats.gunRoomCurrentSteam = 0;
            gunRoomText.color = Color.red;
            gunRoomText.text = gunRoomDisabled;
            gunRoomText.fontStyle = FontStyle.Italic;
        }
        //hp = 0,5 => steam = 0,5
        else if (AirshipStats.gunRoomHp < 5)
        {
            AirshipStats.gunRoomCurrentSteam = AirshipStats.gunRoomSteam * 0.5f;
            gunRoomText.color = Color.red;
            gunRoomText.fontStyle = FontStyle.Italic;
            gunRoomText.text = "GUN";
        }
        else if (AirshipStats.gunRoomHp < 10)
        {
            gunRoomText.fontStyle = FontStyle.Normal;
            gunRoomText.text = "GUN";
            gunRoomText.color = Color.red;
        }
        //hp full => steam full
        else
        {
            AirshipStats.gunRoomCurrentSteam = AirshipStats.gunRoomSteam;
            gunRoomText.color = Color.black;
        }
        
        //cockpit
        //hp = 0 => steam = 0
        if (AirshipStats.cockpitRoomHp <= 0)
        {
            AirshipStats.cockpitRoomCurrentSteam = 0;
            cockpitRoomText.color = Color.red;
            cockpitRoomText.text = cockpitRoomDisabled;
            cockpitRoomText.fontStyle = FontStyle.Italic;
        }
        //hp = 0,5 => steam = 0,5
        else if (AirshipStats.cockpitRoomHp < 5)
        {
            AirshipStats.cockpitRoomCurrentSteam = AirshipStats.cockpitRoomSteam * 0.5f;
            cockpitRoomText.color = Color.red;
            cockpitRoomText.fontStyle = FontStyle.Italic;
            cockpitRoomText.text = "COCKPIT";
        }
        else if (AirshipStats.cockpitRoomHp < 10)
        {
            cockpitRoomText.fontStyle = FontStyle.Normal;
            cockpitRoomText.color = Color.red;
            cockpitRoomText.text = "COCKPIT";
        }
        //hp full => steam full
        else
        {
            AirshipStats.cockpitRoomCurrentSteam = AirshipStats.cockpitRoomSteam;
            cockpitRoomText.color = Color.black;
        }

        //generator
        //hp = 0 => steam = 0
        if (AirshipStats.generatorRoomHp <= 0)
        {
            AirshipStats.generatorRoomCurrentSteam = 0;
            generatorRoomText.color = Color.red;
            generatorRoomText.text = generatorRoomDisabled;
            generatorRoomText.fontStyle = FontStyle.Italic;
        }
        //hp = 0,5 => steam = 0,5
        else if (AirshipStats.generatorRoomHp < 5)
        {
            AirshipStats.generatorRoomCurrentSteam = AirshipStats.generatorRoomSteam * 0.5f;
            generatorRoomText.fontStyle = FontStyle.Italic;
            generatorRoomText.color = Color.red;
            generatorRoomText.text = "GENERATOR";
        }
        else if (AirshipStats.generatorRoomHp < 10)
        {
            generatorRoomText.fontStyle = FontStyle.Normal;
            generatorRoomText.color = Color.red;
            generatorRoomText.text = "GENERATOR";
        }
        //hp full => steam full
        else
        {
            AirshipStats.generatorRoomCurrentSteam = AirshipStats.generatorRoomSteam;
            generatorRoomText.color = Color.black;
        }

        //thrust
        //hp = 0 => steam = 0
        if (AirshipStats.thrustRoomHp <= 0)
        {
            AirshipStats.thrustRoomCurrentSteam = 0;
            thrustRoomText.color = Color.red;
            thrustRoomText.text = thrustRoomDisabled;
            thrustRoomText.fontStyle = FontStyle.Italic;
        }
        //hp = 0,5 => steam = 0,5
        else if (AirshipStats.thrustRoomHp < 5)
        {
            AirshipStats.thrustRoomCurrentSteam = AirshipStats.thrustRoomSteam * 0.5f;
            thrustRoomText.fontStyle = FontStyle.Italic;
            thrustRoomText.color = Color.red;
            thrustRoomText.text = "THRUST";
        }
        else if (AirshipStats.thrustRoomHp < 10)
        {
            thrustRoomText.fontStyle = FontStyle.Normal;
            thrustRoomText.color = Color.red;
            thrustRoomText.text = "THRUST";
        }
        //hp full => steam full
        else
        {
            AirshipStats.thrustRoomCurrentSteam = AirshipStats.thrustRoomSteam;
            thrustRoomText.color = Color.black;
        }

        //main
        if(mainRoomExists)
        {
            //hp = 0 => steam = 0
            if (AirshipStats.mainRoomHp <= 0)
            {
                mainRoomText.color = Color.red;
                mainRoomText.text = mainRoomDisabled;
                mainRoomText.fontStyle = FontStyle.Italic;
            }
            //hp = 0,5 => steam = 0,5
            else if (AirshipStats.mainRoomHp < 5)
            {
                mainRoomText.text = "MAIN";
                mainRoomText.color = Color.red;
                mainRoomText.fontStyle = FontStyle.Italic;
            }
            else if (AirshipStats.mainRoomHp < 10)
            {
                mainRoomText.fontStyle = FontStyle.Normal;
                mainRoomText.color = Color.red;
                mainRoomText.text = "MAIN";
            }
            //hp full => steam full
            else
            {
                mainRoomText.color = Color.black;
            }
        }
    }

    private void RoomOnFire()
    {
        if(AirshipStats.gunRoomOnFire)
        {
            if(AirshipStats.gunRoomHp > 5)
            {
                AirshipStats.gunRoomHp -= Time.deltaTime/2;
            }
            else if (AirshipStats.gunRoomHp > 0)
            {
                AirshipStats.gunRoomHp -= Time.deltaTime / 2;
                gunRoomText.color = Color.red;
            }
            else
            {
                gunRoomText.text = gunRoomDisabled;
                gunRoomText.fontStyle = FontStyle.Italic;
            }
        }
        //room is not on fire so we repair it
        else
        {
            if(repairingGunRoom)
            {
                AirshipStats.gunRoomHp += Time.deltaTime / 2;
            }
        }
        if(AirshipStats.cockpitRoomOnFire)
        {
            if (AirshipStats.cockpitRoomHp > 5)
            {
                AirshipStats.cockpitRoomHp -= Time.deltaTime / 2;
            }
            else if (AirshipStats.cockpitRoomHp > 0)
            {
                AirshipStats.cockpitRoomHp -= Time.deltaTime / 2;
                cockpitRoomText.color = Color.red;
            }
            else
            {
                cockpitRoomText.text = cockpitRoomDisabled;
                cockpitRoomText.fontStyle = FontStyle.Italic;
            }
        }
        //room is not on fire so we repair it
        else
        {
            if (repairingCockpitRoom)
            {
                AirshipStats.cockpitRoomHp += Time.deltaTime / 2;
            }
        }
        if (AirshipStats.generatorRoomOnFire)
        {
            if (AirshipStats.generatorRoomHp > 5)
            {
                AirshipStats.generatorRoomHp -= Time.deltaTime / 2;
            }
            else if (AirshipStats.generatorRoomHp > 0)
            {
                AirshipStats.generatorRoomHp -= Time.deltaTime / 2;
                generatorRoomText.color = Color.red;
            }
            else
            {
                generatorRoomText.text = generatorRoomDisabled;
                generatorRoomText.fontStyle = FontStyle.Italic;
            }
        }
        //room is not on fire so we repair it
        else
        {
            if (repairingGeneratorRoom)
            {
                AirshipStats.generatorRoomHp += Time.deltaTime / 2;
            }
        }
        if (AirshipStats.thrustRoomOnFire)
        {
            if (AirshipStats.thrustRoomHp > 5)
            {
                AirshipStats.thrustRoomHp -= Time.deltaTime / 2;
            }
            else if (AirshipStats.thrustRoomHp > 0)
            {
                AirshipStats.thrustRoomHp -= Time.deltaTime / 2;
                thrustRoomText.color = Color.red;
            }
            else
            {
                thrustRoomText.text = thrustRoomDisabled;
                thrustRoomText.fontStyle = FontStyle.Italic;
            }
        }
        //room is not on fire so we repair it
        else
        {
            if (repairingThrustRoom)
            {
                AirshipStats.thrustRoomHp += Time.deltaTime / 2;
            }
        }
        if (AirshipStats.mainRoomOnFire)
        {
            if (AirshipStats.mainRoomHp > 5)
            {
                AirshipStats.mainRoomHp -= Time.deltaTime / 2;
            }
            else if (AirshipStats.mainRoomHp > 0)
            {
                AirshipStats.mainRoomHp -= Time.deltaTime / 2;

                if (mainRoomExists)
                {
                    mainRoomText.color = Color.red;
                }
            }
            else
            {

                if (mainRoomExists)
                {
                    mainRoomText.text = mainRoomDisabled;
                    mainRoomText.fontStyle = FontStyle.Italic;
                }
            }
        }
        //room is not on fire so we repair it
        else
        {
            if (repairingMainRoom)
            {
                AirshipStats.mainRoomHp += Time.deltaTime / 2;
            }
        }
    }

    public void RoomTookDamage(string room)
    {
        if (room == "gun")
        {
            AirshipStats.gunRoomHp -= 5;
        }
        else if (room == "cockpit")
        {
            AirshipStats.cockpitRoomHp -= 5;
        }
        else if (room == "generator")
        {
            AirshipStats.generatorRoomHp -= 5;
        }
        else if (room == "thrust")
        {
            AirshipStats.thrustRoomHp -= 5;
        }
        else if (room == "main")
        {
            AirshipStats.mainRoomHp -= 5;
        }
    }

    public string StrikeThrough(string s)
    {
        string strikethrough = "" + '\u0336';
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }
}
