using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Rarity { Common, Uncommon, Rare, Special };

public class Parcel : MonoBehaviour, IClickable
{

    public bool InReach;
    public Rarity rarity = 0;
    public bool specialParcel;

    // Update is called once per frame
    void Update()
    {

        if (InReach == true)
        {
            gameObject.tag = "Parcel";
        }

        else
        {
            gameObject.tag = "Untagged";
        }

    }
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Reach")
        {
            InReach = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        if (collider.tag == "Reach")
        {
            InReach = false;

        }

    }

    public void OnClick()
    {
        if (InReach)
        {

            if (rarity == Rarity.Common)
            {
                Gamemanager.Get.currentMinigameParcel = this;
                SceneSystem.Get.LoadMinigame(2);
                gameObject.SetActive(false);
            }
            if (rarity == Rarity.Uncommon)
            {
                if (Gamemanager.Get.PlayerInventory.Snatchatron == true)
                {
                    Gamemanager.Get.currentMinigameParcel = this;
                    SceneSystem.Get.LoadMinigame(3);
                    gameObject.SetActive(false);
                }
                else
                {
                    Gamemanager.Get.PlayerInventory.UI.SnatchatronRequiredPopup.SetActive(true);
                    PlayerVariables.playerMove = false;
                }
            }
            if (rarity == Rarity.Rare)
            {
                Gamemanager.Get.currentMinigameParcel = this;
                SceneSystem.Get.LoadMinigame(4);
                gameObject.SetActive(false);
            }
            if (rarity == Rarity.Special)
            {
                Gamemanager.Get.currentMinigameParcel = this;
                SceneSystem.Get.LoadMinigame(5);
                gameObject.SetActive(false);
            }

        }
    }
}
