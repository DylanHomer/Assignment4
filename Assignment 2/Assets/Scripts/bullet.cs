using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    Rigidbody2D rb;
    public int speed;
    public Renderer rend;
    private bool flipped;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        if (speed == 0) speed = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (speed > 0 && !flipped)
        {
            flip();
        }
        rb.velocity = new Vector2(speed, rb.position.y);
    }
    void flip()
    {
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x = -scaleFactor.x;
        transform.localScale = scaleFactor;
        flipped = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Colliders")
        {
            Destroy(gameObject);
        }
    }
}
