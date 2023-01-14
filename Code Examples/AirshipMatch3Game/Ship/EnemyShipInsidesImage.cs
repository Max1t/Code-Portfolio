using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyShipInsidesImage : MonoBehaviour
{

    public Image theimage;
    public Sprite normalsprite;
    public Sprite normalsprite2;
    public Sprite normalsprite3;

    public Sprite lasBossSprite;



    // Start is called before the first frame update



    public void setImage(int rng)
    {
        if (AirshipStats.specialBattle)
        {
            theimage.sprite = lasBossSprite;
        }
        else
        {
            if (rng == 0) theimage.sprite = normalsprite;
            if (rng == 2) theimage.sprite = normalsprite2;
            if (rng == 1) theimage.sprite = normalsprite3;
        }







    }


    // Update is called once per frame
    void Update()
    {

    }
}
