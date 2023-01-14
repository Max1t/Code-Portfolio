using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ToLittleMaps : MonoBehaviour
{
    
    GameObject instance;
    public bool canGo = false;
   public bool canMove = false;
    public bool wentThere = false;
    public bool nowThere = false;
   

    public GameMan manager;


    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {


            if (canGo)
            {

                manager.GetComponent<GameMan>().setCanvas(gameObject);

            }
        }



            
    }


 public void saveMap()
    {




    }



    public void Yes()
    {
        
        canMove = true;
        nowThere = true;

        canGo = false;
        if (wentThere == false)
        {
            wentThere = true;
            manager.GetComponent<GameMan>().MapCleared();
            manager.GetComponent<GameMan>().CloseCanvas();

            manager.bigMapsave();
            AirshipStats.savedBigMap = true;

            SceneManager.LoadScene("Map");

        }
        else
        {
            manager.bigMapsave();
            //SceneManager.LoadScene("TheBigMap");
            manager.No();
        }



    }

    public void lastMap()
    {
        manager.GetComponent<GameMan>().MapCleared();
        manager.GetComponent<GameMan>().CloseCanvas();
        manager.bigMapsave();
        AirshipStats.savedBigMap = true;

        SceneManager.LoadScene("LastMap");
    }

    public void No()
    {
       


       // canMove = true;
        manager.GetComponent<GameMan>().CloseCanvas();


    }







    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameMan>();




    }

    // Update is called once per frame
    void Update()
    {
        
 



    }
}
