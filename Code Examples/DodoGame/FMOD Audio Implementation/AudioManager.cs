
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;
    public AudioData _data;

    EventInstance _atmoInstance;
    EventInstance _musicInstance;

    [Range(0, 1)]
    [SerializeField]
    private float _masterVolume = 1f;
    Bus masterBus;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(manager);
        }
        else if (manager != this)
        {
            Destroy(this);
        }

        StartMusic();
    }

    void Start()
    {
        masterBus = RuntimeManager.GetBus("Bus:/");
        _masterVolume = Settings.MasterVolume;
        masterBus.setVolume(_masterVolume);
    }

    private void OnValidate()
    {
        masterBus.setVolume(_masterVolume);
    }

    public void OnValueChanged(Slider slider)
    {
        _masterVolume = slider.value;
        masterBus.setVolume(_masterVolume);
    }

    /*private void OnEnable()
    {
        StartAtmo();
        StartMusic();
    }*/

    public void OnStartGame()
    {
        StartAtmo();
    }

    public void OnStopGame()
    {
        StopAtmo();
    }

    private void StopAllSounds()
    {
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void StopAtmo()
    {
        _atmoInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _atmoInstance.release();
    }

    public void PauseAtmoSmooth()
    {
        _atmoInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void ResumeAtmo()
    {
        _atmoInstance.start();
    }

    public void StartAtmo()
    {
        _atmoInstance = RuntimeManager.CreateInstance(_data.Atmosphere.Atmo);
        _atmoInstance.start();
    }

    public void StartMusic()
    {
        _musicInstance = RuntimeManager.CreateInstance(_data.Music.Music);
        _musicInstance.start();
    }

    /// <summary>
    /// changes music.
    /// </summary>
    /// <param name="val">0 = normal music, 2 = raptor rush music</param>
    public void ChangeMusic(float val)
    {
        _musicInstance.setParameterByName("MUSIC_TRANS", val);
    }

    public void StopMusic()
    {
        _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _musicInstance.release();
    }

    public void PlayOneShot(string audio, Vector3 position)
    {
        RuntimeManager.PlayOneShot(audio, position);
    }

    /// <summary>
    /// Creates new instance and starts it immediately
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public EventInstance NewLoopSound(string audio, Vector3 position)
    {
        EventInstance audioInstance = RuntimeManager.CreateInstance(audio);
        audioInstance.start();
        return audioInstance;
    }

    public void StartSound(EventInstance instance)
    {
        instance.start();
    }

    public void PauseSound(EventInstance instance)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopSound(EventInstance instance)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
    }
}
