using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrewAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;


    private void Awake()
    {
        agent.updateRotation = false;
    }
    private void Update()
    {
        if (agent.velocity.x != 0 || agent.velocity.z != 0)
        {
            anim.SetBool("Walking", true);
        }
        else if (anim.GetBool("Walking") == true) anim.SetBool("Walking", false);
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized, Vector3.back);
        }
    }
}
