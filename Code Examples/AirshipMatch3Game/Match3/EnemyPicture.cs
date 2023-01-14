using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyPicture : MonoBehaviour
{

    Image theimage;
    public Sprite normalsprite;
    public Sprite normalsprite2;
    public Sprite normalsprite3;

    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;

    public Sprite secondEnemy;
    public Sprite lasBossSprite;
    public bool pushedAlready = false;
    public GameObject enemyShipInfo;

    public EnemyShipInsidesImage insidesImage;

    // Start is called before the first frame update
    void Start()
    {
        theimage = GetComponent<Image>();

        setImage();

    }


    public void setImage()
    {
        if (AirshipStats.specialBattle)
        {
            theimage.sprite = lasBossSprite;

        }
        /* 
        else if(AirshipStats.badBttleStart)
        {

             //   theimage.sprite = secondEnemy;
            theimage.sprite = normalsprite;
        }
         */
        else
        {
            int rng = Random.Range(0, 3);
            if (rng == 0)
            {
                sprite1.SetActive(true);
            }
            if (rng == 1)
            {
                sprite2.SetActive(true);
            }
            if (rng == 2)
            {
                sprite3.SetActive(true);
            }
            insidesImage.setImage(rng);

        }

    }

    public void enemyButtonPush()
    {
        if (pushedAlready == false)
        {
            pushedAlready = true;

        }
        else
        {
            pushedAlready = false;
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (pushedAlready)
        {
            AirshipStats.battlePause = true;
            enemyShipInfo.SetActive(true);



        }
        else
        {
            AirshipStats.battlePause = false;
            enemyShipInfo.SetActive(false);


        }
    }
}
