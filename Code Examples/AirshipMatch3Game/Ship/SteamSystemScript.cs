using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamSystemScript : MonoBehaviour
{
    public GameObject gunRoom;
    public GameObject cockpitRoom;
    public GameObject thrustRoom;
    public GameObject generatorRoom;
    public GameObject mainRoom;

    // Update is called once per frame
    void Update()
    {
        if(gunRoom.GetComponent<RoomCollider>().playerInRoom)
        {
            AirshipStats.crewInGunRoom = true;
        }
        else
        {
            AirshipStats.crewInGunRoom = false;
        }
        if (cockpitRoom.GetComponent<RoomCollider>().playerInRoom)
        {
            AirshipStats.crewInCockpitRoom = true;
        }
        else
        {
            AirshipStats.crewInCockpitRoom = false;
        }
        if (thrustRoom.GetComponent<RoomCollider>().playerInRoom)
        {
            AirshipStats.crewInThrustRoom = true;
        }
        else
        {
            AirshipStats.crewInThrustRoom = false;
        }
        if (generatorRoom.GetComponent<RoomCollider>().playerInRoom)
        {
            AirshipStats.crewInGeneratorRoom = true;
        }
        else
        {
            AirshipStats.crewInGeneratorRoom = false;
        }
        if(mainRoom != null)
        {
            if (mainRoom.GetComponent<RoomCollider>().playerInRoom)
            {
                AirshipStats.crewInMainRoom = true;
            }
            else
            {
                AirshipStats.crewInMainRoom = false;
            }
        }
    }
}
