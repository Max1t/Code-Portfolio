using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonLeaderboard : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
