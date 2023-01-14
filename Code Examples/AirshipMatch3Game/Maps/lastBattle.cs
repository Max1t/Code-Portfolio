using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lastBattle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            AirshipStats.specialBattle = true;
            SceneManager.LoadScene("Airship0220");
            

        }




    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
