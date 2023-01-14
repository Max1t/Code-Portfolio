using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour
{

    public GameObject canvas;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject text;

    
    private GameObject temp;

    public Sprite canGoSprite;
    public Sprite Normal;


    public int HaveAllMapsBeenCleared = 1;

    public GameObject[] objectlist = new GameObject[13];



    public void CloseCanvas()
    {
        Debug.Log("false");

        //canvas.SetActive(false);
        yesButton.SetActive(false);


        noButton.SetActive(false);
        text.SetActive(false);
    }




    public void setCanvas(GameObject temporal)
    {
        temp = temporal;
        Yes();
        Debug.Log("true");

      //  yesButton.SetActive(true);
       
       // noButton.SetActive(true);
      //  text.SetActive(true);

    }



    public void Yes()
    {
        //bigMapsave();
        if(temp.tag == "Exit")
        {
            temp.GetComponent<ToLittleMaps>().lastMap();

        }
        else
        {
            temp.GetComponent<ToLittleMaps>().Yes();

        }

    }

    public void No()
    {
       
       // temp.GetComponent<ToLittleMaps>().No();

        for(int i = 0; i < objectlist.Length; i++)
        {
            objectlist[i].GetComponent<ToLittleMaps>().canMove = false;
        }

        temp.GetComponent<ToLittleMaps>().canMove = true;

        SpriteCheck();
    }



    public void SpriteCheck()
    {

        for(int i = 0; i < objectlist.Length; i++)
        {

            if(objectlist[i].GetComponent<ToLittleMaps>().canGo == false)
            {
                objectlist[i].GetComponent<SpriteRenderer>().sprite = Normal;

            }

            else if (objectlist[i].GetComponent<ToLittleMaps>().canMove)
            {
                objectlist[i].GetComponent<SpriteRenderer>().sprite = Normal;

            }

            else if(objectlist[i].GetComponent<ToLittleMaps>().canGo == true)
            {
                objectlist[i].GetComponent<SpriteRenderer>().sprite = canGoSprite;

            }

        }


    }



    public void Prefabcheck()
    {

        if (objectlist[0].GetComponent<ToLittleMaps>().canMove)
        {
            objectlist[1].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[2].GetComponent<ToLittleMaps>().canGo = true;


            SpriteCheck();

        }

        if (objectlist[1].GetComponent<ToLittleMaps>().canMove) {

            objectlist[2].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[4].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[6].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[8].GetComponent<ToLittleMaps>().canGo = false;

            SpriteCheck();
        }

        if (objectlist[2].GetComponent<ToLittleMaps>().canMove) {

            objectlist[1].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[3].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[5].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;


         
            SpriteCheck();


        }

        if (objectlist[3].GetComponent<ToLittleMaps>().canMove) {

            objectlist[5].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;

        

            SpriteCheck();


        }

        if (objectlist[4].GetComponent<ToLittleMaps>().canMove) {

            

            objectlist[6].GetComponent<ToLittleMaps>().canGo = true;




            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[8].GetComponent<ToLittleMaps>().canGo = false;

            

            SpriteCheck();

        }

        if (objectlist[5].GetComponent<ToLittleMaps>().canMove) {

            

            objectlist[6].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[7].GetComponent<ToLittleMaps>().canGo = true;



            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[8].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[9].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[10].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[11].GetComponent<ToLittleMaps>().canGo = false;

            

            SpriteCheck();

        }

        if (objectlist[6].GetComponent<ToLittleMaps>().canMove) {


            

            objectlist[5].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[8].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[9].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[10].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[11].GetComponent<ToLittleMaps>().canGo = false;

      

            SpriteCheck();

        }

        if (objectlist[7].GetComponent<ToLittleMaps>().canMove) {
            

            objectlist[9].GetComponent<ToLittleMaps>().canGo = true;



            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[8].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[10].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[11].GetComponent<ToLittleMaps>().canGo = false;




            SpriteCheck();


        }

        if (objectlist[8].GetComponent<ToLittleMaps>().canMove) {

          

            objectlist[10].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;

            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[9].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[11].GetComponent<ToLittleMaps>().canGo = false;



            SpriteCheck();

        }

        if (objectlist[9].GetComponent<ToLittleMaps>().canMove) {

            objectlist[7].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[11].GetComponent<ToLittleMaps>().canGo = true;


            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[10].GetComponent<ToLittleMaps>().canGo = false;



            SpriteCheck();


        }

        if (objectlist[10].GetComponent<ToLittleMaps>().canMove) {

            objectlist[8].GetComponent<ToLittleMaps>().canGo = true;

            objectlist[11].GetComponent<ToLittleMaps>().canGo = true;



            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[9].GetComponent<ToLittleMaps>().canGo = false;

       

            SpriteCheck();


        }

        if (objectlist[11].GetComponent<ToLittleMaps>().canMove) {

         


            objectlist[1].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[2].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[3].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[4].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[5].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[6].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[7].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[8].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[9].GetComponent<ToLittleMaps>().canGo = false;
            objectlist[10].GetComponent<ToLittleMaps>().canGo = false;





                objectlist[12].GetComponent<ToLittleMaps>().canGo = true;


            


            SpriteCheck();


        }







    }






    public void bigMapsave()
    {
        List<BigMapInfo> MapInformation = new List<BigMapInfo>();


        for (int i = 0; i < objectlist.Length; i++)
        {

            BigMapInfo info = new BigMapInfo(objectlist[i].GetComponent<ToLittleMaps>().canGo, objectlist[i].GetComponent<ToLittleMaps>().canMove, objectlist[i].GetComponent<ToLittleMaps>().wentThere);
            MapInformation.Add(info);
        }

        AirshipStats.BigMapInformation = MapInformation;


    }



    public void MapCleared()
    {

        HaveAllMapsBeenCleared = HaveAllMapsBeenCleared + 1;

    }


    // Start is called before the first frame update
    void Start()
    {
        CloseCanvas();

        if (AirshipStats.savedBigMap)
        {
            for(int i = 0; i < objectlist.Length; i++)
            {

                objectlist[i].GetComponent<ToLittleMaps>().canGo = AirshipStats.BigMapInformation[i]._canGo;
                objectlist[i].GetComponent<ToLittleMaps>().canMove = AirshipStats.BigMapInformation[i]._canMove;
                objectlist[i].GetComponent<ToLittleMaps>().wentThere = AirshipStats.BigMapInformation[i]._wentThere;


            }
        }

        for(int i = 0; i < objectlist.Length; i++)
        {
            if(objectlist[i].GetComponent<ToLittleMaps>().wentThere == true)
            {
                HaveAllMapsBeenCleared = HaveAllMapsBeenCleared + 1;

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Prefabcheck();
    }
}
