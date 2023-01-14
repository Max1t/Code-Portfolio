using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RedLinePicture : MonoBehaviour
{

    Image theimage;
    
  //  public Sprite firstSprite;
  //  public Sprite secondSprite;
  //  public Sprite thirdSprite;
   

    // Start is called before the first frame update
    void Start()
    {
        //theimage = GetComponent<Image>();
        gameObject.transform.position = new Vector3(AirshipStats.RedLinePictureX, 1.68f);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 position = new Vector3(AirshipStats.RedLinePictureX, 1.68f);
        gameObject.transform.position = position;
    }


    public void movePicture()
    {
       AirshipStats.RedLinePictureX += 1.5f;
        
      //  Vector3 position = new Vector3(x, y);

      //  theimage.transform.position = position;


    }


}
