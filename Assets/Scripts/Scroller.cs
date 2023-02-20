using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private BoxCollider2D col;
    private float width;
    public float scrollFactor = 1.0f;
    public float offset = 1.5f;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        width = col.size.x;
    }

    void Update()
    {
        float scrollSpeed = GameManager.Instance.gameSpeed * scrollFactor;
        transform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -width / 3)
        {
            transform.position = transform.position + Vector3.right * (width / 3 + offset);
        }
    }
}
