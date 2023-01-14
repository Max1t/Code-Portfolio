using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClick : Block
{
    public Matches matches;
    public Swapper swap;
    public BlockStorageScript storage;
    public EnemyBlockStorage enemyStorage;
    private bool isBeingMoved;
    private Vector3 lastFrameTransform;
    public GameObject blockExplosion;

    public bool stageReset;


    void Start()
    {
        swap = GameObject.Find("Swap").GetComponent<Swapper>();
        storage = GameObject.Find("BlockStorage").GetComponent<BlockStorageScript>();
        enemyStorage = FindObjectOfType<EnemyBlockStorage>();
        matches = FindObjectOfType<Matches>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swap.SwapBlocks(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (storage != null && !stageReset && matches.playersTurn) //player's turn so he gets the blocks
        {
            storage.addToBlockStorage(gameObject.tag);
            if (BezierCurveController.instance != null)
                BezierCurveController.instance.TriggerEffect(transform);

        }
        else if (storage != null && !stageReset && !matches.playersTurn) //AI's turn so it gets the blocks
        {
            enemyStorage.AddToBlockStorage(gameObject.tag);
        }

    }

    void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, lastFrameTransform) > 0.01f)
        {
            IsMoving = true;
            Timer(5f);

        }
        else
        {
            if (IsMoving)
                IsMoving = false;
        }

        lastFrameTransform = transform.position;
    }


    private void Timer(float time)
    {
        float timer = 0f;

        if (timer <= 0f) timer = time;

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Timer 1 Working");

                IsMoving = false;
            }
        }
    }
}
