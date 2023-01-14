using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAndSelectScript : MonoBehaviour
{

    public GameObject selectedGameObject;
    public GameObject movementMarker;
    public LayerMask layer_mask;

    GameObject movementMarkerInstance;

    private void Awake()
    {
        layer_mask = LayerMask.GetMask("ShipNPC");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray raycast = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit, layer_mask))
            {
                if (hit.transform.CompareTag("Player"))
                {

                    if (selectedGameObject == hit.transform.gameObject)
                    {
                        if (movementMarkerInstance != null) movementMarkerInstance.SetActive(false);
                        SpriteRenderer renderer = selectedGameObject.GetComponentInChildren<SpriteRenderer>();
                        renderer.enabled = !renderer.enabled;
                        selectedGameObject = null;
                    }

                    else if (selectedGameObject == null)
                    {
                        if (movementMarkerInstance != null) movementMarkerInstance.SetActive(false);
                        selectedGameObject = hit.transform.gameObject;
                        SpriteRenderer renderer = selectedGameObject.GetComponentInChildren<SpriteRenderer>();
                        renderer.enabled = !renderer.enabled;
                    }

                    else if (selectedGameObject != null)
                    {
                        if (movementMarkerInstance != null) movementMarkerInstance.SetActive(false);
                        SpriteRenderer renderer = selectedGameObject.GetComponentInChildren<SpriteRenderer>();
                        renderer.enabled = !renderer.enabled;
                        selectedGameObject = hit.transform.gameObject;
                        renderer = selectedGameObject.GetComponentInChildren<SpriteRenderer>();
                        renderer.enabled = !renderer.enabled;
                    }
                }



                else if (selectedGameObject != null)
                {
                    selectedGameObject.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    if (movementMarkerInstance == null)
                        movementMarkerInstance = Instantiate(movementMarker, hit.point, Quaternion.identity);
                    else
                    {
                        movementMarkerInstance.SetActive(true);
                        movementMarkerInstance.transform.position = hit.point;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedGameObject != null)
            {
                SpriteRenderer renderer = selectedGameObject.GetComponentInChildren<SpriteRenderer>();
                renderer.enabled = !renderer.enabled;
                movementMarkerInstance.SetActive(false);
                selectedGameObject = null;
            }
        }
    }
}
