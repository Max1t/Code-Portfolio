using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Matches matches;
    private int largestMatch = 0;
    private int largestMatchUpDown = 0;
    private int largestMatchLeftRight = 0;
    private bool nullCheck = false;
    private bool startAIagain = false;
    public GameObject instanciatedHighlightParticles;
    public GameObject firstBlockLocation;
    public GameObject startingBlock;
    private GameObject[,] allCurrentBlocksInOrder;
    private char[, ] asciiTableOfAllBlocksInOrder;
    public Button buttonEnemyAiSwap;

    private bool playerTouches = false;
    
    public float radius = 0.61f;
    public float raycastMaxDistance = 0.9f; // Blocks are 1.2 units away from eachother
    private List<GameObject> howManySameBlocksUpAndDown = new List<GameObject>(); //list that has blocks that need to be destroyed if amount >= 3
    private List<GameObject> howManySameBlocksLeftAndRight = new List<GameObject>();//list that has blocks that need to be destroyed if amount >= 3
    public Spawn spawn;
    public Swapper swapper;
    public DetectNoAvailableMoves detector;
    public ContactFilter2D contactFilter2D;

    private void Start()
    {
        buttonEnemyAiSwap.onClick.AddListener(() => AiButtonClicked());
        contactFilter2D = new ContactFilter2D();
        if (!AirshipStats.debugging)
            buttonEnemyAiSwap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startAIagain)
        {
            startAIagain = false;
            CreateTableOfAllBlocks();
        }
    }

    public void AiButtonClicked()
    {
        CreateTableOfAllBlocks();
    }
    
    public void CreateTableOfAllBlocks()
    {
        RaycastHit2D hit = Physics2D.CircleCast(firstBlockLocation.transform.position, 0.3f, Vector2.zero);
        if (hit.collider)
        {
            startingBlock = hit.collider.gameObject;
        }
        else
        {
            Debug.Log("return;");
            //CreateTableOfAllBlocks();
            return;
        }

        allCurrentBlocksInOrder = FindAllInOrder(startingBlock);
        if (nullCheck)
        {
            nullCheck = false;
            startAIagain = true;
            return;
        }
        string[] gameBoard = new string[8];
        for (int y = 7; y >= 0; y--)
        {
            gameBoard[y] = "" + allCurrentBlocksInOrder[0, y].tag+"-" + allCurrentBlocksInOrder[1, y].tag + "-" + allCurrentBlocksInOrder[2, y].tag + "-" + allCurrentBlocksInOrder[3, y].tag + "-" + allCurrentBlocksInOrder[4, y].tag + "-" + allCurrentBlocksInOrder[5, y].tag + "-" + allCurrentBlocksInOrder[6, y].tag + "-" + allCurrentBlocksInOrder[7, y].tag + "-";
        }
        Debug.Log(gameBoard[7] + "\n" + gameBoard[6] + "\n" + gameBoard[5] + "\n" + gameBoard[4] + "\n" + gameBoard[3] + "\n" + gameBoard[2] + "\n" + gameBoard[1] + "\n" + gameBoard[0]);
        
        asciiTableOfAllBlocksInOrder = new char[8, 8];
        
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                char c = 'x';
                if (allCurrentBlocksInOrder[x, y].CompareTag("Blue"))
                    c = 'b';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("Damage"))
                    c = 'd';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("Green"))
                    c = 'g';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("Red"))
                    c = 'r';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("Violet"))
                    c = 'v';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("White"))
                    c = 'w';
                else if (allCurrentBlocksInOrder[x, y].CompareTag("Yellow"))
                    c = 'y';
                asciiTableOfAllBlocksInOrder[x, y] = c;
            }
        }

        //write the table of blocks to debug log
        //string[] gameBoard = new string[8];
        gameBoard = new string[8];
        for (int y = 7; y >=0; y--)
        {
            gameBoard[y] = "" + asciiTableOfAllBlocksInOrder[0, y] + asciiTableOfAllBlocksInOrder[1, y] + asciiTableOfAllBlocksInOrder[2, y] + asciiTableOfAllBlocksInOrder[3, y] + asciiTableOfAllBlocksInOrder[4, y] + asciiTableOfAllBlocksInOrder[5, y] + asciiTableOfAllBlocksInOrder[6, y] + asciiTableOfAllBlocksInOrder[7, y];
        }
        Debug.Log(gameBoard[7] + "\n" + gameBoard[6] + "\n" + gameBoard[5] + "\n" + gameBoard[4] + "\n" + gameBoard[3] + "\n" + gameBoard[2] + "\n" + gameBoard[1] + "\n" + gameBoard[0]);
        GoThroughAllPossibleSwaps();
    }

    public GameObject[,] FindAllInOrder(GameObject startingBlock)
    {
        GameObject[,] tempArray = new GameObject[8, 8];
        tempArray[0, 0] = startingBlock;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (x == 7 && y == 7)
                {
                    continue;
                }
                if (y == 7)
                {
                    if(matches.GetBlockRight(tempArray[x, 0]) == null)
                    {
                        nullCheck = true;
                        return tempArray;
                    }
                    tempArray[x + 1, 0] = matches.GetBlockRight(tempArray[x, 0]);
                }
                else
                {
                    if (matches.GetBlockUp(tempArray[x, y]) == null)
                    {
                        nullCheck = true;
                        return tempArray;
                    }
                    tempArray[x, y + 1] = matches.GetBlockUp(tempArray[x, y]);
                }
            }
        }
        return tempArray;
    }

    private void GoThroughAllPossibleSwaps()
    {
        char[,] tempArray = new char[8, 8];
        char tempChar = 'x';
        int match = 0;
        int startingPositionX = 0; //coordinates for the block that we move to get the biggest match
        int startingPositionY = 0;
        int endingPositionX = 0; //coordinates of the position where the block is moved
        int endingPositionY = 0;
        largestMatch = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tempChar = asciiTableOfAllBlocksInOrder[x, y];
                //go through all the possible swaps for the block
                if (y + 1 < 8) //can move up
                {
                    //make sure the blocks are of different color so that we don't swap the same colored blocks
                    if (asciiTableOfAllBlocksInOrder[x, y] != asciiTableOfAllBlocksInOrder[x, y + 1]) 
                    {
                        tempArray = (char[,])asciiTableOfAllBlocksInOrder.Clone();
                        tempArray[x, y] = tempArray[x, y + 1];
                        tempArray[x, y + 1] = tempChar;

                        match = BiggestMatchInAsciiTable(tempArray);
                        if (match > largestMatch)
                        {
                            largestMatch = match;
                            startingPositionX = x; 
                            startingPositionY = y;
                            endingPositionX = x;
                            endingPositionY = y+1;
                        }
                    }
                }
                if (y - 1 >= 0) //can move down
                {
                    if (asciiTableOfAllBlocksInOrder[x, y] != asciiTableOfAllBlocksInOrder[x, y - 1]) //different color blocks
                    {
                        tempArray = (char[,])asciiTableOfAllBlocksInOrder.Clone();
                        tempArray[x, y] = tempArray[x, y - 1];
                        tempArray[x, y - 1] = tempChar;

                        match = BiggestMatchInAsciiTable(tempArray);
                        if (match > largestMatch)
                        {
                            largestMatch = match;
                            startingPositionX = x;
                            startingPositionY = y;
                            endingPositionX = x;
                            endingPositionY = y - 1;
                        }
                    }
                }
                if (x + 1 < 8) //can move right
                {
                    if (asciiTableOfAllBlocksInOrder[x, y] != asciiTableOfAllBlocksInOrder[x + 1, y]) //different color blocks
                    {
                        tempArray = (char[,])asciiTableOfAllBlocksInOrder.Clone();
                        tempArray[x, y] = tempArray[x + 1, y];
                        tempArray[x + 1, y] = tempChar;

                        match = BiggestMatchInAsciiTable(tempArray);
                        if (match > largestMatch)
                        {
                            largestMatch = match;
                            startingPositionX = x;
                            startingPositionY = y;
                            endingPositionX = x + 1;
                            endingPositionY = y;
                        }
                    }
                }
                if (x - 1 >= 0) //can move left
                {
                    if (asciiTableOfAllBlocksInOrder[x, y] != asciiTableOfAllBlocksInOrder[x - 1, y]) //different color blocks
                    {
                        tempArray = (char[,])asciiTableOfAllBlocksInOrder.Clone();
                        tempArray[x, y] = tempArray[x - 1, y];
                        tempArray[x - 1, y] = tempChar;

                        match = BiggestMatchInAsciiTable(tempArray);
                        if (match > largestMatch)
                        {
                            largestMatch = match;
                            startingPositionX = x;
                            startingPositionY = y;
                            endingPositionX = x - 1;
                            endingPositionY = y;
                        }
                    }
                }
            }
        }
        Debug.Log("Best match found by AI: " + largestMatch);
        DoTheBestSwap(startingPositionX, startingPositionY, endingPositionX, endingPositionY);
    }


    private void OnMouseDown()
    {
        playerTouches = true;
    }

    private void OnMouseUp()
    {
        playerTouches = false;

    }

    private void DoTheBestSwap(int startingPositionX, int startingPositionY, int endingPositionX, int endingPositionY)
    {
        RaycastHit2D hit = Physics2D.CircleCast(firstBlockLocation.transform.position, 0.3f, Vector2.zero);
        if (hit.collider)
        {
            startingBlock = hit.collider.gameObject;
        }
        else
        {
            Debug.Log("return;");
            return;
        }

        GameObject[,] tempArray = new GameObject[8, 8]; //array that has all the blocks of the current game situation in order
        tempArray = FindAllInOrder(startingBlock);
        
        //swap the blocks
        Vector3 tempPos = tempArray[startingPositionX, startingPositionY].transform.position;
        //Instantiate particles to show which 2 blocks are being swapped.
        if(playerTouches == false)
        {

            Destroy((GameObject)Instantiate(instanciatedHighlightParticles, tempPos, Quaternion.identity), 2f);
            Destroy((GameObject)Instantiate(instanciatedHighlightParticles, tempArray[endingPositionX, endingPositionY].transform.position, Quaternion.identity), 2f);
        }
        tempArray[startingPositionX, startingPositionY].transform.position = tempArray[endingPositionX, endingPositionY].transform.position;
        tempArray[endingPositionX, endingPositionY].transform.position = tempPos;
        Debug.Log("AI swapped from (" + (startingPositionX+1) + "," + (startingPositionY+1) + ") to (" + (endingPositionX+1) + "," + (endingPositionY+1) + ")");
        StartCoroutine(matches.MatchAfterEnemyAIMove());
    }

    //return the highest match from the ascii table
    private int BiggestMatchInAsciiTable(char[,] asciiTable)
    {
        int highestMatch = 0;
        largestMatchUpDown = 1;
        largestMatchLeftRight = 1;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                FindMatchesFromNearAscii(y, x, "no", asciiTable);

                if (largestMatchLeftRight >= largestMatchUpDown)
                {
                    if (largestMatchLeftRight >= 3)
                        highestMatch = largestMatchLeftRight;
                }
                else
                {
                    if (largestMatchUpDown >= 3)
                        highestMatch = largestMatchUpDown;
                }
                largestMatchUpDown = 1;
                largestMatchLeftRight = 1;
            }
        }
        //Debug.Log("Highest match for this swap found by AI: " + highestMatch);
        return highestMatch;
    }

    private void FindMatchesFromNearAscii(int y, int x, string direction, char[,] asciiTable)
    {
        //check that x and y are on the gameBoard
        if(y >= 0 && y < 8 && x >= 0 && x < 8)
        {
            if ((direction == "up" || direction == "no") && y + 1 < 8) //up
            {
                if(asciiTable[x,y] == asciiTable[x, y + 1]) //same color blocks
                {
                    largestMatchUpDown++;
                    FindMatchesFromNearAscii(y + 1, x, "up", asciiTable);
                }
            }
            if((direction == "down" || direction == "no") && y - 1 >= 0) //down
            {
                if (asciiTable[x, y] == asciiTable[x, y - 1]) //same color blocks
                {
                    largestMatchUpDown++;
                    FindMatchesFromNearAscii(y - 1, x, "down", asciiTable);
                }
            }
            if((direction == "right" || direction == "no") && x + 1 < 8) //right
            {
                if (asciiTable[x, y] == asciiTable[x + 1, y]) //same color blocks
                {
                    largestMatchLeftRight++;
                    FindMatchesFromNearAscii(y, x + 1, "right", asciiTable);
                }
            }
            if((direction == "left" || direction == "no") && x - 1 >= 0) //left
            {
                if (asciiTable[x, y] == asciiTable[x - 1, y]) //same color blocks
                {
                    largestMatchLeftRight++;
                    FindMatchesFromNearAscii(y, x - 1, "left", asciiTable);
                }
            }
        }
    }
}
