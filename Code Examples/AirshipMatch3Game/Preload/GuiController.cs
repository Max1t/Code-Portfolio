using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiController : MonoBehaviour
{
    private bool gui = false;
    public bool dead = false;

    /// <summary>
    /// Called once per frame. Checks if ESC is pressed.
    /// </summary>
    void Update()
    {
        //This gameobject is not destroyed so F10 will close the game in every situation
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AirshipStats.ResetAirshipStats();
            dead = false;
            gui = false;
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                gui = true;
            }
            else
            {
                Time.timeScale = 1;
                gui = false;
            }
        }
        if(AirshipStats.airshipCurrentHealth <= 0 && !dead)
        {
            dead = true;
            Time.timeScale = 0;

            //we lost so delete the autosave to get ready for another run
            if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
            {
                File.Delete(Application.persistentDataPath + "/savedGame.dat");
            }
        }
    }

    private void OnGUI()
    {
        if (gui)
        {
            GUIStyle newStyle = new GUIStyle();
            newStyle.fontSize = 100;
            GUI.TextArea(new Rect(Screen.width / 2.5f, Screen.height / 4, Screen.width / 25, Screen.height / 40), "PAUSED", newStyle);
        }
        if (dead)
        {
            GUIStyle newStyle = new GUIStyle();
            newStyle.fontSize = 100;
            GUI.TextArea(new Rect(Screen.width / 2.5f, Screen.height / 4, Screen.width / 25, Screen.height / 40), "YOU LOST", newStyle);
            //if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height / 2, Screen.width / 10, Screen.height / 20), "Red", newStyle))
            //SceneManager.LoadScene(0);
            if (GUI.Button(new Rect(Screen.width / 2.2f, Screen.height / 3, Screen.width / 10, Screen.height / 20), "Main Menu"))
            {
                dead = false;
                gui = false;
                Time.timeScale = 1;
                AirshipStats.ResetAirshipStats();
                SceneManager.LoadScene(0);
            }
        }
    }

}
