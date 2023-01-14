using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolutions : MonoBehaviour
{
    public GameObject screenModeDropdown;
    private Dropdown m_Dropdown;
    private Resolution[] resolutions; //all available resolutions
    private int resolutionWidth;
    private int resolutionHeight;

    private void Awake()
    {
        resolutions = Screen.resolutions;
    }

    void Start()
    {
        resolutionWidth = Screen.currentResolution.width;
        resolutionHeight = Screen.currentResolution.height;
        m_Dropdown = GetComponent<Dropdown>();
        List<string> resos = new List<string>();
        foreach (var res in resolutions)
        {
            string reso = res.width + "x" + res.height + " : " + res.refreshRate;
            resos.Add(reso);
        }
        m_Dropdown.AddOptions(resos);
        m_Dropdown.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(m_Dropdown);
        });
    }
    
    private void myDropdownValueChangedHandler(Dropdown target)
    {
        if(screenModeDropdown.GetComponent<WindowedMode>().screenMode == 0)
        {
            Screen.SetResolution(resolutions[target.value].width, resolutions[target.value].height, FullScreenMode.Windowed, resolutions[target.value].refreshRate);
        }
        else if(screenModeDropdown.GetComponent<WindowedMode>().screenMode == 1)
        {
            Screen.SetResolution(resolutions[target.value].width, resolutions[target.value].height, FullScreenMode.FullScreenWindow, resolutions[target.value].refreshRate);
        }
        else if (screenModeDropdown.GetComponent<WindowedMode>().screenMode == 2)
        {
            Screen.SetResolution(resolutions[target.value].width, resolutions[target.value].height, FullScreenMode.MaximizedWindow, resolutions[target.value].refreshRate);
        }
        else
        {
            Screen.SetResolution(resolutions[target.value].width, resolutions[target.value].height, FullScreenMode.ExclusiveFullScreen, resolutions[target.value].refreshRate);
        }
    }
}
