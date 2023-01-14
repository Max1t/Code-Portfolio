using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoad : MonoBehaviour
{
    /// <summary>
    /// When the gameobject is created.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Serialize playerdata and save it in a file.
    /// </summary>
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.dat");
        PlayerData data = new PlayerData();

        data.gunRoomLevel = AirshipStats.gunRoomLevel;
        data.gunRoomCurrentSteam = AirshipStats.gunRoomCurrentSteam; //if the room takes damage this lowers
        data.gunRoomSteam = AirshipStats.gunRoomSteam; //how much steam the current gun room has: 1 level - 10 steam
        data.gunRoomHp = AirshipStats.gunRoomHp;
        data.cockpitRoomLevel = AirshipStats.cockpitRoomLevel;
        data.cockpitRoomCurrentSteam = AirshipStats.cockpitRoomCurrentSteam;
        data.cockpitRoomSteam = AirshipStats.cockpitRoomSteam;
        data.cockpitRoomHp = AirshipStats.cockpitRoomHp;
        data.thrustRoomLevel = AirshipStats.thrustRoomLevel;
        data.thrustRoomCurrentSteam = AirshipStats.thrustRoomCurrentSteam;
        data.thrustRoomSteam = AirshipStats.thrustRoomSteam;
        data.thrustRoomHp = AirshipStats.thrustRoomHp;
        data.generatorRoomLevel = AirshipStats.generatorRoomLevel;
        data.generatorRoomCurrentSteam = AirshipStats.generatorRoomCurrentSteam;
        data.generatorRoomSteam = AirshipStats.generatorRoomSteam;
        data.generatorRoomHp = AirshipStats.generatorRoomHp;
        data.mainRoomHp = AirshipStats.mainRoomHp;

        data.credits = AirshipStats.credits;
        data.gas = AirshipStats.gas;

        data.ammo1 = AirshipStats.ammo1;
        data.ammo2 = AirshipStats.ammo2;

        data.airshipMaxHealth = AirshipStats.airshipMaxHealth;
        data.airshipCurrentHealth = AirshipStats.airshipCurrentHealth;
        data.currentSteam = AirshipStats.currentSteam;
        data.maxSteam = AirshipStats.maxSteam;

        data.crewInGunRoom = AirshipStats.crewInGunRoom;
        data.crewInCockpitRoom = AirshipStats.crewInCockpitRoom;
        data.crewInThrustRoom = AirshipStats.crewInThrustRoom;
        data.crewInGeneratorRoom = AirshipStats.crewInGeneratorRoom;
        data.crewInMainRoom = AirshipStats.crewInMainRoom;
        data.gunRoomOnFire = AirshipStats.gunRoomOnFire;
        data.cockpitRoomOnFire = AirshipStats.cockpitRoomOnFire;
        data.generatorRoomOnFire = AirshipStats.generatorRoomOnFire;
        data.thrustRoomOnFire = AirshipStats.thrustRoomOnFire;
        data.mainRoomOnFire = AirshipStats.mainRoomOnFire;

        data.islandsInformation = AirshipStats.islandsInformation;
        data.createdMap = AirshipStats.createdMap;
        data.specialBattle = AirshipStats.specialBattle;

        data.enemyGunBroken = AirshipStats.enemyGunBroken;
        data.enemyGunHit = AirshipStats.enemyGunHit;
        data.battlePause = AirshipStats.battlePause;
        data.enemyShipCurrentHealth = AirshipStats.enemyShipCurrentHealth;

        data.howManyNewCrew = AirshipStats.howManyNewCrew;

        data.BigMapInformation = AirshipStats.BigMapInformation;
        data.savedBigMap = AirshipStats.savedBigMap;
        data.Exit = AirshipStats.Exit;

        data.showSituationEndingText = AirshipStats.showSituationEndingText;
        data.situationEndingText = AirshipStats.situationEndingText;
        data.showMapTargetEndingText = AirshipStats.showMapTargetEndingText;
        data.mapTargetEndingText = AirshipStats.mapTargetEndingText;

        data.randomEncounters = AirshipStats.randomEncounters; //all the random encounters

        data.RedLinePictureX = AirshipStats.RedLinePictureX;
        data.badBttleStart = AirshipStats.badBttleStart;

        data.equippedItems = Inventory.instance.SaveEquippedData();
        data.cargoItems = Inventory.instance.SaveCargoData();

        data.speedrunSavedTime = AirshipStats.speedrunSavedTime + Time.time;

        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Load the serialized data.
    /// </summary>
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
        {
            Debug.Log(Application.persistentDataPath + "/savedGame.dat");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            AirshipStats.gunRoomLevel = data.gunRoomLevel;
            AirshipStats.gunRoomCurrentSteam = data.gunRoomCurrentSteam; //if the room takes damage this lowers
            AirshipStats.gunRoomSteam = data.gunRoomSteam; //how much steam the current gun room has: 1 level - 10 steam
            AirshipStats.gunRoomHp = data.gunRoomHp;
            AirshipStats.cockpitRoomLevel = data.cockpitRoomLevel;
            AirshipStats.cockpitRoomCurrentSteam = data.cockpitRoomCurrentSteam;
            AirshipStats.cockpitRoomSteam = data.cockpitRoomSteam;
            AirshipStats.cockpitRoomHp = data.cockpitRoomHp;
            AirshipStats.thrustRoomLevel = data.thrustRoomLevel;
            AirshipStats.thrustRoomCurrentSteam = data.thrustRoomCurrentSteam;
            AirshipStats.thrustRoomSteam = data.thrustRoomSteam;
            AirshipStats.thrustRoomHp = data.thrustRoomHp;
            AirshipStats.generatorRoomLevel = data.generatorRoomLevel;
            AirshipStats.generatorRoomCurrentSteam = data.generatorRoomCurrentSteam;
            AirshipStats.generatorRoomSteam = data.generatorRoomSteam;
            AirshipStats.generatorRoomHp = data.generatorRoomHp;
            AirshipStats.mainRoomHp = data.mainRoomHp;

            AirshipStats.credits = data.credits;
            AirshipStats.gas = data.gas;

            AirshipStats.ammo1 = data.ammo1;
            AirshipStats.ammo2 = data.ammo2;

            AirshipStats.airshipMaxHealth = data.airshipMaxHealth;
            AirshipStats.airshipCurrentHealth = data.airshipCurrentHealth;
            AirshipStats.currentSteam = data.currentSteam;
            AirshipStats.maxSteam = data.maxSteam;

            AirshipStats.crewInGunRoom = data.crewInGunRoom;
            AirshipStats.crewInCockpitRoom = data.crewInCockpitRoom;
            AirshipStats.crewInThrustRoom = data.crewInThrustRoom;
            AirshipStats.crewInGeneratorRoom = data.crewInGeneratorRoom;
            AirshipStats.crewInMainRoom = data.crewInMainRoom;
            AirshipStats.gunRoomOnFire = data.gunRoomOnFire;
            AirshipStats.cockpitRoomOnFire = data.cockpitRoomOnFire;
            AirshipStats.generatorRoomOnFire = data.generatorRoomOnFire;
            AirshipStats.thrustRoomOnFire = data.thrustRoomOnFire;
            AirshipStats.mainRoomOnFire = data.mainRoomOnFire;

            AirshipStats.islandsInformation = data.islandsInformation;
            AirshipStats.createdMap = data.createdMap;
            AirshipStats.specialBattle = data.specialBattle;

            AirshipStats.enemyGunBroken = data.enemyGunBroken;
            AirshipStats.enemyGunHit = data.enemyGunHit;
            AirshipStats.battlePause = data.battlePause;
            AirshipStats.enemyShipCurrentHealth = data.enemyShipCurrentHealth;

            AirshipStats.howManyNewCrew = data.howManyNewCrew;

            AirshipStats.BigMapInformation = data.BigMapInformation;
            AirshipStats.savedBigMap = data.savedBigMap;
            AirshipStats.Exit = data.Exit;

            AirshipStats.showSituationEndingText = data.showSituationEndingText;
            AirshipStats.situationEndingText = data.situationEndingText;
            AirshipStats.showMapTargetEndingText = data.showMapTargetEndingText;
            AirshipStats.mapTargetEndingText = data.mapTargetEndingText;

            AirshipStats.randomEncounters = data.randomEncounters; //all the random encounters

            AirshipStats.RedLinePictureX = data.RedLinePictureX;
            AirshipStats.badBttleStart = data.badBttleStart;

            Inventory.instance.LoadCargoData(data.cargoItems);
            Inventory.instance.LoadEquippedData(data.equippedItems);

            AirshipStats.speedrunSavedTime = data.speedrunSavedTime;
        }
    }
}

