using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    public GameObject sprite;
    public ParticleSystem particles;

    private void Start()
    {
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1.5f);
        PlayParticles();
    }

    IEnumerator WaitToStopSprite()
    {
        yield return new WaitForSeconds(0.2f);
        sprite.gameObject.SetActive(false);
    }

    private void PlayParticles()
    {
        
        particles.Emit(9999);
        StartCoroutine(WaitToStopSprite());
    }
}
