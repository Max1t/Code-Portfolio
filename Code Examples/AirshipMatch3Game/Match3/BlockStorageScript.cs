using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BlockStorageScript : MonoBehaviour
{

    public TextMeshProUGUI blueText;
    public int blueAmount;
    public int maxBlue;
    public TextMeshProUGUI redText;
    public int redAmount;
    public int maxRed;
    public TextMeshProUGUI greenText;
    public int greenAmount;
    public int maxGreen;
    public TextMeshProUGUI whiteText;
    public int whiteAmount;
    public int maxWhite;

    public TextMeshProUGUI yellowText;
    public int yellowAmount;
    public int maxYellow;

    public TextMeshProUGUI purpleText;
    public int violetAmount;
    public int maxViolet;


    public EnemyHealthMatch3 enemyHealth;
    // Gets the tag of the block as parameter

    private void Start()
    {
        refreshText();
    }
    public void addToBlockStorage(string tag)
    {
        if (tag == "Blue")
        {
            if (blueAmount < maxBlue)
                blueAmount++;
        }
        if (tag == "Red")
        {
            if (redAmount < maxRed)
                redAmount++;
        }
        if (tag == "Green")
        {
            if (greenAmount < maxGreen)
                greenAmount++;
        }
        if (tag == "White")
        {
            if (whiteAmount < maxWhite)
                whiteAmount++;
        }
        if (tag == "Yellow")
        {
            if (yellowAmount < maxYellow)
                yellowAmount++;
        }
        if (tag == "Violet")
        {
            if (violetAmount < maxViolet)
                violetAmount++;
        }
        if (tag == "Credit")
        {
            AirshipStats.credits += 10;
        }
        refreshText();
    }

    public bool useBlocksFromStorageSingleColor(string color, int amount)
    {
        if (color == "Blue")
        {
            if ((blueAmount - amount) < 0) return false;
            else blueAmount -= amount;
        }
        if (color == "Red")
        {
            if ((redAmount - amount) < 0) return false;
            else redAmount -= amount;
        }
        if (color == "Green")
        {
            if ((greenAmount - amount) < 0) return false;
            else greenAmount -= amount;
        }
        if (color == "White")
        {
            if ((whiteAmount - amount) < 0) return false;
            else whiteAmount -= amount;
        }
        if (color == "Yellow")
        {
            if ((yellowAmount - amount) < 0) return false;
            else yellowAmount -= amount;
        }
        if (color == "Violet")
        {
            if ((violetAmount - amount) < 0) return false;
            else violetAmount -= amount;
        }
        refreshText();
        return true;
    }
    // Check if enough blocks in storage, doesn't use up any blocks
    public bool checkBlocksFromStorageMultipleColor(string[] colors, int[] amounts)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i] == "Blue")
            {
                if ((blueAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Red")
            {
                if ((redAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Green")
            {
                if ((greenAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "White")
            {
                if ((whiteAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Yellow")
            {
                if ((yellowAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Violet")
            {
                if ((violetAmount - amounts[i]) < 0) return false;
            }
        }
        return true;
    }
    // Pass 2 arrays as parameters and the total damage to enemy health
    // amounts[i] = the amount of color[i]
    public bool useBlocksFromStorageMultipleColor(string[] colors, int[] amounts)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i] == "Blue")
            {
                if ((blueAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Red")
            {
                if ((redAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Green")
            {
                if ((greenAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "White")
            {
                if ((whiteAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Yellow")
            {
                if ((yellowAmount - amounts[i]) < 0) return false;
            }
            if (colors[i] == "Violet")
            {
                if ((violetAmount - amounts[i]) < 0) return false;

            }

        }

        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i] == "Blue")
            {
                blueAmount -= amounts[i];
            }
            if (colors[i] == "Red")
            {
                redAmount -= amounts[i];
            }
            if (colors[i] == "Green")
            {
                greenAmount -= amounts[i];
            }
            if (colors[i] == "White")
            {
                whiteAmount -= amounts[i];
            }
            if (colors[i] == "Yellow")
            {
                yellowAmount -= amounts[i];
            }
            if (colors[i] == "Violet")
            {
                violetAmount -= amounts[i];
            }

        }

        refreshText();
        return true;
    }

    private void refreshText()
    {
        blueText.text = " x" + blueAmount.ToString();
        redText.text = " x" + redAmount.ToString();
        greenText.text = " x" + greenAmount.ToString();
        whiteText.text = " x" + whiteAmount.ToString();
        yellowText.text = "x" + yellowAmount.ToString();
        if (AirshipStats.ammo1 >= 1)
        {
            purpleText.text = "Violet: " + violetAmount.ToString();

        }
    }
    /* 
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2 + 25, Screen.width / 10, Screen.height / 20), "Blue"))
            useBlocksFromStorage("Blue", 2);
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2 + 50, Screen.width / 10, Screen.height / 20), "Green"))
            useBlocksFromStorage("Green", 5);
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2 + 75, Screen.width / 10, Screen.height / 20), "Red"))
            useBlocksFromStorage("Red", 10);
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2 + 100, Screen.width / 10, Screen.height / 20), "White"))
            useBlocksFromStorage("White", 1);
        if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2 + 125, Screen.width / 10, Screen.height / 20), "Yellow"))
            useBlocksFromStorage("Yellow", 20);
    }
    */
}
