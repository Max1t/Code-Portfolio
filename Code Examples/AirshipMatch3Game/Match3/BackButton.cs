using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton.GetComponent<Button>().onClick.AddListener(() => clickedButton());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedButton()
    {
        SceneManager.LoadScene("Map");
    }
}
