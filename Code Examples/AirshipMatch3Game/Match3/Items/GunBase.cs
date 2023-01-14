using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class GunBase : ItemBase
{

    public float dmg;

    [Header("-----Initialized in script------")]
    public BlockStorageScript blockStorage;
    public EnemyHealthMatch3 enemyHealth;
    public Match3Abilities abilities;
    public Swapper swap;
    public Matches matches;
    public GameObject canvas;
    public GameObject crossHair;
    public ChooseBlockCoroutine Coroutine;
    [Header("--------------------------------")]

    // Arrays to govern how much which color to use from blockstorage
    // First color in colorToUse changes the guns buttons outline color
    public string[] colorsToUse;
    public int[] amountOfColorsToUse;
    public string soundClip = "";

    public AudioManager audio;
    // Method used in the button
    public abstract void OnButtonDown();
    // Required for OnButtonDown if it needs a block from the match3 game
    //public abstract IEnumerator ChooseBlock();

    // References
    public void Initialize()
    {
        audio = FindObjectOfType<AudioManager>();
        enemyHealth = GameObject.Find("Enemy").GetComponent<EnemyHealthMatch3>();
        blockStorage = GameObject.Find("BlockStorage").GetComponent<BlockStorageScript>();
        swap = GameObject.Find("Swap").GetComponent<Swapper>();
        abilities = GameObject.Find("Swap").GetComponent<Match3Abilities>();
        canvas = GameObject.Find("Canvas");
        crossHair = GameObject.Find("crossHair");
        Coroutine = ChooseBlockCoroutine.Instance;
        matches = GameObject.Find("Swap").GetComponent<Matches>();
    }

    // Activates crosshair on the canvas if it's inactive
    public void activateCrosshair()
    {
        if (!crossHair.activeSelf)
        {
            crossHair.SetActive(true);
        }
    }

    // Deactivates crosshair from the canvas if it's active
    public void deactivateCrosshair()
    {
        if (crossHair.activeSelf)
        {
            crossHair.SetActive(false);
        }
    }
}
