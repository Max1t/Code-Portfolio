using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuiTesting : MonoBehaviour
{
    public ResourcesScriptUI resources;
    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 32, Screen.height / 2, Screen.width / 10, Screen.height / 20), "-Health"))
            resources.TakeDamage(30);

        if (GUI.Button(new Rect(Screen.width / 32, Screen.height / 2 + 50, Screen.width / 10, Screen.height / 20), "-Steam"))
            resources.UseSteam(30);

        if (GUI.Button(new Rect(Screen.width / 32, Screen.height / 2 + 100, Screen.width / 10, Screen.height / 20), "-Health∞ "))
            StartCoroutine("Test");
    }
*/
    IEnumerator Test()
    {
        while (true)
        {
            resources.TakeDamage(2);
            yield return new WaitForSecondsRealtime(0.5f);
        }

    }
}
