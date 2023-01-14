using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Abilities : MonoBehaviour
{
    // Start is called before the first frame update
    public Swapper swapper;
    public Matches matches;
    public EnemyHealthMatch3 enemyHealth;
    public LayerMask blockLayerMask;

    void Start()
    {
        blockLayerMask = LayerMask.GetMask("Block");
    }

    public void DestroySingle(GameObject block)
    {
        List<GameObject> DestroyList = new List<GameObject>();
        if (!DestroyList.Contains(block))
        {
            DestroyList.Add(block);
        }
        matches.CreateDestroyList(DestroyList);
    }

    public void DestroyColumnAndRow(GameObject block)
    {
        DestroyColumn(block);
        DestroyRow(block);
    }
    public void DestroyRow(GameObject block)
    {
        List<GameObject> DestroyList = new List<GameObject>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(blockLayerMask);
        RaycastHit2D[] results = new RaycastHit2D[30];
        Vector2 BoxCastOffset = new Vector2(block.transform.position.x - 11, block.transform.position.y);
        Vector3 BoxCastSize = new Vector2(0.5f, 0.5f);
        Physics2D.BoxCast(BoxCastOffset, BoxCastSize, 0, Vector2.right, contactFilter2D, results, 20f);
        foreach (RaycastHit2D hit in results)
        {
            if (hit)
            {
                if (!DestroyList.Contains(hit.transform.gameObject))
                {
                    DestroyList.Add(hit.transform.gameObject);
                }
            }
        }
        matches.CreateDestroyList(DestroyList);
    }

    public void DestroyColumn(GameObject block)
    {
        List<GameObject> DestroyList = new List<GameObject>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(blockLayerMask);
        RaycastHit2D[] results = new RaycastHit2D[30];
        Vector2 BoxCastOffset = new Vector2(block.transform.position.x, block.transform.position.y - 11f);
        Vector3 BoxCastSize = new Vector2(0.5f, 0.5f);
        Physics2D.BoxCast(BoxCastOffset, BoxCastSize, 0, Vector2.up, contactFilter2D, results, 20f);
        foreach (RaycastHit2D hit in results)
        {
            if (hit)
            {
                if (!DestroyList.Contains(hit.transform.gameObject))
                {
                    DestroyList.Add(hit.transform.gameObject);
                }
            }
        }
        matches.CreateDestroyList(DestroyList);
    }

    public void DestroyBlocks3x3(GameObject block)
    {
        List<GameObject> DestroyList = new List<GameObject>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(blockLayerMask);
        RaycastHit2D[] results = new RaycastHit2D[30];
        Vector3 BoxCastSize = new Vector2(2f, 2f);
        Physics2D.BoxCast(block.transform.position, BoxCastSize, 0, Vector2.zero, contactFilter2D, results, 0F);
        foreach (RaycastHit2D hit in results)
        {
            if (hit)
            {
                if (!DestroyList.Contains(hit.transform.gameObject))
                {
                    DestroyList.Add(hit.transform.gameObject);
                }
            }
        }
        matches.CreateDestroyList(DestroyList);
    }

    public int DestroyAllOfColor(string tag)
    {
        List<GameObject> DestroyList = new List<GameObject>();
        foreach (GameObject block in matches.AllBlocks)
        {
            if (tag == block.tag) DestroyList.Add(block);
        }
        return matches.CreateDestroyList(DestroyList);
    }
}
