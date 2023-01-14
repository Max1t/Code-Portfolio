using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public List<Collider2D> colliders = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Blue") || collision.gameObject.CompareTag("White") || collision.gameObject.CompareTag("Green") || collision.gameObject.CompareTag("Red") || collision.gameObject.CompareTag("Yellow"))
        {
            Collider2D coll = collision.gameObject.GetComponent<PolygonCollider2D>();
            if (!colliders.Contains(coll))
                colliders.Add(coll);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blue") || collision.gameObject.CompareTag("White") || collision.gameObject.CompareTag("Green") || collision.gameObject.CompareTag("Red") || collision.gameObject.CompareTag("Yellow"))
        {
            Collider2D coll = collision.gameObject.GetComponent<PolygonCollider2D>();
            if (colliders.Contains(coll))
                colliders.Remove(coll);
        }
    }


    
}
