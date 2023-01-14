using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
        if (transform.position.x < -87.62)
            transform.position = startPos;
    }
}
