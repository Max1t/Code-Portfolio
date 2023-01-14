using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIsBurning : MonoBehaviour
{
    private float duration = 10f; //how often fire burns the ship
    private float gunFireHp = 8;
    private float cockpitFireHp = 8;
    private float generatorFireHp = 8;
    private float thrustFireHp = 8;
    private float mainFireHp = 8;

    private bool startedBurningGunRoom = false;
    private bool startedBurningCockpitRoom = false;
    private bool startedBurningGeneratorRoom = false;
    private bool startedBurningThrustRoom = false;
    private bool startedBurningMainRoom = false;
    
    public ResourcesScriptUI resources;
    public Matches matches;

    // Start is called before the first frame update
    void Start()
    {
        resources = FindObjectOfType<ResourcesScriptUI>();
        matches = FindObjectOfType<Matches>();
    }

    private void RoomsOnFireCheck()
    {
        //stop the fire in the gun room
        if(startedBurningGunRoom && AirshipStats.crewInGunRoom)
        {
            if(gunFireHp > 0)
            {
                gunFireHp -= Time.deltaTime;
            }
            else
            {
                AirshipStats.gunRoomOnFire = false;
                matches.StopBurningFire("gun");
                startedBurningGunRoom = false;
                gunFireHp = 8;
            }
        }
        //cockpit fire stop
        if (startedBurningCockpitRoom && AirshipStats.crewInCockpitRoom)
        {
            if (cockpitFireHp > 0)
            {
                cockpitFireHp -= Time.deltaTime;
            }
            else
            {
                AirshipStats.cockpitRoomOnFire = false;
                matches.StopBurningFire("cockpit");
                startedBurningCockpitRoom = false;
                cockpitFireHp = 8;
            }
        }

        if (startedBurningGeneratorRoom && AirshipStats.crewInGeneratorRoom)
        {
            if (generatorFireHp > 0)
            {
                generatorFireHp -= Time.deltaTime;
            }
            else
            {
                AirshipStats.generatorRoomOnFire = false;
                matches.StopBurningFire("generator");
                startedBurningGeneratorRoom = false;
                generatorFireHp = 8;
            }
        }

        if (startedBurningThrustRoom && AirshipStats.crewInThrustRoom)
        {
            if (thrustFireHp > 0)
            {
                thrustFireHp -= Time.deltaTime;
            }
            else
            {
                AirshipStats.thrustRoomOnFire = false;
                matches.StopBurningFire("thrust");
                startedBurningThrustRoom = false;
                thrustFireHp = 8;
            }
        }

        if (startedBurningMainRoom && AirshipStats.crewInMainRoom)
        {
            if (mainFireHp > 0)
            {
                mainFireHp -= Time.deltaTime;
            }
            else
            {
                AirshipStats.mainRoomOnFire = false;
                matches.StopBurningFire("main");
                startedBurningMainRoom = false;
                mainFireHp = 8;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AirshipStats.gunRoomOnFire && !startedBurningGunRoom)
        {
            startedBurningGunRoom = true;
            StartCoroutine(WaitGunRoomFire());
        }
        if (AirshipStats.cockpitRoomOnFire && !startedBurningCockpitRoom)
        {
            startedBurningCockpitRoom = true;
            StartCoroutine(WaitCockpitRoomFire());
        }
        if (AirshipStats.generatorRoomOnFire && !startedBurningGeneratorRoom)
        {
            startedBurningGeneratorRoom = true;
            StartCoroutine(WaitGeneratorRoomFire());
        }
        if (AirshipStats.thrustRoomOnFire && !startedBurningThrustRoom)
        {
            startedBurningThrustRoom = true;
            StartCoroutine(WaitThrustRoomFire());
        }
        if (AirshipStats.mainRoomOnFire && !startedBurningMainRoom)
        {
            startedBurningMainRoom = true;
            StartCoroutine(WaitMainRoomFire());
        }

        RoomsOnFireCheck();
    }

    private void BurnTheShip()
    {
        resources.TakeDamage(5);
    }

    IEnumerator WaitGunRoomFire()
    {
        Debug.Log("Started burning gun room at: " + Time.time);
        yield return new WaitForSeconds(duration);
        if(AirshipStats.gunRoomOnFire)
        {
            Debug.Log("gun burned: " + Time.time);
            BurnTheShip();
            StartCoroutine(WaitGunRoomFire());
        }
    }

    IEnumerator WaitCockpitRoomFire()
    {
        Debug.Log("Started burning cockpit at: " + Time.time);
        yield return new WaitForSeconds(duration);
        if (AirshipStats.cockpitRoomOnFire)
        {
            Debug.Log("cockpit burned: " + Time.time);
            BurnTheShip();
            StartCoroutine(WaitCockpitRoomFire());
        }
    }

    IEnumerator WaitGeneratorRoomFire()
    {
        Debug.Log("Started burning generator at: " + Time.time);
        yield return new WaitForSeconds(duration);
        if (AirshipStats.generatorRoomOnFire)
        {
            Debug.Log("generator burned: " + Time.time);
            BurnTheShip();
            StartCoroutine(WaitGeneratorRoomFire());
        }
    }

    IEnumerator WaitThrustRoomFire()
    {
        Debug.Log("Started burning thrust room at: " + Time.time);
        yield return new WaitForSeconds(duration);
        if (AirshipStats.thrustRoomOnFire)
        {
            Debug.Log("thrust burned: " + Time.time);
            BurnTheShip();
            StartCoroutine(WaitThrustRoomFire());
        }
    }

    IEnumerator WaitMainRoomFire()
    {
        Debug.Log("Started burning main room at: " + Time.time);
        yield return new WaitForSeconds(duration);
        if (AirshipStats.mainRoomOnFire)
        {
            Debug.Log("main burned: " + Time.time);
            BurnTheShip();
            StartCoroutine(WaitMainRoomFire());
        }
    }
    
}
