using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    public static AudioManager instance;

    public Sound newclip;
    // Start is called before the first frame update
    void Awake()
    {

       
            if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        
            DontDestroyOnLoad(gameObject);

        
    
    









        foreach (Sound s in sounds)
        {
            //  s.volume = vol.value;
            s.source = gameObject.AddComponent<AudioSource>();

            if (AirshipStats.battleMusicOn)
            {
                if(s.name == "Main_Music")
                {
                    s.volume = 0;
                }
            }
            s.source.clip = s.clip;
            s.source.volume = AirshipStats.soundVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //  volume = FindObjectOfType<Slider>();
        if (AirshipStats.battleMusicOn == false)
        {
            Play("Main_Music");


        }
        else
        {
            Play("Battle");
        }
      
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void ChangeVolume(Slider vol)
    {
        foreach (Sound s in sounds)
        {
            AirshipStats.soundVolume = vol.value;
        }
    }

    public void turnMusicOff()
    {
        if (instance != null)
        {
            
            Destroy(this.gameObject);
            AudioManager.instance = null;
        }
    }

    public void Update()
    {
        foreach (Sound s in sounds)
        {

            s.source.volume = AirshipStats.soundVolume;
        }


    /*    if (AirshipStats.battleMusicOn == true)
        {
            foreach(Sound s in sounds)
            {
                if(s.source.name == "Main_Music")
                {
                    s.source.volume = 0;
                   
                }
            }
           // Play("Battle");

        }*/
    }
}







