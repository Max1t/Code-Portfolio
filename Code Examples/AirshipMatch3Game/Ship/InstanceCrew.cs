using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstanceCrew : MonoBehaviour
{
    public GameObject blue;
    public GameObject red;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;



    public Transform crewParent;

    public float agentSpeed;


    private void Awake()
    {
        AirshipStats.battleMusicOn = true;
        AudioManager.instance.Play("Battle"); // Weird place to put this but ok ;>
    }
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


}
