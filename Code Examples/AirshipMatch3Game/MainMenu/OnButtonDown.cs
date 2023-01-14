using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonDown : MonoBehaviour
{
    private AirshipStats airshipStats;
    private GuiController guiControl;
    // Start is called before the first frame update
    void Start()
    {
        airshipStats = FindObjectOfType<AirshipStats>();
        guiControl = FindObjectOfType<GuiController>();

        //if we return from game over back to main menu we need to destroy the original "don't destroy on load" gameobject
        if (guiControl != null)
        {
            guiControl.dead = false;
        }
        if (airshipStats != null)
        {
            AirshipStats.createdMap = false;
            Destroy(airshipStats.gameObject);
        }

    }

    // Update is called once per frames
    void Update()
    {

    }

    public void OnStartButtonDown()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.dat")) //start a new game so delete old save
            File.Delete(Application.persistentDataPath + "/savedGame.dat");
        SceneManager.LoadScene("Preload");
    }

    public void OnLoadButtonDown()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
            SceneManager.LoadScene("Preload");
    }

    public void OnLeaderboardButtonDown()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
