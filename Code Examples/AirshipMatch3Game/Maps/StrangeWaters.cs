using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrangeWaters : MonoBehaviour
{
    private GameObject islandSpawnPositions;

    private List<GameObject> islands = new List<GameObject>();
    public GameObject canvas;
    public GameObject yesButton;
    public GameObject buttonToBigMap;
    public GameObject noButton;
    public GameObject text;

    private GameObject temp;

    public GameObject XMark;
    public GameObject Exit;
    public GameObject IslandParent;
    public GameObject mapTarget; //this is the red arrow that shows the target island that was given in a dialogue

    int RandomNumber;
    float RandomCoordX;
    float RandomCoordY;

    public Collider2D[] colliders;
    public Collider2D[] colliders2;

    public Sprite AlreadyWentX;
    public GameObject startingPoint;
    public RenderLines Lines;

    public Transform maxDistanceTransform;

    GameObject shipPrefab;
    GameObject target;

    public RedLinePicture redpic;

    void Start()
    {
        int mapPositions = 1;
        //Random.Range(1, 6); //amount of different map spawn blueprints
        islandSpawnPositions = GameObject.Find("IslandSpawnPositions" + mapPositions);
        redpic = FindObjectOfType<RedLinePicture>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        CloseCanvas();

        if (!AirshipStats.createdMap) //new map so we save the information
        {
            int amountOfIslands = islandSpawnPositions.transform.childCount;
            float xCoord = 0;
            float yCoord = 0;
            float angle = 0;
            float angleRadian = 0;
            float minRadius = 1f;
            float maxRadius = 2f;
            float distance = 0f;
            float maxDistance = Vector2.Distance(IslandParent.transform.position, maxDistanceTransform.position);
            float localYZero = IslandParent.transform.position.y;
            Vector2 start = IslandParent.transform.position;
            Vector2 spawn = Vector2.zero;




            //spawn normal islands
            for (int i = 0; i < amountOfIslands; i++)
            {
                if (spawn == Vector2.zero)
                    spawn = start;
                //random spawn position
                /* Vector2 xCoordMin = islandSpawnPositions.transform.GetChild(i).transform.GetChild(0).transform.position; //x coord min
                Vector2 xCoordMax = islandSpawnPositions.transform.GetChild(i).transform.GetChild(1).transform.position; //x coord max
                Vector2 yCoordMax = islandSpawnPositions.transform.GetChild(i).transform.GetChild(2).transform.position; //y coord max
                Vector2 yCoordMin = islandSpawnPositions.transform.GetChild(i).transform.GetChild(3).transform.position; //y coord min
                */

                //Maths 
                if (i % 2 == 0)
                {
                    angle = UnityEngine.Random.Range(0, 80f);
                }
                else
                {
                    angle = UnityEngine.Random.Range(0, -80f);
                }
                angleRadian = angle * Mathf.PI / 180f;
                float radius = UnityEngine.Random.Range(minRadius, maxRadius);
                distance += radius;
                if (i == amountOfIslands - 2 && distance > (maxDistance - 3f))
                {
                    radius = 1f;
                }
                if (i == amountOfIslands - 1 && distance > (maxDistance - 2f))
                {
                    radius = 1f;
                }
                xCoord = radius * Mathf.Cos(angleRadian);
                yCoord = radius * Mathf.Sin(angleRadian);
                Vector2 positionIsland = new Vector2(spawn.x + xCoord, spawn.y + yCoord);
                spawn = new Vector2(positionIsland.x, localYZero);

                GameObject islandInstance;
                if (i == amountOfIslands - 1) //spawn exit
                {
                    islandInstance = (GameObject)Instantiate(Exit, positionIsland, transform.rotation, IslandParent.transform);
                    islandInstance.GetComponent<NextMap>().exit = true;
                    islandInstance.name = "exit";
                    islandInstance.GetComponent<NextMap>().number = 100;
                }
                else
                {
                    //spawn island
                    islandInstance = (GameObject)Instantiate(XMark, positionIsland, transform.rotation, IslandParent.transform);
                    islandInstance.name = i.ToString();
                    islandInstance.GetComponent<NextMap>().number = i;
                }

                //start island
                if (i == 0)
                {
                    islandInstance.GetComponent<NextMap>().searchNext = true;
                    islandInstance.GetComponent<SpriteRenderer>().sprite = AlreadyWentX;
                    shipPrefab = (GameObject)Instantiate(startingPoint, (islandInstance.transform.position + new Vector3(0, 0, 6f)), transform.rotation, IslandParent.transform);
                    target = islandInstance;

                    shipPrefab.transform.RotateAround((target.transform.position + new Vector3(0, 0, -2f)), new Vector3(0, 1, 0), 100 * Time.deltaTime);

                }
                islands.Add(islandInstance);
            }

            SaveCurrentMap();
        }
        else //load map
        {
            foreach (var isl in AirshipStats.islandsInformation)
            {
                if (isl._exit) //is exit
                {
                    GameObject isleInstance = Instantiate(Exit, new Vector2(isl._x, isl._y), Quaternion.identity, IslandParent.transform);
                    isleInstance.GetComponent<NextMap>().canGo = false;
                    // isleInstance.GetComponent<NextMap>().canGo = isl._canGo;
                    isleInstance.GetComponent<NextMap>().searchNext = isl._searchNext;
                    isleInstance.GetComponent<NextMap>().goneThere = isl._goneThere;
                    isleInstance.GetComponent<NextMap>().exit = isl._exit;
                    isleInstance.GetComponent<NextMap>().number = isl._number;
                    isleInstance.name = isl._name;
                    islands.Add(isleInstance);
                }
                else //is not exit
                {
                    GameObject isleInstance = Instantiate(XMark, new Vector2(isl._x, isl._y), Quaternion.identity, IslandParent.transform);
                    //isleInstance.GetComponent<NextMap>().canGo = isl._canGo;
                    isleInstance.GetComponent<NextMap>().canGo = false;
                    isleInstance.GetComponent<NextMap>().searchNext = isl._searchNext;
                    isleInstance.GetComponent<NextMap>().goneThere = isl._goneThere;
                    isleInstance.name = isl._name;
                    isleInstance.GetComponent<NextMap>().number = isl._number;
                    isleInstance.GetComponent<NextMap>().mapTarget = isl._mapTarget;
                    //this is a target island, need to mark it
                    if (isl._mapTarget)
                    {
                        GameObject mapTargetInstance = Instantiate(mapTarget, new Vector2(isl._x, isl._y+0.45f), Quaternion.identity, IslandParent.transform);
                    }
                    //this is the island where we are at
                    if (isl._searchNext)
                    {
                        isleInstance.GetComponent<SpriteRenderer>().sprite = AlreadyWentX;
                        shipPrefab = (GameObject)Instantiate(startingPoint, (isleInstance.transform.position + new Vector3(0, 0, 6f)), transform.rotation, IslandParent.transform);
                        shipPrefab.transform.position = isleInstance.transform.position;

                        target = isleInstance;

                        shipPrefab.transform.RotateAround((target.transform.position + new Vector3(0f, 0, -2f)), new Vector3(0, 1, 0), 100 * Time.deltaTime);

                    }
                    if (isl._goneThere)
                        isleInstance.GetComponent<SpriteRenderer>().sprite = AlreadyWentX;
                    islands.Add(isleInstance);
                }
            }
        }

        //foreach (var isl in islands)
        //{
        //    isl.GetComponent<NextMap>().canGo = false;
        //}

        foreach (var isl in islands)
        {
            if (isl.GetComponent<NextMap>().searchNext)
            {
                isl.GetComponent<NextMap>().CheckForIslandsNear();
                break;
            }
        }

        Lines.GetIslands();
        StartCoroutine(Lines.SetLines());
    }

    //save current map to AirshipStats
    private void SaveCurrentMap()
    {
        AirshipStats.islandsInformation = new List<IslandInfo>();
        foreach (var isl in islands)
        {
            IslandInfo isle = new IslandInfo(isl.transform.position.x, isl.transform.position.y, isl.GetComponent<NextMap>().canGo, isl.GetComponent<NextMap>().searchNext, isl.GetComponent<NextMap>().goneThere, isl.GetComponent<NextMap>().exit, isl.name, isl.GetComponent<NextMap>().number, isl.GetComponent<NextMap>().mapTarget);
            AirshipStats.islandsInformation.Add(isle);
        }
        AirshipStats.createdMap = true;
    }

    public void SetCanGoBoolsToFalse()
    {
        foreach (var island in islands)
        {
            if (island.GetComponent<NextMap>().canGo)
                island.GetComponent<NextMap>().canGo = false;
        }
    }

    //void Start()
    //{
    //    Lines.GetIslands();
    //    StartCoroutine(Lines.SetLines());
    //}

    public void yesButtonClick()
    {

        buttonToBigMap.SetActive(false);
        noButton.SetActive(false);
        text.SetActive(false);
        SceneManager.LoadScene("TheBigMap");

    }

    public void SetCanvas(GameObject temp1)
    {
        //making alternative button

        temp = temp1;

        if (temp.tag == "Exit")
        {
            buttonToBigMap.SetActive(true);
            noButton.SetActive(true);
            text.SetActive(true);
        }
        else
        {
            Debug.Log("true");
            //canvas.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
            text.SetActive(true);
        }
    }

    public void NewMap(GameObject mapPoint)
    {
        //this is the target for a special encounter
        if (mapPoint.GetComponent<NextMap>().mapTarget)
        {
            mapPoint.GetComponent<NextMap>().mapTarget = false; //gone there so no need to show a target anymore
            AirshipStats.showMapTargetEndingText = true;
        }

        if (mapPoint.CompareTag("Exit"))
        {
            AirshipStats.createdMap = false;
            AirshipStats.RedLinePictureX = -235f;
            AirshipStats.gas += 5; //get 5 gas for getting to the next area
            Debug.Log("Gave the player 5 gas for getting to the next area.");
            AirshipStats.RedLinePictureX = -0f;

            SceneManager.LoadScene("TheBigMap");
        }
        else
        {


            if (mapPoint.GetComponent<NextMap>().goneThere)
            {
                foreach (var i in islands)
                {
                    if (i.GetComponent<NextMap>().searchNext)
                    {
                        i.GetComponent<NextMap>().searchNext = false;
                        i.GetComponent<NextMap>().setGoneThere();
                        break;
                    }
                }
                mapPoint.GetComponent<NextMap>().setGoneThere();
                mapPoint.GetComponent<NextMap>().searchNext = true;
                mapPoint.GetComponent<SpriteRenderer>().sprite = AlreadyWentX;
                Destroy(shipPrefab); //remove our current ship from the map
                SaveCurrentMap();
                SceneManager.LoadScene("Map");

            }
            else
            {
                //set the current island where the player is to false and set the island where the player is going to true
                foreach (var i in islands)
                {
                    if (i.GetComponent<NextMap>().searchNext)
                    {
                        i.GetComponent<NextMap>().searchNext = false;
                        i.GetComponent<NextMap>().setGoneThere();
                        break;
                    }
                }
                mapPoint.GetComponent<NextMap>().setGoneThere();
                mapPoint.GetComponent<NextMap>().searchNext = true;
                mapPoint.GetComponent<SpriteRenderer>().sprite = AlreadyWentX;
                Destroy(shipPrefab); //remove our current ship from the map
                redpic.movePicture();


                SaveCurrentMap();


                if (mapPoint.transform.position.x <= AirshipStats.RedLinePictureX)
                {
                    AirshipStats.badBttleStart = true;
                    AudioManager.instance.turnMusicOff();
                    AirshipStats.battleMusicOn = true;
                    SceneManager.LoadScene("Match3");

                }
                else
                {
                    AirshipStats.gas--; //lose gas everytime we fly to another island
                    AirshipStats.badBttleStart = false;

                    int randomNumber = Random.Range(1, 3);
                    if (AirshipStats.debugging)
                    {
                        if (AirshipStats.alwaysMatch3Scene)
                            randomNumber = 2;
                        else if (AirshipStats.alwaysShipScene)
                            randomNumber = 1; //DEBUG start airship scene everytime
                    }

                    //automatically save the game
                    SaveLoad.Save();

                    if (randomNumber == 1)
                    {
                        SceneManager.LoadScene("Airship0220");
                    }
                    else if (randomNumber == 2)
                    {
                        AirshipStats.battleMusicOn = true;
                        AudioManager.instance.turnMusicOff();
                        SceneManager.LoadScene("Match3");
                    }
                }


            }


        }
    }


    public void Update()
    {

        shipPrefab.transform.RotateAround((target.transform.position + new Vector3(0, 0, 10f)), new Vector3(0, 0, 1), 100 * Time.deltaTime);

        // shipPrefab.transform.Rotate(0, 0, 2);
    }




    public void CloseCanvas()
    {
        Debug.Log("false");

        //canvas.SetActive(false);
        yesButton.SetActive(false);
        buttonToBigMap.SetActive(false);

        noButton.SetActive(false);
        text.SetActive(false);
    }
}