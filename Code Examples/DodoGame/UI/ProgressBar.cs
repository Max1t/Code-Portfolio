using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private SpriteRenderer __background;
    [SerializeField] private Transform _bar;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Transform _followTransform;
    [SerializeField] private bool _pause = true;
    
    void LateUpdate()
    {
        if (Camera.main != null && _followTransform != null)
        {
            transform.rotation = Camera.main.transform.rotation;
            transform.position = _followTransform.position;
        }
    }

    public void ShowProgressBar(float time, Transform followTarget)
    {
        _followTransform = followTarget;
        StartCoroutine(nameof(RunProgress), time);
    }

    public void StopProgressBar()
    {
        StopCoroutine(nameof(RunProgress));
        StartCoroutine(nameof(DisableProgressBar));
    }

    public void ToggleProggressPause()
    {
        _pause = !_pause;
    }
    
    IEnumerator RunProgress(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            if(_pause)
                currentTime += Time.deltaTime;
            _bar.localScale = new Vector3(Mathf.Lerp(0, 1, (currentTime / time)), 1, 1);
            _sprite.color = Color.Lerp(_startColor, _endColor, (currentTime / time));
            yield return null;
        }
        StartCoroutine(nameof(DisableProgressBar));
    }

    IEnumerator DisableProgressBar()
    {
        yield return new WaitForSeconds(2f);
        _followTransform = null;
        gameObject.SetActive(false);
    }
}