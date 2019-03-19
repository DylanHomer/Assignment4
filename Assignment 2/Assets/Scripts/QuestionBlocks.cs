using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlocks : MonoBehaviour {

    Rigidbody2D rb;
    public int coinsToAdd;
    public int blockContents;
    bool blockHit;
    public bool blockEmpty;
    public Transform hitBox;
    public LayerMask isPlayer;
    public GameObject OneUp;
    Animator anim;
    Vector2 blockPosition;
    // Use this for initialization
    void Start() {
        blockPosition = new Vector2(transform.position.x, transform.position.y + 1f);
        //rb = GetComponent<Rigidbody2D>();
        blockEmpty = false;
        anim = GetComponent<Animator>();
        if (blockContents <= 0)
        {
            blockContents = 1;
            Debug.Log("Block contents not set on " + name + ". Defaulting to coin.");
        }
        if (coinsToAdd <= 0 && blockContents == 1)
        {
            coinsToAdd = 1;
            Debug.Log("coinsToAdd not set on " + name + ". Defaulting to " + coinsToAdd);
        }
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("blockIsEmpty", blockEmpty);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (blockEmpty)
            {
                c.gameObject.GetComponent<Character>().emptyBlockHit.Play();
            }
            else if (!blockEmpty)
            {
                Debug.Log("BLOCK HIT");
                if(blockContents == 1)
                {
                    c.gameObject.GetComponent<Character>().coins += coinsToAdd;
                    c.gameObject.GetComponent<Character>().coinGet.Play();
                }
                if(blockContents == 2)
                {
                    c.gameObject.GetComponent<Character>().emptyBlockHit.Play();
                    Instantiate(OneUp, blockPosition, Quaternion.identity);
                }
                blockEmpty = true;
            }
        }
    }
}
