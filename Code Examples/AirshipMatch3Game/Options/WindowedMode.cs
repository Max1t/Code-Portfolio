using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowedMode : MonoBehaviour
{
    private Dropdown m_Dropdown;
    private int resolutionWidth;
    private int resolutionHeight;
    public int screenMode = 0;
    
    void Start()
    {
        resolutionWidth = Screen.currentResolution.width;
        resolutionHeight = Screen.currentResolution.height;
        m_Dropdown = GetComponent<Dropdown>();
        m_Dropdown.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(m_Dropdown);
        });
    }
    
    private void myDropdownValueChangedHandler(Dropdown target)
    {
        if(target.value == 0)
        {
            SetWindowed();
        }
        else if (target.value == 1)
        {
            SetFullScreenWindow();
        }
        else if (target.value == 2)
        {
            SetMaximizedWindow();
        }
        else
        {
            SetExclusiveFullScreen();
        }
    }

    public void SetExclusiveFullScreen()
    {
        screenMode = 3;
        Screen.SetResolution(resolutionWidth, resolutionHeight, FullScreenMode.ExclusiveFullScreen);
    }

    public void SetFullScreenWindow()
    {
        screenMode = 1;
        Screen.SetResolution(resolutionWidth, resolutionHeight, FullScreenMode.FullScreenWindow);
    }

    public void SetMaximizedWindow()
    {
        screenMode = 2;
        Screen.SetResolution(resolutionWidth, resolutionHeight, FullScreenMode.MaximizedWindow);
    }

    public void SetWindowed()
    {
        screenMode = 0;
        Screen.SetResolution(resolutionWidth, resolutionHeight, FullScreenMode.Windowed);
    }
}
