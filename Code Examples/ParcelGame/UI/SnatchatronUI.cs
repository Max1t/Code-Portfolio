using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnatchatronUI : MonoBehaviour
{
    [SerializeField]
    private GameObject snatchatronPopup;

    public void StartTimer(float timeInSeconds)
    {
        Gamemanager.Get.PlayerInventory.Snatchatron = true;
        snatchatronPopup.SetActive(true);
        WaitForSecondsRealtime waitFor = new WaitForSecondsRealtime(timeInSeconds);
        StartCoroutine(Timer(waitFor));
    }

    public IEnumerator Timer(WaitForSecondsRealtime time)
    {
        yield return time;
        Gamemanager.Get.PlayerInventory.Snatchatron = false;
        snatchatronPopup.SetActive(false);

    }
}
