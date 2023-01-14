using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Debug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!AirshipStats.debugging)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
