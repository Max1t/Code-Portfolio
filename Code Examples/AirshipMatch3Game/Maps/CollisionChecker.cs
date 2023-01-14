using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool trigger = false;
    public Vector3 colliderPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
        colliderPosition = collision.gameObject.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        trigger = true;
        colliderPosition = collision.gameObject.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }
}
