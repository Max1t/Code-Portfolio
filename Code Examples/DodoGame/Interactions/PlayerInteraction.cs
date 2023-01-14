using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;


public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private Transform _carryPosition;
    [SerializeField] private bool _carrying = false;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Animator _anim;
    public Transform _interactableCarried;
    public GameManager _manager;
    private bool _petting;

    public ControlHandler _control;

    private RaycastHit _hit;

    public List<Transform> Interactables;

    public bool Carrying { get => _carrying; }

    [SerializeField] private int NavMeshAreaWalkable;

    public void ResetPlayerInteraction()
    {
        if (_interactableCarried == null) return;
        Interactables.Clear();
        _petting = false;
        NavMeshHit hit;
        if (_interactableCarried != null)
        {
            if (_interactableCarried.CompareTag("Dodo"))
            {
                NavMesh.SamplePosition(transform.position + transform.forward, out hit, 10f, NavMesh.AllAreas);
            }
            else
            {
                NavMesh.SamplePosition(transform.position + transform.forward, out hit, 10f, NavMeshAreaWalkable);
            }
            _interactableCarried.transform.position = hit.position;
            _interactableCarried = null;
            _carrying = false;
            _anim.SetBool("Carrying", false);
        }
    }

    void Awake()
    {
        NavMeshAreaWalkable = 1 << NavMesh.GetAreaFromName("Walkable");
    }
    private void Start()
    {
        if (_manager == null)
        {
            _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }
    // Moves picked dodo with you
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 15, Color.green, 0.1f);
        if (_interactableCarried != null)
        {
            if (_interactableCarried.GetComponent<StatePatternDodo>() != null)
            {
                if (_interactableCarried.GetComponent<StatePatternDodo>().isDying)
                {
                    _interactableCarried.GetComponent<StatePatternDodo>().playerCarrying = null;
                    _interactableCarried = null;
                    _carrying = false;
                    _anim.SetBool("Carrying", false);
                    return;
                }
            }
            if (Carrying && _interactableCarried.gameObject.activeSelf)
            {
                _interactableCarried.transform.position = _carryPosition.position;
                _interactableCarried.transform.rotation = transform.rotation;
                return;
            }
            if (!_interactableCarried.gameObject.activeSelf)
            {
                DropInteractable();
                _carrying = false;
                _anim.SetBool("Carrying", false);
            }
        }
    }

    public void Interact()
    {
        if (Carrying == false)
        {
            if (Interactables.Any())
            {
                if (Interactables[0].GetComponent<Interactable>().Interact(this))
                {
                    return;
                }
            }
        }
        if (Carrying == true)
        {
            if (Interactables.Any())

                if (Interactables[0].CompareTag("Ark"))

                    if (Interactables[0].GetComponent<Interactable>().Interact(this))
                        return;


            if (_interactableCarried.GetComponent<Interactable>().Interact(this))
            {
                return;
            }
        }
    }

    public void Touch()
    {
        if (!_petting)
        {
            if (Interactables.Any())
            {
                if (Interactables[0].GetComponent<Touchable>().Touch(this))
                {
                    StartCoroutine(Pet());
                    AudioManager.manager.PlayOneShot(AudioManager.manager._data.Interactions.Pet, transform.position);
                }
            }
        }
    }

    IEnumerator Pet()
    {
        _petting = true;
        _anim.SetTrigger("Pet");
        yield return new WaitForSeconds(1.25f);
        _petting = false;
    }


    public void PickUpInteractable(Transform interactable)
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Vocals.Player_Pickup, transform.position);
        _anim.SetTrigger("Lift");
        _anim.SetBool("Carrying", true);
        _carrying = true;
        _interactableCarried = interactable;
        Interactables.Remove(interactable);
    }

    public void DropInteractable()
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Vocals.Player_Putdown, transform.position);
        _anim.SetTrigger("Lower");
        _anim.SetBool("Carrying", false);
        _carrying = false;
        NavMeshHit hit;
        if (_interactableCarried.CompareTag("Dodo"))
        {
            NavMesh.SamplePosition(transform.position + transform.forward, out hit, 10f, NavMesh.AllAreas);
        }
        else
        {
            NavMesh.SamplePosition(transform.position + transform.forward, out hit, 10f, NavMeshAreaWalkable);
        }
        _interactableCarried.transform.position = hit.position;
        _interactableCarried = null;
    }

    public void DropInteractable(Transform dropPosition)
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Vocals.Player_Putdown, transform.position);
        _anim.SetTrigger("Lower");
        _anim.SetBool("Carrying", false);
        _carrying = false;
        _interactableCarried.transform.position = dropPosition.position;
        _interactableCarried = null;
    }

    public void LoseInteractable()
    {
        _interactableCarried = null;
        _anim.SetTrigger("Lower");
        _anim.SetBool("Carrying", false);
        _carrying = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (((_mask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) && other.gameObject != _interactableCarried)
        {
            CheckInteractablesList();
            if (!Interactables.Contains(other.transform))
            {
                Interactables.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((_mask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) && other.gameObject != _interactableCarried)
        {
            Interactables.Remove(other.transform);
        }
    }


    private void CheckInteractablesList()
    {
        List<Transform> toDestroyList = new List<Transform>();
        foreach (Transform interactable in Interactables)
        {
            if (!interactable.gameObject.activeSelf) toDestroyList.Add(interactable);
            if (interactable.gameObject.CompareTag("Dodo"))
            {
                if (interactable.GetComponent<StatePatternDodo>().isDying) toDestroyList.Add(interactable);
            }
        }
        foreach (Transform item in toDestroyList)
        {
            Interactables.Remove(item);
        }
        OrderInteractablesList();
    }

    private void OrderInteractablesList()
    {
        Interactables = Interactables.OrderByDescending(item => item.tag == "Ark")
                                     .ThenByDescending(item => item.tag == "Dodo")
                                     .ToList();
    }

}