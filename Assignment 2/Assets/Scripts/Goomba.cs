using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public bool moveLeft;

    public AudioSource killed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        if (speed <= 0)
        {
            speed = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (moveLeft == true)
        {
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
        }
        else if (moveLeft == false)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "EnemySwapLeft")
        {
            Debug.Log("EDGE");
            moveLeft = false;
        }
        if (c.gameObject.tag == "EnemySwapRight")
        {
            Debug.Log("EDGE");
            moveLeft = true;
        }
    }
}
