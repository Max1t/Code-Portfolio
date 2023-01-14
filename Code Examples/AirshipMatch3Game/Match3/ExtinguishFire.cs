using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public GameObject extinguishParticlesPrefab;
    public GameObject repairParticlesPrefab;
    private GameObject extinguishParticlesInstance;
    private GameObject repairParticlesInstance;

    private RoomHpController roomHpController;

    // Start is called before the first frame update
    void Start()
    {
        roomHpController = FindObjectOfType<RoomHpController>();
        extinguishParticlesInstance = Instantiate(extinguishParticlesPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z-8), Quaternion.identity);
        repairParticlesInstance = Instantiate(repairParticlesPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 8), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(extinguishParticlesInstance != null)
            extinguishParticlesInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);
        repairParticlesInstance.transform.position = new Vector3(transform.position.x, transform.position.y+0.25f, transform.position.z - 8);
    }

    private void OnTriggerStay(Collider other)
    {
        if (extinguishParticlesInstance == null)
            return;
        if(other.CompareTag("Gun"))
        {
            if(AirshipStats.gunRoomOnFire)
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
                //room not on fire but has taken damage so we let the gamecontroller know the room is being repaired
                if(AirshipStats.gunRoomHp < 10)
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Play();
                    roomHpController.repairingGunRoom = true;
                }
                else
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Stop();
                    roomHpController.repairingGunRoom = false;
                }
            }
        }
        else if (other.CompareTag("Cockpit"))
        {
            if (AirshipStats.cockpitRoomOnFire)
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
                //room not on fire but has taken damage so we let the gamecontroller know the room is being repaired
                if (AirshipStats.cockpitRoomHp < 10)
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Play();
                    roomHpController.repairingCockpitRoom = true;
                }
                else
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Stop();
                    roomHpController.repairingCockpitRoom = false;
                }
            }
        }
        else if (other.CompareTag("Generator"))
        {
            if (AirshipStats.generatorRoomOnFire)
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
                //room not on fire but has taken damage so we let the gamecontroller know the room is being repaired
                if (AirshipStats.generatorRoomHp < 10)
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Play();
                    roomHpController.repairingGeneratorRoom = true;
                }
                else
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Stop();
                    roomHpController.repairingGeneratorRoom = false;
                }
            }
        }
        else if (other.CompareTag("Thrust"))
        {
            if (AirshipStats.thrustRoomOnFire)
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
                //room not on fire but has taken damage so we let the gamecontroller know the room is being repaired
                if (AirshipStats.thrustRoomHp < 10)
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Play();
                    roomHpController.repairingThrustRoom = true;
                }
                else
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Stop();
                    roomHpController.repairingThrustRoom = false;
                }
            }
        }
        else if (other.CompareTag("Main"))
        {
            if (AirshipStats.mainRoomOnFire)
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
                //room not on fire but has taken damage so we let the gamecontroller know the room is being repaired
                if (AirshipStats.mainRoomHp < 10)
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Play();
                    roomHpController.repairingMainRoom = true;
                }
                else
                {
                    repairParticlesInstance.GetComponent<ParticleSystem>().Stop();
                    roomHpController.repairingMainRoom = false;
                }
            }
        }
        else
        {
            extinguishParticlesInstance.GetComponent<ParticleSystem>().Stop();
            repairParticlesInstance.GetComponent<ParticleSystem>().Stop();

            roomHpController.repairingGunRoom = false;
            roomHpController.repairingCockpitRoom = false;
            roomHpController.repairingGeneratorRoom = false;
            roomHpController.repairingThrustRoom = false;
            roomHpController.repairingMainRoom = false;
        }
    }
}
