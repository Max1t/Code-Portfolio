using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLines : MonoBehaviour
{

    public List<Transform> IslandList;
    public Transform ShipLocation;



    public void GetIslands()
    {
        foreach (Transform Island in GameObject.Find("IslandsParent").transform)
        {
            if (Island.tag == "Ship")
            {
                ShipLocation = Island;
            }
            else IslandList.Add(Island);
        }
    }

    public IEnumerator SetLines()
    {
        yield return new WaitForFixedUpdate();
        foreach (Transform Island in IslandList)
        {
            if (Island.GetComponent<NextMap>().searchNext == true)
            {
                ShipLocation = Island.transform;
                break;
            }
        }
        foreach (Transform Island in IslandList)
        {
            if (Island.GetComponent<NextMap>().canGo == true)
            {
                LineRenderer lineRenderer = Island.GetComponent<LineRenderer>();
                lineRenderer.enabled = !lineRenderer.enabled;
                lineRenderer.SetPosition(0, ShipLocation.transform.position);
                lineRenderer.SetPosition(1, Island.transform.position);
                //float distance = Vector3.Distance(Island.position, ShipLocation.position);
                lineRenderer.material.SetTextureScale("_MainTex", new Vector2(2 * 3, 1));
            }
        }
    }
}
