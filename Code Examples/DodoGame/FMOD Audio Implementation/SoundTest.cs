using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.manager.PlayOneShot(AudioManager.manager._data.Vocals.Dodo_Idle, transform.position);
    }
}
