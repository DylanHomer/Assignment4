using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUp : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public bool moveLeft;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (speed <= 0)
        {
            speed = 1;
        }
        int chance = Random.Range(1, 100);
        if (chance < 50)
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (moveLeft)
        {
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
