using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    public Transform point1Mirror;
    public Transform point2Mirror;

    public GameObject Trail;

    private float t = 0;

    public bool ready = true;


    private float numPoints = 50;
    private Vector3[] positions = new Vector3[50];

    [ContextMenu("Test")]
    public void Test()
    {
        StartCoroutine(AnimateCubicCurve(2.5f));
    }

    public void Animate(float time, bool mirror)
    {
        if(!mirror) StartCoroutine(AnimateCubicCurve(time));
        if (mirror) StartCoroutine(AnimateCubicCurveMirror(time));
    }

    IEnumerator AnimateCubicCurve(float timeToTarget)
    {   
        ready = false;
        while (t < 1)
        {
            t += Time.deltaTime / timeToTarget;
            Trail.transform.position = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            yield return null;
        }
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Trail.transform.position = point0.transform.position;
        t = 0;
        ready = true;
        yield return null;
    }

    IEnumerator AnimateCubicCurveMirror(float timeToTarget)
    {
        ready = false;
        while (t < 1)
        {
            t += Time.deltaTime / timeToTarget;
            Trail.transform.position = CalculateCubicBezierPoint(t, point0.position, point1Mirror.position, point2Mirror.position, point3.position);
            yield return null;
        }
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Trail.transform.position = point0.transform.position;
        t = 0;
        ready = true;
        yield return null;
    }


    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawCubicCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        }
        lineRenderer.SetPositions(positions);
    }


    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // return = (1-t)2P0 + 2(1-t)tP1 + t2P2 , 0 < t < 1
        //            uu           u        tt
        //        uu* p0 + 2 * u * t * p1 + tt* p2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0 + 2 * u * t * p1 + tt * p2;
        return p;
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // return  = (1-t)3P0 + 3(1-t)2tP1 + 3(1-t)t2P2 + t3P3
        //             uuu         uu           u        
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;
        return p;
    }


}
