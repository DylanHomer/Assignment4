using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public float time;
    public bool fireRight;
    public bool fireLeft;
    private float currentTime;
    public GameObject bulletRight;
    public GameObject bulletLeft;
    public Transform bulletSpawn;
    public AudioSource shoot;

    public float spawnX;
    public float spawnY;
    private Vector2 spawnPos;
    // Use this for initialization
    void Start () {
        fireRight = false;
        currentTime = 0f;
        if (time <= 0)
        {
            time = 3f;
            Debug.LogWarning("Time not found on " + name + ". Defaulting to " + time);
        }
        if (spawnX == 0)
        {
            spawnX = bulletSpawn.position.x;
        }
        if (spawnY == 0)
        {
            spawnY = bulletSpawn.position.y;
        }
        spawnPos = new Vector2(spawnX, spawnY);
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + 0.1f;
        //fireRight = false;
        //fireLeft = false;
        if (currentTime >= time)
        {
            currentTime = time;
            if (fireRight)
            {
                currentTime = 0;
                Instantiate(bulletRight, spawnPos, Quaternion.identity);
                shoot.Play();
            }
            if (fireLeft)
            {
                currentTime = 0;
                Instantiate(bulletLeft, spawnPos, Quaternion.identity);
                shoot.Play();
            }
        }
    }

}
