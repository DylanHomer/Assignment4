using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour {

    Rigidbody2D rb;

    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool isFacingLeft;
    public int lives;
    public Text livesUI = null;
    public Text coinsUI = null;
    public int checkpoint;
    public int coins;
    public int stars;

    public AudioSource jump;
    public AudioSource oneUp;
    public AudioSource die;
    public AudioSource coinGet;
    public AudioSource killedEnemy;
    public AudioSource emptyBlockHit;
    public AudioSource starGet;
    public AudioSource gameEnd;
    public AudioSource music;
    Animator anim;

    // Use this for initialization
    void Start () {

        

        isFacingLeft = false;
        stars = 0;
        checkpoint = 0;
        coins = 0;
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.LogWarning("Rigidbody2D not found on " + name);   //Log warning if Rigidbody is not found
        }

        if (speed <= 0)
        {
            speed = 5.0f; // Default speed to 5
            Debug.LogWarning("Speed not set on " + name + " - defaulting to " + speed);  //Log warning if speed is not set
        }

        if (jumpForce <= 0)
        {
            jumpForce = 10.0f; //Default jump force to 5
            Debug.LogWarning("Jump Force not set on " + name + " - defaulting to " + jumpForce); //Log warning if jump force is not set
        }
        if (!groundCheck)
        {
            Debug.LogWarning("groundCheck not found on " + name); //Log warning if ground check is not set
        }
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f; //Default ground check radius to 0.02
            Debug.LogWarning("groundCheckRadius not set on " + name + " - defaulting to " + groundCheckRadius); //Log warning if ground check radius is not set
        }
        if (lives <= 0)
        {
            lives = 3;
            Debug.LogWarning("Lives not set on " + name + " - defaulting to " + lives);
        }
            anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.LogWarning("Animator not found on " + name);   //Log warning if Animator is not found
        }
        anim = GetComponent<Animator>(); //Initialize anim
        if (!anim)
        {
            Debug.LogWarning("Animator not found on " + name);   //Log warning if Animator is not found
        }
        music.Play();
        
    }
	
	// Update is called once per frame
	void Update () {
        livesUI.text = lives.ToString();
        coinsUI.text = coins.ToString();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        float moveValue = Input.GetAxisRaw("Horizontal");
        float jumpMoveValue = Input.GetAxis("Horizontal");     
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump.Play();
            Debug.LogWarning("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (isGrounded)
        {
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y); //Move character left or right
            if (rb.velocity.x < 0 && !isFacingLeft) flip();
            if (rb.velocity.x > 0 && isFacingLeft) flip();
        }
        else
        {
            rb.velocity = new Vector2(jumpMoveValue * speed, rb.velocity.y); //Move character left or right in air
            if (rb.velocity.x < 0 && !isFacingLeft) flip();
            if (rb.velocity.x > 0 && isFacingLeft) flip();
        }
        anim.SetFloat("speed", Mathf.Abs(moveValue));
        anim.SetBool("grounded", isGrounded);

        if (lives <= 0)
        {
            Application.LoadLevel(0);
        }

        if(coins >= 100)
        {
            lives++;
            oneUp.Play();
            coins = 0;
        }

    }
    void flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x = -scaleFactor.x;
        transform.localScale = scaleFactor;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(c.gameObject.tag);
        if (c.gameObject.tag == "collectable")
        {
            coinGet.Play();
            coins = coins + 1;
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "Killzone")
        {
            kill();
            
        }
        if (c.gameObject.tag == "OneUp")
        {
            oneUp.Play();
            Destroy(c.gameObject);
            lives++;
        }
        if (c.gameObject.tag == "EnemyKill")
        {
            killedEnemy.Play();
            Destroy(c.gameObject);
            rb.AddForce(Vector2.up * jumpForce * 2, ForceMode2D.Impulse);
        }
        if (c.gameObject.tag == "Enemy")
        {
            kill();
        }
        if (c.gameObject.tag == "Bullet")
        {
            kill();
        }
        if (c.gameObject.tag == "Star")
        {
            stars++;
            if (stars < 3) starGet.Play();
            else if (stars == 3)
            {
                gameEnd.Play();
                music.Stop();
                Application.LoadLevel(2);
            }
            Destroy(c.gameObject);
            
        }
    }
    void kill()
    {
        die.Play();
        if (stars == 0)
        {
            rb.position = new Vector3(-8.4f, -2.33f, -1.9f);
        }
        else if (stars == 1)
        {
            rb.position = new Vector3(28.55f, 5.12f, -1.9f);
        }
        else if (stars == 2)
        {
            rb.position = new Vector3(82.04f, 5.32f, -1.9f);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
            GameObject.Destroy(bullet);
        lives = lives - 1;
    }
}
