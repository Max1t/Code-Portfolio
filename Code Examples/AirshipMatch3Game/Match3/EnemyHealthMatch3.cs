using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyHealthMatch3 : MonoBehaviour
{

    public float healthLerpTime = 2f, currentHealthLerpTime;
    public float timer = 0f;
    public float maxHealth, currentHealth, previousHealth;

    public ShipInfoUI shipInfoUI;
    public Image healthFill;
    public TextMeshProUGUI healthText;
    public EnemyTouch enemyTouch;
    public GameObject enemyShip;

    //particle system effets to show the player that he got credits/ammo/repairs...
    public GameObject CreditsEffectPrefab;
    public GameObject RepairEffectPrefab;
    public GameObject AmmoEffectPrefab;




    // Start is called before the first frame update
    void Start()
    {

        int rng = Random.Range(0, 6);
        if (rng == 0) maxHealth = 1000;
        if (rng == 1) maxHealth = 1500;
        if (rng == 2) maxHealth = 2000;
        if (rng == 3) maxHealth = 2500;
        if (rng == 4) maxHealth = 3000;
        if (rng == 5) maxHealth = 3500;
        AirshipStats.enemyGunBroken = 0;
        AirshipStats.battlePause = false;
        enemyTouch = FindObjectOfType<EnemyTouch>();
        shipInfoUI = FindObjectOfType<ShipInfoUI>();
        currentHealth = maxHealth;
        AirshipStats.enemyShipCurrentHealth = currentHealth;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        LerpHealth();
        if (AirshipStats.enemyShipCurrentHealth <= 0)
        {
            AirshipStats.battlePause = true;
            // AirshipStats.ammo1 += 1;
        }
        if (AirshipStats.airshipCurrentHealth <= 0)
        {
            AirshipStats.battlePause = true;
        }
    }

    public void TakeDamage(float damageTaken)
    {
        if (AirshipStats.enemyGunHit == false)
        {
            if (AirshipStats.crewInGunRoom)
                currentHealth -= (damageTaken + AirshipStats.gunRoomCurrentSteam + 10); //Bonus damage from crew. Damage is higher for each gun room level that is using steam is
            else
                currentHealth -= (damageTaken + AirshipStats.gunRoomCurrentSteam); //damage is higher for each gun room level that is using steam is

            previousHealth = healthFill.fillAmount * maxHealth;
            currentHealthLerpTime = 0;
            if (currentHealth <= 0)
            {

                AirshipStats.battlePause = true;

                if (AirshipStats.specialBattle)
                {
                    AirshipStats.battleMusicOn = false;
                    AudioManager.instance.turnMusicOff();
                    SceneManager.LoadScene("Leaderboard");
                }
                if (AirshipStats.showSituationEndingText)
                {
                    AirshipStats.battleMusicOn = false;
                    AudioManager.instance.turnMusicOff();
                    SceneManager.LoadScene("Airship0220");
                }

                shipInfoUI.FlyButtonFill(); //set the fly button to full
                AirshipStats.credits += 400;
                AirshipStats.ammo1 += 1;
                Destroy(Instantiate(CreditsEffectPrefab, new Vector3(-19.9f, 10.1f, -4.9f), Quaternion.identity), 6);

                Destroy(enemyShip);
                Destroy(gameObject);
                currentHealth = 0;
            }
        }

        if (AirshipStats.enemyGunHit == true)
        {
            enemyTouch.GetComponent<EnemyTouch>().damageToGun(damageTaken);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        healthText.text = currentHealth.ToString();
    }

    private void LerpHealth()                           // Health Bar animaatio
    {
        if (currentHealthLerpTime > healthLerpTime)
        {
            previousHealth = currentHealth;
            currentHealthLerpTime = 0;
        }
        else
        {
            currentHealthLerpTime += Time.deltaTime;
        }
        float t = currentHealthLerpTime / healthLerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        healthFill.fillAmount = Mathf.Lerp(previousHealth / maxHealth, currentHealth / maxHealth, t);
    }
}