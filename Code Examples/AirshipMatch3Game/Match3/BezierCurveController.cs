using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveController : MonoBehaviour
{

    public static BezierCurveController instance;

    public Transform curveParent;
    public GameObject curve;

    public Transform ship;

    public Transform bluePosition;
    public Transform redPosition;
    public Transform greenPosition;
    public Transform purplePosition;
    public Transform yellowPosition;

    public Transform enemyShip;

    public List<GameObject> curvePool;
    public int poolAmount;
    public bool expand;


    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        curvePool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject instance = Instantiate(curve, Vector3.zero, Quaternion.identity, curveParent);
            instance.SetActive(false);
            curvePool.Add(instance);
        }

        //enemyShip = GameObject.Find("")
    }

    /// <summary>
    /// Get text object from pool
    /// </summary>
    public GameObject GetFromPool()
    {
        foreach (GameObject curve in curvePool)
        {
            if(curve != null) {
            if (!curve.activeInHierarchy && curve.GetComponent<BezierCurve>().ready)
            {
                return curve;
            }
            }

        }
        if (expand)
        {
            GameObject instance = Instantiate(curve, Vector3.zero, Quaternion.identity, curveParent);
            instance.SetActive(false);
            curvePool.Add(instance);
            return instance;
        }
        else return null;
    }


    public void TriggerEffect(Transform tr)
    {
        float time = 1.5f;
        GameObject instance = GetFromPool();
        instance.transform.position = new Vector3(tr.position.x, tr.position.y, -5);
        string tempTag = tr.tag;
        switch (tempTag)
        {
            case "Blue":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.blue;
                    instance.transform.GetChild(3).transform.position = new Vector3(bluePosition.position.x, bluePosition.position.y, -5);

                    break;
                }
            case "Red":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.red;
                    instance.transform.GetChild(3).transform.position = new Vector3(redPosition.position.x, redPosition.position.y, -5);

                    break;
                }
            case "Green":

                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.green;
                    instance.transform.GetChild(3).transform.position = new Vector3(greenPosition.position.x, greenPosition.position.y, -5);

                    break;
                }
            case "White":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.magenta;
                    instance.transform.GetChild(3).transform.position = new Vector3(purplePosition.position.x, purplePosition.position.y, -5);

                    break;
                }
            case "Violet":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.magenta;
                    instance.transform.GetChild(3).transform.position = new Vector3(purplePosition.position.x, purplePosition.position.y, -5);

                    break;
                }
            case "Yellow":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.yellow;
                    instance.transform.GetChild(3).transform.position = new Vector3(yellowPosition.position.x, yellowPosition.position.y, -5);

                    break;
                }
            case "Damage":
                {
                    instance.transform.GetChild(4).GetComponent<TrailRenderer>().material.color = Color.black;
                    instance.transform.GetChild(3).transform.position = new Vector3(enemyShip.position.x, enemyShip.position.y, -5);
                    time = 0.5f;

                    break;
                }
        }


        instance.SetActive(true);
        if (Random.Range(0, 2) == 1)
            instance.GetComponent<BezierCurve>().Animate(time, true);
        else
        {
            instance.GetComponent<BezierCurve>().Animate(time, false);
        }
    }
}
