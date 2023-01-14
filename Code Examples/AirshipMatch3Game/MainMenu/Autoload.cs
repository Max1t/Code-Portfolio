using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!AirshipStats.noAutoLoad)
            SaveLoad.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
