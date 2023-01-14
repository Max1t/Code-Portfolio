using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstanceShipScene : MonoBehaviour
{

    public GameObject blue;
    public GameObject red;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;

    public Transform crewParent;
    public float agentSpeed;

    public bool shipScene;

    // Start is called before the first frame update
    void Start()
    {
        if (AirshipStats.howManyNewCrew >= 1)
        {
            GameObject Instance = (GameObject)Instantiate(red, pos1.position, transform.rotation, crewParent);
            Instance.GetComponent<NavMeshAgent>().speed = agentSpeed;

        }

        if (AirshipStats.howManyNewCrew >= 2)
        {
            GameObject Instance = (GameObject)Instantiate(blue, pos2.position, transform.rotation, crewParent);
            Instance.GetComponent<NavMeshAgent>().speed = agentSpeed;

        }
        if (AirshipStats.howManyNewCrew >= 3)
        {
            GameObject Instance = (GameObject)Instantiate(red, pos3.position, transform.rotation, crewParent);
            Instance.GetComponent<NavMeshAgent>().speed = agentSpeed;

        }

        if (AirshipStats.howManyNewCrew >= 4)
        {
            GameObject Instance = (GameObject)Instantiate(blue, pos4.position, transform.rotation, crewParent);
            Instance.GetComponent<NavMeshAgent>().speed = agentSpeed;

        }
    }


    public void InstanceTheCrewFirst()
    {
        if (AirshipStats.howManyNewCrew == 1)
        {
            Vector3 pos = new Vector3(5f, 17.2f, 27f);
            GameObject Instance = (GameObject)Instantiate(red, pos, transform.rotation, crewParent);
        }

        if (AirshipStats.howManyNewCrew == 2)
        {

            Vector3 pos = new Vector3(1.30f, 17.2f, 27f);
            GameObject Instance = (GameObject)Instantiate(blue, pos, transform.rotation, crewParent);


        }
        if (AirshipStats.howManyNewCrew == 3)
        {

            Vector3 pos = new Vector3(3f, 16f, 27f);
            GameObject Instance = (GameObject)Instantiate(red, pos, transform.rotation, crewParent);


        }

        if (AirshipStats.howManyNewCrew == 4)
        {

            Vector3 pos = new Vector3(3f, 17f, 27f);
            GameObject Instance = (GameObject)Instantiate(blue, pos, transform.rotation, crewParent);


        }
    }
}