using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Back : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void BackToMap()
    {
        SceneManager.LoadScene("Map");
    }


    public void ExitGame()
    {

        Application.Quit();

    }

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {

        audioMixer.SetFloat("volume", volume);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
