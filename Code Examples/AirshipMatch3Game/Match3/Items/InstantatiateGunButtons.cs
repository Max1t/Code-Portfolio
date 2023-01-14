using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantatiateGunButtons : MonoBehaviour
{


    public GameObject gunButton;
    public GameObject CanvasLayoutGroupButtons;

    //public List<GunBase> Inventory;



    // Start is called before the first frame update
    void Start()
    {
        if (Inventory.instance == null) return;
        foreach (GunBase gun in Inventory.instance.equippedItems)
        {

            gun.Initialize();
            GameObject Instance = Instantiate(gunButton, CanvasLayoutGroupButtons.transform.position,
                                            CanvasLayoutGroupButtons.transform.rotation,
                                            CanvasLayoutGroupButtons.transform);

            Instance.GetComponent<Button>().onClick.AddListener(() => gun.OnButtonDown());
            TextMeshProUGUI buttonText = Instance.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = gun.itemName + "\n";

            int index = 0;
            foreach (string color in gun.colorsToUse)
            {
                string toAppend = "";
                if (color == "Blue") toAppend = "B";
                else if (color == "Green") toAppend = "G";
                else if (color == "Red") toAppend = "R";
                else if (color == "White") toAppend = "P";
                else if (color == "Yellow") toAppend = "Y";
                else if (color == "Violet") toAppend = "V";

                buttonText.text += gun.amountOfColorsToUse[index] + toAppend + " ";
                index++;
            }

            if (gun.colorsToUse[0] == "Blue") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.blue;
            else if (gun.colorsToUse[0] == "Green") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.green;
            else if (gun.colorsToUse[0] == "Red") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.red;
            else if (gun.colorsToUse[0] == "White") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.magenta;
            else if (gun.colorsToUse[0] == "Yellow") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.yellow;
            else if (gun.colorsToUse[0] == "Violet") Instance.GetComponentInChildren<TextMeshProUGUI>().outlineColor = Color.magenta;

        }

    }


}
