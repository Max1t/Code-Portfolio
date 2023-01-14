using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private float Mspeed;
    [SerializeField] private bool _moving;
    [SerializeField] private float _movementMagnitude;
    [SerializeField] private NavMeshAgent _navAgent;
    [SerializeField] private bool _canWalk = true;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        Mspeed = PlayerSettings.Interaction.MovementSpeed;
        _canWalk = true;
    }

    private void Update()
    {
        if (_moving)
        {
            _animator.SetFloat("Movement", 1);
        }
        else
        {
            _animator.SetFloat("Movement", 0f);
        }
        if (_navAgent.velocity.magnitude > 0.1f)
        {
            _moving = true;
        }
    }



    // Checking area not needed
    // Can set Navmesharea mask in navmesh agent!!!
    public void Move(Vector2 movement)
    {
        /* if(!_canWalk)
        {
            Debug.Log(movement);
            if (movement != Vector2.zero)
            {
                return;
            }
            _moving = false;
            _canWalk = true;
        } */

        if (movement == Vector2.zero)
        {
            _moving = false;
            return;
        }
        //_navAgent.SamplePathPosition(NavMesh.AllAreas, 0, out NavMeshHit hit);
        //if (hit.mask == 1)
        //{
        Vector2 Movement = Mspeed * Time.deltaTime * -movement;
        transform.Translate(Movement.x, 0f, Movement.y, Space.World);
        if (Movement != Vector2.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(Movement.x, 0f, Movement.y)),
                PlayerSettings.Interaction.RotationSpeed);
        _moving = true;
        // }
        // else
        // {
        //     transform.position -= transform.forward * 0.5f;
        //     _canWalk = false;
        //     _moving = false;
        // }
    }

    public void MoveAgent(Transform pos)
    {
        _navAgent.ResetPath();
        _navAgent.SetDestination(pos.position);
        _navAgent.isStopped = false;
    }

    public void MoveTo(Transform pos)
    {
        transform.position = pos.position;
    }
}
