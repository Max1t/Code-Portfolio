using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirshipStats : MonoBehaviour
{
    //debugging
    public static bool debugging = false; //boolean for removing randomness for debugging reasons
    public static bool alwaysMatch3Scene = false; //putting one of these booleans on makes the scene happen everytime if debugging boolean is also true
    public static bool alwaysShipScene = true;
    public static bool skipMatch3Scene = false;
    public static bool noAutoLoad = false; //helps testing maps

    //actual game variables (not for debugging)
    //stats that are tracked through the game
    public static int gunRoomLevel = 1;
    public static float gunRoomCurrentSteam = 10; //if the room takes damage this lowers
    public static float gunRoomSteam = 10; //how much steam the current gun room has: 1 level - 10 steam
    public static float gunRoomHp = 10f;
    public static int cockpitRoomLevel = 1;
    public static float cockpitRoomCurrentSteam = 10;
    public static float cockpitRoomSteam = 10;
    public static float cockpitRoomHp = 10f;
    public static int thrustRoomLevel = 1;
    public static float thrustRoomCurrentSteam = 10;
    public static float thrustRoomSteam = 10;
    public static float thrustRoomHp = 10f;
    public static int generatorRoomLevel = 30;
    public static float generatorRoomCurrentSteam = 0;
    public static float generatorRoomSteam = 0;
    public static float generatorRoomHp = 10f;
    public static float mainRoomHp = 10f;

    public static bool battleMusicOn = false;
    public static bool finalbattleMusicOn = false;
    public static float soundVolume = 1;

    public static int credits = 200;
    public static int gas = 10;

    public static int ammo1 = 0;
    public static int ammo2 = 0;

    public static float airshipMaxHealth = 100;
    public static float airshipCurrentHealth = 100;
    public static float currentSteam = 0;
    public static float maxSteam = 40;

    public static bool crewInGunRoom = false;
    public static bool crewInCockpitRoom = false;
    public static bool crewInThrustRoom = false;
    public static bool crewInGeneratorRoom = false;
    public static bool crewInMainRoom = false;
    public static bool gunRoomOnFire = false;
    public static bool cockpitRoomOnFire = false;
    public static bool generatorRoomOnFire = false;
    public static bool thrustRoomOnFire = false;
    public static bool mainRoomOnFire = false;

    public static List<IslandInfo> islandsInformation = new List<IslandInfo>();
    public static bool createdMap = false;
    public static bool specialBattle = false;

    public static int enemyGunBroken = 0;
    public static bool enemyGunHit = false;
    public static bool battlePause = false;
    public static float enemyShipCurrentHealth;

    public static int howManyNewCrew = 2;


    public static List<BigMapInfo> BigMapInformation = new List<BigMapInfo>();
    public static bool savedBigMap = false;
    public static IslandInfo Exit;

    public static bool showSituationEndingText = false;
    public static string situationEndingText = "";
    public static bool showMapTargetEndingText = false;
    public static string mapTargetEndingText = "";

    public static List<string> randomEncounters; //all the random encounters
    public static string[] randomEnc;
    //public static List<TextAsset> randomEncounters; //all the random encounters


    public static float RedLinePictureX = -0f;
    public static bool badBttleStart = false;

    public static float speedrunSavedTime = 0;


    /// <summary>
    /// When the gameobject is created.
    /// </summary>
    void Awake()
    {
        //Add every random encounter file to the list that keeps track of which random encounters we have encountered.
        ReadAllRandomEncounterFiles();

        //Make it so that this script is available in every scene by not destroying the gameobject when a new scene is loaded
        DontDestroyOnLoad(transform.gameObject);
    }

    public static void ReadAllRandomEncounterFiles()
    {
        //read the txt files as TextAssets because the files inside resources are hidden during build
        Object[] texts = Resources.LoadAll("Situations");
        randomEncounters = new List<string>();
        foreach (var t in texts)
        {
            TextAsset tex = (TextAsset)t;
            string textDialogue = tex.text;
            randomEncounters.Add(textDialogue);
        }
    }

    public static void ResetAirshipStats()
    {
        gunRoomLevel = 1;
        gunRoomSteam = 10;
        gunRoomCurrentSteam = 10;
        gunRoomHp = 10;
        cockpitRoomLevel = 1;
        cockpitRoomSteam = 10;
        cockpitRoomCurrentSteam = 10;
        cockpitRoomHp = 10;
        thrustRoomLevel = 1;
        thrustRoomSteam = 10;
        thrustRoomCurrentSteam = 10;
        thrustRoomHp = 10;
        generatorRoomLevel = 30;
        generatorRoomSteam = 0;
        generatorRoomCurrentSteam = 10;
        generatorRoomHp = 10;
        mainRoomHp = 10;

        RedLinePictureX = 0f;

        howManyNewCrew = 2;
        credits = 200;
        gas = 10;
        ammo1 = 0;
        ammo2 = 0;

        airshipMaxHealth = 100;
        airshipCurrentHealth = 100;
        currentSteam = 0;
        maxSteam = 40;

        crewInGunRoom = false;
        crewInCockpitRoom = false;
        crewInThrustRoom = false;
        crewInGeneratorRoom = false;
        crewInMainRoom = false;
        gunRoomOnFire = false;
        cockpitRoomOnFire = false;
        generatorRoomOnFire = false;
        thrustRoomOnFire = false;
        mainRoomOnFire = false;

        islandsInformation = new List<IslandInfo>();
        createdMap = false;
        specialBattle = false;
        BigMapInformation = new List<BigMapInfo>();
        savedBigMap = false;
        Exit = null;

        showSituationEndingText = false;
        situationEndingText = "";
        showMapTargetEndingText = false;
        mapTargetEndingText = "";

        randomEncounters = new List<string>();
        //randomEncounters = new List<TextAsset>();

        badBttleStart = false;

        speedrunSavedTime = 0;

        ReadAllRandomEncounterFiles();
    }
}