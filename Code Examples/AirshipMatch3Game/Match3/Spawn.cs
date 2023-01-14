using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public List<GameObject> AllBlocksList;          // All blocks
    public GameObject purpleBall;
    public List<GameObject> PossibleBlocksList;     // Blocks legal to spawn, Only affects the initial spawn

    public GameObject SpawnPoint;
    public Matches matches;
    public int howManyColors;

    private void Start()
    {
        StartCoroutine(InitialSpawn());
        // MOVEC TO COROUTINE
        /*  GameObject[,] AllBlocksCoordinates = new GameObject[8, 8];
         howManyColors = AllBlocksList.Count;
         possibleColors = howManyColors;
         for (int x = 0; x < 8; x++)
         {
             for (int y = 0; y < 8; y++)
             {
                 PossibleBlocksList = new List<GameObject>(AllBlocksList);

                 if (y != 0 || x != 0)
                 {
                     foreach (GameObject block in AllBlocksList)
                     {
                         if (x - 1 >= 0 && x - 2 >= 0)
                         {
                             if (AllBlocksCoordinates[x - 1, y].tag == block.tag && AllBlocksCoordinates[x - 2, y].tag == block.tag)
                             {
                                 if (PossibleBlocksList.Contains(block)) PossibleBlocksList.Remove(block);
                             }
                         }
                         if (y - 1 >= 0 && y - 2 >= 0)
                         {
                             if (AllBlocksCoordinates[x, y - 1].tag == block.tag && AllBlocksCoordinates[x, y - 2].tag == block.tag)
                             {
                                 if (PossibleBlocksList.Contains(block)) PossibleBlocksList.Remove(block);
                             }
                         }
                     }
                 }
                 int RandomNumber = Random.Range(0, PossibleBlocksList.Count);
                 Vector3 SpawnLocation = (SpawnPoint.transform.position + (new Vector3(x * 1.2f, y * 1.2f, 0)));
                 GameObject instance = (GameObject)Instantiate(PossibleBlocksList[RandomNumber], SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                 instance.name = name + " " + (x + 1) + "-" + (y + 1); //for debugging
                 AllBlocksCoordinates[x, y] = instance;
                 matches.AddBlock(instance);
             }
         }
         matches.gameReadyToStart = true;
         */
    }

    IEnumerator InitialSpawn()
    {
        GameObject[,] AllBlocksCoordinates = new GameObject[8, 8]; // Multidimensional array for holding all blocks for the initial spawning
        howManyColors = AllBlocksList.Count;

        for (int x = 0; x < 8; x++)
        {
            yield return new WaitForSeconds(.1f); // Little Animation for the spawn, staggered columns
            for (int y = 0; y < 8; y++)
            {
                PossibleBlocksList = new List<GameObject>(AllBlocksList); // Copy All Blocks to a temp list for possible blocks
                if (y != 0 || x != 0)
                {
                    foreach (GameObject block in AllBlocksList)
                    {
                        if (x - 1 >= 0 && x - 2 >= 0)
                        {
                            if (AllBlocksCoordinates[x - 1, y].tag == block.tag && AllBlocksCoordinates[x - 2, y].tag == block.tag)
                            {
                                if (PossibleBlocksList.Contains(block)) PossibleBlocksList.Remove(block); // Remove a block from possible list if it would cause a match
                            }
                        }
                        if (y - 1 >= 0 && y - 2 >= 0)
                        {
                            if (AllBlocksCoordinates[x, y - 1].tag == block.tag && AllBlocksCoordinates[x, y - 2].tag == block.tag)
                            {
                                if (PossibleBlocksList.Contains(block)) PossibleBlocksList.Remove(block); // Same as above
                            }
                        }
                    }
                }
                /* 
                if (AirshipStats.ammo1 >= 1)
                {

                    Vector3 SpawnLocation = (SpawnPoint.transform.position + (new Vector3(x * 1f, y * 1.2f, 0)));
                    GameObject instance;

                    int testball = Random.Range(0, 6);
                    if (testball > 4)
                    {


                        instance = (GameObject)Instantiate(purpleBall, SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                        instance.name = name + " " + (x + 1) + "-" + (y + 1); //for debugging
                        AllBlocksCoordinates[x, y] = instance;
                        matches.AddBlock(instance);

                    }


                    if (testball < 5)
                    {

                        int RandomNumber = Random.Range(0, PossibleBlocksList.Count);
                        instance = (GameObject)Instantiate(PossibleBlocksList[RandomNumber], SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                        instance.name = name + " " + (x + 1) + "-" + (y + 1); //for debugging
                        AllBlocksCoordinates[x, y] = instance;
                        matches.AddBlock(instance);


                    }

                }
                */
                
                    int RandomNumber = Random.Range(0, PossibleBlocksList.Count);
                    Vector3 SpawnLocation = (SpawnPoint.transform.position + (new Vector3(x * 1f, y * 1.2f, 0)));
                    GameObject instance = (GameObject)Instantiate(PossibleBlocksList[RandomNumber], SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                    instance.name = name + " " + (x + 1) + "-" + (y + 1); //for debugging
                    AllBlocksCoordinates[x, y] = instance;
                    matches.AddBlock(instance);
                


            }
        }
        AirshipStats.battlePause = false;
        matches.gameReadyToStart = true;
        AllBlocksCoordinates = null; // Array no longer used, set to null for garbage collection;
        yield return null;
    }

    public void CreateABlock(GameObject block)
    {
        /* if (AirshipStats.ammo1 >= 1) {

            GameObject instance;
            Vector3 SpawnLocation = new Vector3(block.transform.position.x, block.transform.position.y + 9, block.transform.position.z);


            int testball = Random.Range(0, 6);

            if(testball < 5)
            {
                int RandomNumber = Random.Range(0, howManyColors);
                instance = (GameObject)Instantiate(AllBlocksList[RandomNumber], SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                matches.AddBlock(instance);

            }


            if (testball > 4) {

                instance = (GameObject)Instantiate(purpleBall, SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
                matches.AddBlock(instance);


            }
        }
        else
        */
        {
            int RandomNumber = Random.Range(0, howManyColors);
            Vector3 SpawnLocation = new Vector3(block.transform.position.x, block.transform.position.y + 9, block.transform.position.z);
            GameObject instance = (GameObject)Instantiate(AllBlocksList[RandomNumber], SpawnLocation, SpawnPoint.transform.rotation, SpawnPoint.transform);
            matches.AddBlock(instance);
        }
    }

   
}