[Serializable]
class PlayerData
{
    public int gunRoomLevel;
    public float gunRoomCurrentSteam; //if the room takes damage this lowers
    public float gunRoomSteam; //how much steam the current gun room has: 1 level - 10 steam
    public float gunRoomHp;
    public int cockpitRoomLevel;
    public float cockpitRoomCurrentSteam;
    public float cockpitRoomSteam;
    public float cockpitRoomHp;
    public int thrustRoomLevel;
    public float thrustRoomCurrentSteam;
    public float thrustRoomSteam;
    public float thrustRoomHp;
    public int generatorRoomLevel;
    public float generatorRoomCurrentSteam;
    public float generatorRoomSteam;
    public float generatorRoomHp;
    public float mainRoomHp;

    public int credits;
    public int gas;

    public int ammo1;
    public int ammo2;

    public float airshipMaxHealth;
    public float airshipCurrentHealth;
    public float currentSteam;
    public float maxSteam;

    public bool crewInGunRoom;
    public bool crewInCockpitRoom;
    public bool crewInThrustRoom;
    public bool crewInGeneratorRoom;
    public bool crewInMainRoom;
    public bool gunRoomOnFire;
    public bool cockpitRoomOnFire;
    public bool generatorRoomOnFire;
    public bool thrustRoomOnFire;
    public bool mainRoomOnFire;

    public List<IslandInfo> islandsInformation = new List<IslandInfo>();
    public bool createdMap;
    public bool specialBattle;

    public int enemyGunBroken;
    public bool enemyGunHit;
    public bool battlePause;
    public float enemyShipCurrentHealth;

    public int howManyNewCrew;

    public List<BigMapInfo> BigMapInformation = new List<BigMapInfo>();
    public bool savedBigMap;
    public IslandInfo Exit;

    public bool showSituationEndingText;
    public string situationEndingText;
    public bool showMapTargetEndingText;
    public string mapTargetEndingText;

    public List<string> randomEncounters; //all the random encounters

    public float RedLinePictureX;
    public bool badBttleStart;

    public int[] equippedItems;
    public int[] cargoItems;

    public float speedrunSavedTime;
}