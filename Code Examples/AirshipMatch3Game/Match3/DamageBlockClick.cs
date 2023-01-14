using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlockClick : Block
{
    public Matches matches;
    public Swapper swap;
    public BlockStorageScript storage;
    public EnemyHealthMatch3 enemyHealth;
    public ResourcesScriptUI resources;
    private bool isBeingMoved;
    private Vector3 lastFrameTransform;
    public GameObject enemy;
    public GameObject blockExplosion;

    public bool stageReset;


    public AudioManager audioManager;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        swap = GameObject.Find("Swap").GetComponent<Swapper>();
        storage = GameObject.Find("BlockStorage").GetComponent<BlockStorageScript>();
        enemyHealth = GameObject.Find("Enemy").GetComponent<EnemyHealthMatch3>();
        matches = FindObjectOfType<Matches>();
        resources = FindObjectOfType<ResourcesScriptUI>();
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
        if (enemyHealth != null && !stageReset && matches.playersTurn) //player's turn so he gets the blocks
        {
            enemyHealth.TakeDamage(25f);
            audioManager.Play("Rajahdys");
            if (BezierCurveController.instance != null)
                BezierCurveController.instance.TriggerEffect(transform);
        }


        else if (storage != null && !stageReset && !matches.playersTurn) //AI's turn so it gets the blocks
        {
            resources.TakeDamage(1f);
            if (audioManager != null)
                audioManager.Play("Rajahdys");

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
