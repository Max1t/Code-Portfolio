using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectNoAvailableMoves : MonoBehaviour
{
    public Matches matches;
    public GameObject startingBlock;

    public GameObject[,] AllCurrentBlocksInOrder;
    public GameObject firstBlockLocation;

    public bool nullCheck = false;
    public bool startDetectAgain = false;


    void Update()
    {
        if (startDetectAgain)
        {
            startDetectAgain = false;
            matches.stageReset = DetectNoLegalMoves();
        }
    }
    // Returns true if no legal moves
    public bool DetectNoLegalMoves()
    {
        RaycastHit2D Hit = Physics2D.CircleCast(firstBlockLocation.transform.position, 0.3f, Vector2.zero);
        if (Hit.collider)
        {
            startingBlock = Hit.collider.gameObject;
        }
        else return false;
        AllCurrentBlocksInOrder = FindAllInOrder(startingBlock);

        if (nullCheck)
        {
            nullCheck = false;
            startDetectAgain = true;
            return false;
        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (TestBlockMatches(AllCurrentBlocksInOrder, x, y)) return false;
            }
        }
        return true;
    }

    public List<GameObject> BlockArrayTolist()
    {
        List<GameObject> temp = new List<GameObject>();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                temp.Add(AllCurrentBlocksInOrder[x, y]);
            }
        }
        return temp;
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
                    if (matches.GetBlockRight(tempArray[x, 0]) == null)
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

    // Tests all possible moves for a block for a match. If a match would occur it returns true
    public bool TestBlockMatches(GameObject[,] GoArray, int index_x, int index_y)
    {
        string targetTag = GoArray[index_x, index_y].tag;
        // Method does not move any blocks in the game nor in the array just looks at the blocks at the correct indexes for array given as parameter
        // 
        // Test UP
        if (index_y + 1 < 8)
        {
            int howManyUpDown = 1;
            int howManyLeftRight = 1;
            int simulatedPositionIndex_y = index_y + 1;
            // Up
            for (int i = 1; i < 8 - simulatedPositionIndex_y; i++)
            {
                if (GoArray[index_x, simulatedPositionIndex_y + i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            // Left
            for (int i = 1; i < index_x + 1; i++)
            {
                if (GoArray[index_x - i, simulatedPositionIndex_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            //Right
            for (int i = 1; i < 8 - index_x; i++)
            {
                if (GoArray[index_x + i, simulatedPositionIndex_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            if (howManyLeftRight >= 3 || howManyUpDown >= 3) return true;
        }


        // Test DOWN
        if (index_y - 1 > -1)
        {
            int howManyUpDown = 1;
            int howManyLeftRight = 1;
            int simulatedPositionIndex_y = index_y - 1;
            // Down
            for (int i = 1; i < simulatedPositionIndex_y; i++)
            {
                if (GoArray[index_x, simulatedPositionIndex_y - i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            // Left
            for (int i = 1; i < index_x + 1; i++)
            {
                if (GoArray[index_x - i, simulatedPositionIndex_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            for (int i = 1; i < 8 - index_x; i++)
            {
                if (GoArray[index_x + i, simulatedPositionIndex_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            if (howManyLeftRight >= 3 || howManyUpDown >= 3) return true;
        }


        // Test LEFT
        if (index_x - 1 > -1)
        {
            int howManyUpDown = 1;
            int howManyLeftRight = 1;
            int simulatedPositionIndex_x = index_x - 1;
            //Up
            for (int i = 1; i < 8 - index_y; i++)
            {
                if (GoArray[simulatedPositionIndex_x, index_y + i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            //Down
            for (int i = 1; i < index_y + 1; i++)
            {
                if (GoArray[simulatedPositionIndex_x, index_y - i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            // Left
            for (int i = 1; i < simulatedPositionIndex_x + 1; i++)
            {
                if (GoArray[simulatedPositionIndex_x - i, index_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            if (howManyLeftRight >= 3 || howManyUpDown >= 3) return true;
        }


        // Test RIGHT
        if (index_x + 1 < 8)
        {
            int howManyUpDown = 1;
            int howManyLeftRight = 1;
            int simulatedPositionIndex_x = index_x + 1;
            // Up
            for (int i = 1; i < 8 - index_y; i++)
            {
                if (GoArray[simulatedPositionIndex_x, index_y + i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            // Down
            for (int i = 1; i < index_y + 1; i++)
            {
                if (GoArray[simulatedPositionIndex_x, index_y - i].tag == targetTag)
                {
                    howManyUpDown++;
                }
                else break;
            }
            // Right
            for (int i = 1; i < 8 - simulatedPositionIndex_x; i++)
            {
                if (GoArray[simulatedPositionIndex_x + i, index_y].tag == targetTag)
                {
                    howManyLeftRight++;
                }
                else break;
            }
            if (howManyLeftRight >= 3 || howManyUpDown >= 3) return true;
        }

        return false;
    }

    IEnumerator DebugFlashArray()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Debug.DrawRay(AllCurrentBlocksInOrder[x, y].transform.position, Vector3.right, Color.cyan, 0.25f);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 10 + 75, Screen.height / 2, Screen.width / 10 + 50, Screen.height / 20), "TestAllocateBlocs"))
            DetectNoLegalMoves();
        if (GUI.Button(new Rect(Screen.width / 10 + 75, Screen.height / 2 + 25, Screen.width / 10 + 50, Screen.height / 20), "TestFlashAllBlocs"))
            StartCoroutine(DebugFlashArray());
        if (GUI.Button(new Rect(Screen.width / 10 + 75, Screen.height / 2 + 50, Screen.width / 10 + 50, Screen.height / 20), "Reset Stage"))
            matches.CreateDestroyList(BlockArrayTolist());
    }
    */



}
