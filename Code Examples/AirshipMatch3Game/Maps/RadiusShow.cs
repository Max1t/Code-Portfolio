using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusShow : MonoBehaviour
{
    public float smallRadius = 0.7f;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallRadius);
    }
}
