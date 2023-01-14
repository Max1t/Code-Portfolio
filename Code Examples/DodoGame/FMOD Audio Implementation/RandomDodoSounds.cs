using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDodoSounds : MonoBehaviour
{

    [FMODUnity.EventRef] // This allows us to use the search function to fill in the path string below
    public string eventPath;

    private void OnEnable()
    {
        StartCoroutine(PlayRandomDodoEvent());
    }


    private IEnumerator PlayRandomDodoEvent()
    {
        while (true)
        {

            FMOD.Studio.EventInstance randomDodoSoundEvent = FMODUnity.RuntimeManager.CreateInstance(eventPath);

            randomDodoSoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            randomDodoSoundEvent.start();
            randomDodoSoundEvent.release();

            float interval = Random.Range(1f, 3f);

            yield return new WaitForSeconds(interval);
        }
    }
}
