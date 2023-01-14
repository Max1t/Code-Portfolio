using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Matches : MonoBehaviour
{
    public bool playersTurn = true; //If this is false the AI is making a move and if this is true the player is making a move. This is used for knowing who to give the matched blocks to.
    public bool blocksMoving = false;
    public bool blocksWereDestroyed = false;
    public int blocksDestroyed = 0;
    public bool gameReadyToStart = false;
    public float radius = 0.61f;
    public float raycastMaxDistance = 0.9f; // Blocks are 1.2 units away from eachother
    private List<GameObject> allBlocks = new List<GameObject>();  //list that has all the blocks that are in the game
    public List<GameObject> AllBlocks { get { return allBlocks; } }
    private List<GameObject> destroyBlocks = new List<GameObject>(); //list that has all the blocks that will be destroyed
    private List<GameObject> howManySameBlocksUpAndDown = new List<GameObject>(); //list that has blocks that need to be destroyed if amount >= 3
    private List<GameObject> howManySameBlocksLeftAndRight = new List<GameObject>();//list that has blocks that need to be destroyed if amount >= 3
    public GameObject blockExplosionAnimation;
    public GameObject fireBurningAnimation;
    public GameObject enemyBulletPrefab1;
    public GameObject enemyAmmoSpawn;
    public Spawn spawn;
    public Swapper swapper;
    public DetectNoAvailableMoves detector;
    public EnemyAI enemyAI;
    public EnemyBlockStorage enemyBlockStorage;
    public ContactFilter2D contactFilter2D;
    public bool stageReset = false;
    public bool noLegalMoveDetection = true;

    public ResourcesScriptUI resources;
    public int EnemyAttackCounter;

    public GameObject gunRoom;
    public GameObject cockpitRoom;
    public GameObject generatorRoom;
    public GameObject thrustRoom;
    public GameObject mainRoom;
    public GameObject gunMissPosition;
    public GameObject cockpitMissPosition;
    public GameObject generatorMissPosition;
    public GameObject thrustMissPosition;
    public GameObject mainMissPosition;

    private Vector3 shipRoomPosition = Vector3.zero; //position of the room where the enemy shoots
    private GameObject fireBurningGunRoomInstantiated;
    private GameObject fireBurningCockpitRoomInstantiated;
    private GameObject fireBurningGeneratorRoomInstantiated;
    private GameObject fireBurningThrustRoomInstantiated;
    private GameObject fireBurningMainRoomInstantiated;

    public AudioManager audioManager;

    //WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void Start()
    {
        //debugging
        if (AirshipStats.debugging && AirshipStats.skipMatch3Scene)
            SceneManager.LoadScene("Map");

        audioManager = FindObjectOfType<AudioManager>();
        contactFilter2D = new ContactFilter2D();
        //contactFilter2D.SetLayerMask(8);
        StartCoroutine("CheckIfBlocksAreMoving");
    }
    private void timeattack()
    {
        if (AirshipStats.battlePause == false)
        {
            //MoveEnemyBullet();
            enemyAI.AiButtonClicked();
            enemyBlockStorage.AttackPlayer();
            timer = 0f;
        }
    }

    private void AttackTimer(float time)
    {
        if (timer <= 0f) timer = time;

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timeattack();
            }
        }
    }

    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        /*
        //do this only if game board is created and blocks aren't moving
        if (gameReadyToStart && !blocksMoving && !swapper.swappingBlocks)
        {
            //  CheckColorMatches();
            // DestroyBlocks();
            if (EnemyAttackCounter == 3)
            {
                EnemyAttackCounter = 0;
                MoveEnemyBullet();
                //resources.TakeDamage(15);
            }
        }*/

        if (AirshipStats.specialBattle == false)
        {
            if (AirshipStats.battlePause == false)
            {
                if (AirshipStats.badBttleStart)
                {
                    AttackTimer(10f);

                }
                else
                {
                    int randomNumber = Random.Range(1, 4);

                    if (randomNumber == 1)
                    {
                        AttackTimer(20f);
                    }
                    else if (randomNumber == 2)
                    {
                        AttackTimer(25f);
                    }
                    else if (randomNumber == 3)
                    {
                        AttackTimer(30f);
                    }
                }

            }
        }

        if (AirshipStats.specialBattle)
        {
            if (AirshipStats.battlePause == false)
            {
                AttackTimer(5f);

                //do this only if game board is created and blocks aren't moving
                if (gameReadyToStart && !blocksMoving)
                {
                    // CheckColorMatches();
                    // DestroyBlocks();
                    if (EnemyAttackCounter == 3)
                    {
                        EnemyAttackCounter = 0;
                        enemyBlockStorage.AttackPlayer();
                        //MoveEnemyBullet();
                        //resources.TakeDamage(15);
                    }
                }
            }
        }
    }

    private bool CheckIfEnemyHits()
    {
        int hitOrMiss = Random.Range((int)(AirshipStats.cockpitRoomCurrentSteam / 10), 12); //Check if the enemy hits or misses. Depends on the cockpit room's current level and steam
        Debug.Log("randomed hitormiss: " + hitOrMiss);
        if (hitOrMiss == 11)
        {
            Debug.Log("Enemy's attack is going to miss.");
            return false; //missed
        }
        return true; //hit
    }

    public void MoveEnemyBullet()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        GameObject bullet = Instantiate(enemyBulletPrefab1, enemyAmmoSpawn.transform.position, Quaternion.identity);
        bullet.transform.Rotate(0, 0, 90);
        int rand = Random.Range(0, 5);
        bool hit = CheckIfEnemyHits(); //true if enemy hits, false if it misses
        string room = "";
        switch (rand)
        {
            case 0:
                if (hit)
                    shipRoomPosition = gunRoom.transform.position;
                else
                    shipRoomPosition = gunMissPosition.transform.position;
                room = "gun";
                break;
            case 1:
                if (hit)
                    shipRoomPosition = cockpitRoom.transform.position;
                else
                    shipRoomPosition = cockpitMissPosition.transform.position;
                room = "cockpit";
                break;
            case 2:
                if (hit)
                    shipRoomPosition = generatorRoom.transform.position;
                else
                    shipRoomPosition = generatorMissPosition.transform.position;
                room = "generator";
                break;
            case 3:
                if (hit)
                    shipRoomPosition = thrustRoom.transform.position;
                else
                    shipRoomPosition = thrustMissPosition.transform.position;
                room = "thrust";
                break;
            case 4:
                if (hit)
                    shipRoomPosition = mainRoom.transform.position;
                else
                    shipRoomPosition = mainMissPosition.transform.position;
                room = "main";
                break;
        }
        //shipRoomPosition = new Vector3(shipRoomPosition.x, shipRoomPosition.y, shipRoomPosition.z-10f);

        float ammoDamage = 0;

        if (AirshipStats.specialBattle)
        {
            if (AirshipStats.enemyGunBroken == 0)
            {
                ammoDamage = 30;
            }
            else if (AirshipStats.enemyGunBroken == 1)
            {
                ammoDamage = 20;
            }
            else if (AirshipStats.enemyGunBroken == 2)
            {
                ammoDamage = 10;
            }
        }

        if (AirshipStats.specialBattle == false)
        {
            if (AirshipStats.enemyGunBroken == 0)
            {
                if (AirshipStats.badBttleStart)
                {
                    ammoDamage = 25;
                }
                else
                {
                    ammoDamage = 15;

                }

            }
            else if (AirshipStats.enemyGunBroken == 1)
            {
                if (AirshipStats.badBttleStart)
                {
                    ammoDamage = 20;
                }
                else
                {
                    ammoDamage = 10;

                }
            }
            else if (AirshipStats.enemyGunBroken == 2)
            {
                if (AirshipStats.badBttleStart)
                {
                    ammoDamage = 15;
                }
                else
                {
                    ammoDamage = 5;

                }

            }
        }
        float ammoSpeed = 15;
        bullet.GetComponent<BulletMove>().MoveTowardsPosition(shipRoomPosition, ammoDamage, ammoSpeed, room, hit);
        audioManager.Play("Kertalaukaisin");
    }

    public void StartBurningFire(string room)
    {
        if (room == "gun")
        {
            //instantiate only 1 fire per room
            if (fireBurningGunRoomInstantiated == null)
                fireBurningGunRoomInstantiated = Instantiate(fireBurningAnimation, shipRoomPosition, Quaternion.identity);
            AirshipStats.gunRoomOnFire = true;
        }
        else if (room == "cockpit")
        {
            if (fireBurningCockpitRoomInstantiated == null)
                fireBurningCockpitRoomInstantiated = Instantiate(fireBurningAnimation, shipRoomPosition, Quaternion.identity);
            AirshipStats.cockpitRoomOnFire = true;
        }
        else if (room == "generator")
        {
            if (fireBurningGeneratorRoomInstantiated == null)
                fireBurningGeneratorRoomInstantiated = Instantiate(fireBurningAnimation, shipRoomPosition, Quaternion.identity);
            AirshipStats.generatorRoomOnFire = true;
        }
        else if (room == "thrust")
        {
            if (fireBurningThrustRoomInstantiated == null)
                fireBurningThrustRoomInstantiated = Instantiate(fireBurningAnimation, shipRoomPosition, Quaternion.identity);
            AirshipStats.thrustRoomOnFire = true;
        }
        else if (room == "main")
        {
            if (fireBurningMainRoomInstantiated == null)
                fireBurningMainRoomInstantiated = Instantiate(fireBurningAnimation, shipRoomPosition, Quaternion.identity);
            AirshipStats.mainRoomOnFire = true;
        }
    }

    public void StopBurningFire(string room)
    {
        if (room == "gun")
        {
            Destroy(fireBurningGunRoomInstantiated);
        }
        else if (room == "cockpit")
        {
            Destroy(fireBurningCockpitRoomInstantiated);
        }
        else if (room == "generator")
        {
            Destroy(fireBurningGeneratorRoomInstantiated);
        }
        else if (room == "thrust")
        {
            Destroy(fireBurningThrustRoomInstantiated);
        }
        else if (room == "main")
        {
            Destroy(fireBurningMainRoomInstantiated);
        }
    }

    //public void StartCoroutineToCheckForMatches()
    //{
    //    //StartCoroutine("StartCheckingForMatches");
    //    Invoke("CheckColorMatches", 1);
    //}

    //public IEnumerator StartCheckingForMatches()
    //{
    //    while (true)
    //    {
    //        CheckColorMatches();
    //        DestroyBlocks();

    //        yield return waitForSeconds;
    //    }

    //}

    public void CheckColorMatches()
    {
        //go through a list of all the blocks
        foreach (var block in allBlocks)
        {
            if (block.GetComponent<Block>() != null)
                if (block.GetComponent<Block>().matched == true) continue;
            howManySameBlocksUpAndDown.Add(block);
            howManySameBlocksLeftAndRight.Add(block);
            FindMatchesFromNearRaycast(block, "no");
            CreateDestroyList();
        }
    }

    public void FindMatchesFromNearCollider(GameObject block, string direction)
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(block.transform.position, radius);
        foreach (var collider in block.GetComponent<CollisionCheck>().colliders)
        {
            GameObject go = collider.gameObject;
            //iterate through all the blocks that are next to the block that is not the block itself
            //also check that the block is the same color (via tag)
            if (go != block && go.tag == block.tag)
            {
                //this block is on top of the block we are iterating through
                if ((direction == "up" || direction == "no") && go.transform.position.y > (block.transform.position.y + 1f))
                {
                    howManySameBlocksUpAndDown.Add(go);
                    FindMatchesFromNearCollider(go, "up"); //recursive call
                }
                //this block is below of the block we are iterating through
                if ((direction == "down" || direction == "no") && go.transform.position.y < (block.transform.position.y - 1f))
                {
                    howManySameBlocksUpAndDown.Add(go);
                    FindMatchesFromNearCollider(go, "down"); //recursive call
                }
                //this block is on left of the block we are iterating through
                if ((direction == "left" || direction == "no") && go.transform.position.x < (block.transform.position.x - 1f))
                {
                    howManySameBlocksLeftAndRight.Add(go);
                    FindMatchesFromNearCollider(go, "left"); //recursive call
                }
                //this block is on right of the block we are iterating through
                if ((direction == "right" || direction == "no") && go.transform.position.x > (block.transform.position.x + 1f))
                {
                    howManySameBlocksLeftAndRight.Add(go);
                    FindMatchesFromNearCollider(go, "right"); //recursive call
                }
            }
        }
    }

    public void FindMatchesFromNearRaycast(GameObject block, string direction)
    {
        if (direction == "up" || direction == "no")
        {
            RaycastHit2D[] hit = new RaycastHit2D[5];
            int results = Physics2D.Raycast(block.transform.position, Vector2.up, contactFilter2D, hit, 1f);
            if (results != 0)
            {
                foreach (RaycastHit2D target in hit)
                {
                    if (target.transform != null)
                    {
                        if (target.transform.tag == block.tag && target.transform.gameObject != block)
                        {
                            howManySameBlocksUpAndDown.Add(target.transform.gameObject);
                            FindMatchesFromNearRaycast(target.transform.gameObject, "up");
                            break;
                        }
                    }
                }
            }
        }
        if (direction == "down" || direction == "no")
        {

            RaycastHit2D[] hit = new RaycastHit2D[5];
            int results = Physics2D.Raycast(block.transform.position, Vector2.down, contactFilter2D, hit, 1f);
            if (results != 0)
            {
                foreach (RaycastHit2D target in hit)
                {
                    if (target.transform != null)
                    {
                        if (target.transform.tag == block.tag && target.transform.gameObject != block)
                        {
                            howManySameBlocksUpAndDown.Add(target.transform.gameObject);
                            FindMatchesFromNearRaycast(target.transform.gameObject, "down");
                            break;
                        }
                    }
                }
            }
        }
        if (direction == "right" || direction == "no")
        {
            RaycastHit2D[] hit = new RaycastHit2D[5];
            int results = Physics2D.Raycast(block.transform.position, Vector2.right, contactFilter2D, hit, 1f);
            if (results != 0)
            {
                foreach (RaycastHit2D target in hit)
                {
                    if (target.transform != null)
                    {
                        if (target.transform.tag == block.tag && target.transform.gameObject != block)
                        {
                            howManySameBlocksLeftAndRight.Add(target.transform.gameObject);
                            FindMatchesFromNearRaycast(target.transform.gameObject, "right");
                            break;
                        }
                    }
                }
            }
        }
        if (direction == "left" || direction == "no")
        {
            RaycastHit2D[] hit = new RaycastHit2D[5];
            int results = Physics2D.Raycast(block.transform.position, Vector2.left, contactFilter2D, hit, 1f);
            if (results != 0)
            {
                foreach (RaycastHit2D target in hit)
                {
                    if (target.transform != null)
                    {
                        if (target.transform.tag == block.tag && target.transform.gameObject != block)
                        {
                            howManySameBlocksLeftAndRight.Add(target.transform.gameObject);
                            FindMatchesFromNearRaycast(target.transform.gameObject, "left");
                            break;
                        }
                    }
                }
            }
        }
    }

    public GameObject GetBlockUp(GameObject block)
    {
        RaycastHit2D[] hit = new RaycastHit2D[5];
        int results = Physics2D.Raycast(block.transform.position, Vector2.up, contactFilter2D, hit, 1f);
        if (results != 0)
        {
            foreach (RaycastHit2D target in hit)
            {
                if (target.transform != null)
                {
                    if (target.transform.gameObject != block)
                    {
                        return target.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    public GameObject GetBlockDown(GameObject block)
    {
        RaycastHit2D[] hit = new RaycastHit2D[5];
        int results = Physics2D.Raycast(block.transform.position, Vector2.down, contactFilter2D, hit, 1f);
        if (results != 0)
        {
            foreach (RaycastHit2D target in hit)
            {
                if (target.transform != null)
                {
                    if (target.transform.gameObject != block)
                    {
                        return target.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    public GameObject GetBlockLeft(GameObject block)
    {
        RaycastHit2D[] hit = new RaycastHit2D[5];
        int results = Physics2D.Raycast(block.transform.position, Vector2.left, contactFilter2D, hit, 1f);
        if (results != 0)
        {
            foreach (RaycastHit2D target in hit)
            {
                if (target.transform != null)
                {
                    if (target.transform.gameObject != block)
                    {
                        return target.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    public GameObject GetBlockRight(GameObject block)
    {
        RaycastHit2D[] hit = new RaycastHit2D[5];
        int results = Physics2D.Raycast(block.transform.position, Vector2.right, contactFilter2D, hit, 1f);
        if (results != 0)
        {
            foreach (RaycastHit2D target in hit)
            {
                if (target.transform != null)
                {
                    if (target.transform.gameObject != block)
                    {
                        return target.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    // Version for inputting specific blocks to destroy. Adds all blocks to destroy list in the list parameter
    public int CreateDestroyList(List<GameObject> list)
    {
        foreach (GameObject block in list)
        {
            if (!destroyBlocks.Contains(block)) destroyBlocks.Add(block);
            MatchTextEffect.instance.SpawnText(1, block.tag, block.transform.position);
        }
        int amountDestroyed = DestroyBlocks(true);
        howManySameBlocksUpAndDown = new List<GameObject>();
        howManySameBlocksLeftAndRight = new List<GameObject>();

        return amountDestroyed;
    }

    //if matches of 3 and more have happened then destroy those blocks
    public void CreateDestroyList()
    {
        /* 
        if (stageReset)
        {
            foreach (GameObject block in allBlocks)
            {
                destroyBlocks.Add(block);
            }
            howManySameBlocksUpAndDown = new List<GameObject>();
            howManySameBlocksLeftAndRight = new List<GameObject>();
            stageReset = false;
            return;
        }
        */

        if (howManySameBlocksUpAndDown.Count >= 3)
        {
            //  Debug.Log("Match of " + howManySameBlocksUpAndDown.Count + " happened vertically");
            MatchTextEffect.instance.CollectList(howManySameBlocksUpAndDown);
            foreach (GameObject block in howManySameBlocksUpAndDown)
            {
                //Debug.Log("Destroy block: " + block.name + " " + block.tag);
                if (!destroyBlocks.Contains(block)) //if the block is not in the list
                {
                    destroyBlocks.Add(block);
                    if (block.GetComponent<Block>() != null)
                        block.GetComponent<Block>().matched = true;
                }
            }
        }

        if (howManySameBlocksLeftAndRight.Count >= 3)
        {
            //  Debug.Log("Match of " + howManySameBlocksLeftAndRight.Count + " happened horizontally");
            MatchTextEffect.instance.CollectList(howManySameBlocksLeftAndRight);

            foreach (GameObject block in howManySameBlocksLeftAndRight)
            {
                //Debug.Log("Destroy block: " + block.name + " " + block.tag);
                if (!destroyBlocks.Contains(block)) //if the block is not in the list
                {
                    destroyBlocks.Add(block);
                    if (block.GetComponent<Block>() != null)
                        block.GetComponent<Block>().matched = true;
                }

            }
        }

        howManySameBlocksUpAndDown = new List<GameObject>();
        howManySameBlocksLeftAndRight = new List<GameObject>();
    }

    public int DestroyBlocks(bool abilityUsed)
    {
        int amountDestroyed = 0;
        //list is not empty so we set previous clicks to null so that we do not try to access gameobjects that are destroyed
        if (destroyBlocks.Any())
        {
            EnemyAttackCounter++;
            blocksWereDestroyed = true;
            swapper.previousFirstClick = null;
            swapper.previousSecondClick = null;
            if (!abilityUsed)
            {
                MatchTextEffect.instance.ResolveLists();
            }
        }



        foreach (GameObject block in destroyBlocks.Reverse<GameObject>())
        {
            blocksDestroyed++;
            amountDestroyed++;
            allBlocks.Remove(block);
            //destroyBlocks.Remove(block);
            spawn.CreateABlock(block); //create another block where the previous one was deleted

            if (block.tag != "Damage")
            {
                Destroy(Instantiate(blockExplosionAnimation, block.transform.position, Quaternion.identity), 0.5f);
                if (stageReset) block.GetComponent<BlockClick>().stageReset = true;
                Destroy(block);
            }
            else
            {
                Destroy(Instantiate(block.GetComponent<DamageBlockClick>().blockExplosion, block.transform.position, Quaternion.identity), 1f);
                if (stageReset) block.GetComponent<DamageBlockClick>().stageReset = true;
                Destroy(block);
            }
        }
        destroyBlocks = new List<GameObject>();
        return amountDestroyed;
    }

    public void AddBlock(GameObject block)
    {
        allBlocks.Add(block);
    }

    public void FreezeBlocks()
    {
        foreach (var block in allBlocks)
        {
            block.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void UnFreezeBlocks()
    {
        foreach (var block in allBlocks)
        {
            block.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    //bool CheckIfBlocksAreMoving()
    //{
    //    foreach(var block in allBlocks)
    //    {
    //        if (block.GetComponent<Rigidbody2D>().velocity.y > 0.05f)
    //            return true;
    //    }

    //    return false;
    //}

    public IEnumerator CheckIfBlocksAreMoving()
    {
        while (true)
        {

            bool checkMoving = false;

            if (stageReset)
            {
                CreateDestroyList(allBlocks);
                stageReset = false;
            }

            swapper.CheckParticles();

            foreach (var block in allBlocks)
            {
                if (block.GetComponent<Block>() != null)
                {
                    if (block.GetComponent<Block>().IsMoving)
                    {
                        blocksMoving = true;
                        checkMoving = true;
                        noLegalMoveDetection = true;
                    }
                }
            }
            if (checkMoving == false)
            {
                if (noLegalMoveDetection)
                {

                    if (!detector.DetectNoLegalMoves() && !detector.nullCheck) stageReset = false;
                    else stageReset = true;

                    CheckColorMatches();
                    DestroyBlocks(false);
                    noLegalMoveDetection = false;
                }
                blocksMoving = false;
            }
            yield return new WaitForSeconds(0.10f);
        }
    }

    //check for matches after the AI has moved blocks
    public IEnumerator MatchAfterEnemyAIMove()
    {
        playersTurn = false; //make it AI's turn
        yield return new WaitForSeconds(0.2f);
        CheckColorMatches();
        DestroyBlocks(false);
    }

    //AI's turn to swap blocks
    public IEnumerator CallEnemyAIToMakeAMove()
    {
        yield return new WaitForSeconds(0.5f); //make sure that blocksMoving is checked before we AI checks if blocks are moving because of the human's move
        while (blocksMoving) //do the match after blocks have stopped moving
        {
            yield return new WaitForSeconds(0.5f);
        }
        enemyAI.CreateTableOfAllBlocks();
    }
}
