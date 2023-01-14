using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Text Mesh Pro

public class ResourcesScriptUI : MonoBehaviour
{

    public float previousHealth;
    public Image healthFill;
    public TextMeshProUGUI healthText;

    public float previousSteam;
    public Image steamFill;
    public TextMeshProUGUI steamText;

    public float healthLerpTime = 2f, steamLerpTime = 2f, currentHealthLerpTime, currentSteamLerpTime;

    public float timer = 0f;

    void Start()
    {
        //AirshipStats.airshipCurrentHealth = AirshipStats.airshipMaxHealth;
        AirshipStats.currentSteam = 0;
        UpdateUI();
    }

    void Update()
    {
        LerpHealth();
        LerpSteam();
    }

    // Healthin muuttaminen
    public void TakeDamage(float damageTaken)
    {
        AirshipStats.airshipCurrentHealth -= damageTaken;
        previousHealth = healthFill.fillAmount * AirshipStats.airshipMaxHealth;
        currentHealthLerpTime = 0;
        if (AirshipStats.airshipCurrentHealth <= 0)
        {
            // ded
            AirshipStats.airshipCurrentHealth = 0;
            AirshipStats.battlePause = true; //ei toimi?
        }
        UpdateUI();
    }

    // Steamin käyttö VIP
    public void UseSteam(float steamUsed)
    {
        AirshipStats.currentSteam -= steamUsed;
        previousSteam = steamFill.fillAmount * AirshipStats.maxSteam;
        currentSteamLerpTime = 0;
        if (AirshipStats.currentSteam < 0)
        {
            //sumffing
            AirshipStats.currentSteam = 0;
        }
        UpdateUI();
    }

    private void LerpHealth()                           // Health Bar animaatio
    {
        if (currentHealthLerpTime > healthLerpTime)
        {
            previousHealth = AirshipStats.airshipCurrentHealth;

            currentHealthLerpTime = 0;
        }
        else
        {
            currentHealthLerpTime += Time.deltaTime;
        }

        float t = currentHealthLerpTime / healthLerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        healthFill.fillAmount = Mathf.Lerp(previousHealth / AirshipStats.airshipMaxHealth, AirshipStats.airshipCurrentHealth / AirshipStats.airshipMaxHealth, t);
    }

    private void LerpSteam()                            // Steam Bar animaatio
    {
        if (currentSteamLerpTime > steamLerpTime)
        {
            previousSteam = AirshipStats.currentSteam;

            currentSteamLerpTime = 0;
        }
        else
        {
            currentSteamLerpTime += Time.deltaTime;
        }

        float t = currentSteamLerpTime / steamLerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        steamFill.fillAmount = Mathf.Lerp(previousSteam / AirshipStats.maxSteam, AirshipStats.currentSteam / AirshipStats.maxSteam, t);
    }

   public void UpdateUI()                                     // Päivittää UI tekstit
    {
        healthText.text = AirshipStats.airshipCurrentHealth.ToString();
        steamText.text = AirshipStats.currentSteam.ToString();
    }

}
