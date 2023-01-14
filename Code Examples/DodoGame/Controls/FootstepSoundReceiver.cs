using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootstepSoundReceiver : MonoBehaviour
{
    NavMeshAgent _navAgent;

    [SerializeField] private int NavMeshAreaWalkable;
    [SerializeField] private int NavMeshAreaWater;

    void Awake()
    {
        NavMeshAreaWalkable = 1 << NavMesh.GetAreaFromName("Walkable");
        NavMeshAreaWater = 1 << NavMesh.GetAreaFromName("Walkable");
    }

    public void PlayFootstep()
    {
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas);

        if (hit.mask == NavMeshAreaWalkable)
        {
            AudioManager.manager.PlayOneShot(AudioManager.manager._data.Footsteps.Player, transform.position);
        }
        else
        {
            AudioManager.manager.PlayOneShot(AudioManager.manager._data.Footsteps.Player_Water, transform.position);
        }
    }
}



