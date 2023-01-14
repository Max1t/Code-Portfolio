using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    public Matches matches;
    public GameObject highlightParticles;
    public GameObject instanciatedHighlightParticles;
    public GameObject firstClick = null;
    public GameObject secondClick = null;
    public GameObject previousFirstClick = null;
    public GameObject previousSecondClick = null;
    Vector3 firstPos;
    float timer = 0f;

    public AudioManager audioManager;

    //  private float radius = 0.7f; //radius for casting a circle that is used for checking for other blocks that are next to it

    public bool canClick = true;
    public bool swappingBlocks = false;

    public void ResetClicks()
    {
        if (firstClick != null) firstClick = null;
        if (secondClick != null) secondClick = null;
        if (instanciatedHighlightParticles != null) Destroy(instanciatedHighlightParticles);
    }

    public void CheckParticles()
    {
        if (firstClick == null)
            if (instanciatedHighlightParticles != null)
                Destroy(instanciatedHighlightParticles);
    }



    private void Timer(float time, GameObject block)
    {

        if (timer <= 0f) timer = time;

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            Debug.Log("Time: " + timer);

            if (timer <= 0)
            {
                Debug.Log("Got Here");

                if (matches.blocksMoving)
                {
                    block.GetComponent<Rigidbody2D>().isKinematic = true;
                    Debug.Log("Rigidbody should already be kinematic");
                    matches.blocksMoving = false;
                }

            }
        }
    }

    public void SwapBlocks(GameObject block)
    {

        if (AirshipStats.battlePause == false)
        {

            matches.playersTurn = true; //back to player's turn so he gets the matched blocks to his storage
            if (firstClick == null && canClick)
            {
                if (matches.blocksMoving)
                {
                    Debug.Log("BLOCKS STILL MOVING! CAN'T CLICK YET");
                    Debug.Log("Timer ON");
                    Timer(3, block);
                }
                else
                {
                    firstClick = block;
                    instanciatedHighlightParticles = (GameObject)Instantiate(highlightParticles, firstClick.transform.position, Quaternion.identity);
                    Debug.Log("first: " + block.gameObject.name);
                }
            }
            else if (secondClick == null && canClick)
            {
                Destroy(instanciatedHighlightParticles);
                if (matches.blocksMoving)
                {
                    Debug.Log("BLOCKS STILL MOVING! CAN'T CLICK YET");
                }
                else
                {
                    // audioManager.Play("Match");
                    AudioManager.instance.Play("Match");
                    Debug.Log("second: " + block.gameObject.name);
                    secondClick = block;
                    bool clickedOnANonSwappableBlock = true;

                    if (matches.GetBlockUp(firstClick) == secondClick)
                    {
                        clickedOnANonSwappableBlock = false;
                    }
                    if (matches.GetBlockDown(firstClick) == secondClick)
                    {
                        clickedOnANonSwappableBlock = false;
                    }
                    if (matches.GetBlockLeft(firstClick) == secondClick)
                    {
                        clickedOnANonSwappableBlock = false;
                    }
                    if (matches.GetBlockRight(firstClick) == secondClick)
                    {
                        clickedOnANonSwappableBlock = false;
                    }
                    if (!clickedOnANonSwappableBlock)
                    {
                        swappingBlocks = true;
                        matches.blocksWereDestroyed = false;
                        firstPos = firstClick.transform.position;
                        firstClick.transform.position = secondClick.transform.position;
                        secondClick.transform.position = firstPos;
                        previousFirstClick = firstClick;
                        previousSecondClick = secondClick;

                        firstClick = null;
                        secondClick = null;
                        StartCoroutine(Wait(0.5f));
                    }

                    if (clickedOnANonSwappableBlock)
                    {
                        firstClick = null;
                        secondClick = null;
                    }
                }
            }

        }
    }

    //check if the block movement matched at least 3 blocks
    public void CheckForDestroyedBlocks()
    {
        matches.CheckColorMatches();
        matches.DestroyBlocks(false);
        if (!matches.blocksWereDestroyed) //match did not happen
        {
            matches.FreezeBlocks();
            Debug.Log("swap back");
            firstPos = previousFirstClick.transform.position;
            previousFirstClick.transform.position = previousSecondClick.transform.position;
            previousSecondClick.transform.position = firstPos;
            Invoke("StopUnFreeze", 0.1f);
            swappingBlocks = false;
        }
        else
        {
            //matches.blocksMoving = true;
            swappingBlocks = false;
            //audio.Play("Match");

            StartCoroutine(matches.CallEnemyAIToMakeAMove()); //we made a succesful match so now it's the AI's turn

        }
    }

    void StopUnFreeze()
    {
        matches.UnFreezeBlocks();
    }

    //wait for seconds in another thread
    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        Debug.Log("Start Wait() function. The time is: " + Time.time);
        Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(0.5f);   //Wait
        Debug.Log("End Wait() function and the time is: " + Time.time);
        CheckForDestroyedBlocks();
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

}

