using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBlockStorage : MonoBehaviour
{
    public TextMeshProUGUI blueText;
    public int blueAmount;
    public TextMeshProUGUI redText;
    public int redAmount;
    public TextMeshProUGUI greenText;
    public int greenAmount;
    public TextMeshProUGUI whiteText;
    public int whiteAmount;
    public TextMeshProUGUI yellowText;
    public int yellowAmount;
    public TextMeshProUGUI purpleText;
    public int violetAmount;

    public Matches matches;

    private void Start()
    {
        matches = FindObjectOfType<Matches>();
        RefreshText();
    }

    public void AttackPlayer()
    {
        int amountToAttack = 5;
        if(blueAmount > amountToAttack)
        {
            blueAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
        else if(redAmount > amountToAttack)
        {
            redAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
        else if (greenAmount > amountToAttack)
        {
            greenAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
        else if (whiteAmount > amountToAttack)
        {
            whiteAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
        else if (yellowAmount > amountToAttack)
        {
            yellowAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
        else if (violetAmount > amountToAttack)
        {
            violetAmount -= amountToAttack;
            matches.MoveEnemyBullet();
        }
    }

    public void AddToBlockStorage(string tag)
    {
        if (tag == "Blue")
        {
            blueAmount++;
        }
        if (tag == "Red")
        {
            redAmount++;
        }
        if (tag == "Green")
        {
            greenAmount++;
        }
        if (tag == "White")
        {
            whiteAmount++;
        }
        if (tag == "Yellow")
        {
            yellowAmount++;
        }
        if (tag == "Violet")
        {
            violetAmount++;
        }
        if (tag == "Credit")
        {
            //AirshipStats.credits += 10;
            //AI gets no credits 
        }
        RefreshText();
    }

    public bool UseBlocksFromStorageSingleColor(string color, int amount)
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
        RefreshText();
        return true;
    }
    // Check if enough blocks in storage, doesn't use up any blocks
    public bool CheckBlocksFromStorageMultipleColor(string[] colors, int[] amounts)
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
    public bool UseBlocksFromStorageMultipleColor(string[] colors, int[] amounts)
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

        RefreshText();
        return true;
    }

    private void RefreshText()
    {
        blueText.text = "   x" + blueAmount.ToString();
        redText.text = "   x" + redAmount.ToString();
        greenText.text = "   x" + greenAmount.ToString();
        //whiteText.text = "White: " + whiteAmount.ToString();
        yellowText.text = "   x" + yellowAmount.ToString();
        purpleText.text = "   x" + violetAmount.ToString();
    }
}
