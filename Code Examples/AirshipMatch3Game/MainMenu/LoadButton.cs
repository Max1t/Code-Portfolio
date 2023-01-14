using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/savedGame.dat"))
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
