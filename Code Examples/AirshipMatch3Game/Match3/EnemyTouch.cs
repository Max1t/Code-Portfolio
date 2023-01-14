using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTouch : MonoBehaviour
{
    public float enemyGunHealthSpecialBattleLeft = 500;
    public float enemyGunHealthSpecialBattRight = 500;
    public float enemyGunHealthleft = 200;
    public float enemyGunHealthright = 200;

    public bool rightGun = false;
    public bool leftGun = false;


    public Button rButton;
    public Button lButton;

    public GameObject ltext;
    public GameObject rtext;


    public GameObject lendtext;
    public GameObject rendtext;

    // Start is called before the first frame update
    void Start()
    {
        ltext.SetActive(false);
        rtext.SetActive(false);
        rendtext.SetActive(false);
        lendtext.SetActive(false);

        rButton.GetComponent<Image>().color = Color.cyan;
        lButton.GetComponent<Image>().color = Color.cyan;

    }

    // Update is called once per frame
    void Update()
    {
        if (rightGun == true || leftGun == true)
        {
            AirshipStats.enemyGunHit = true;
        }
        else
        {
            AirshipStats.enemyGunHit = false;

        }

        if (enemyGunHealthleft <= 0)
        {
            leftGun = false;
            AirshipStats.enemyGunBroken += 1;

            lButton.GetComponent<Image>().color = Color.gray;
            ltext.SetActive(false);

            lendtext.SetActive(true);

        }
        if (enemyGunHealthright <= 0)
        {
            rightGun = false;
            AirshipStats.enemyGunBroken += 1;
            rButton.GetComponent<Image>().color = Color.gray;
            rendtext.SetActive(true);
            rtext.SetActive(false);


        }
    }




    public void damageToGun(float damage)
    {
        if (AirshipStats.specialBattle == false)
        {
            if (rightGun)
            {
                enemyGunHealthright -= damage;
            }

            if (leftGun)
            {
                enemyGunHealthleft -= damage;
            }
        }

        if (AirshipStats.specialBattle)
        {
            if (rightGun)
            {
                enemyGunHealthSpecialBattRight -= damage;
            }
            if (leftGun)
            {
                enemyGunHealthSpecialBattleLeft -= damage;
            }
        }
    }


    public void OnButtonUp()
    {


        Debug.Log("Can be touched");
        rButton.GetComponent<Image>().color = Color.blue;
        rButton.GetComponent<Image>().color = Color.cyan;


    }

    public void canBeTouched()
    {
        Debug.Log("Can be touched");
        if (rightGun == false)
        {
            rightGun = true;
            leftGun = false;
            rButton.GetComponent<Image>().color = Color.blue;
            lButton.GetComponent<Image>().color = Color.cyan;
            ltext.SetActive(false);
            rtext.SetActive(true);



        }
        else
        {
            rightGun = false;
            rtext.SetActive(false);

            rButton.GetComponent<Image>().color = Color.cyan;

        }

    }

    public void canTouchLeft()
    {
        Debug.Log("Can be touched");
        if (leftGun == false)
        {
            leftGun = true;
            rightGun = false;
            lButton.GetComponent<Image>().color = Color.blue;
            rButton.GetComponent<Image>().color = Color.cyan;
            ltext.SetActive(true);
            rtext.SetActive(false);


        }
        else
        {
            leftGun = false;
            lButton.GetComponent<Image>().color = Color.cyan;
            ltext.SetActive(false);

        }
    }

}
