using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerBlock : MonoBehaviour {
    public float time;
    public float offSet;
    private float currentTime;
    public bool visible;
    private bool begin;

    public Renderer rend;


    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        begin = true;
        currentTime = 0f;
        if (time <= 0)
        {
            time = 3f;
            Debug.LogWarning("Time not found on " + name + ". Defaulting to " + time);
        }
        if(offSet > 0)
        {
            begin = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + 0.1f;
        if (!begin && currentTime >= offSet)
        {
            currentTime = 0;
            begin = true;
        }
        if (visible)
        {
            Appear();
        }
        if (!visible)
        {
            Disappear();
        }
        if (currentTime >= time && visible && begin)
        {       
            visible = false;
            currentTime = 0;
        }
        if(currentTime >= time && !visible && begin)
        {          
            visible = true;
            currentTime = 0;
        }

	}

    void Appear()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        rend.enabled = true;
        gameObject.layer = 9;
    }

    void Disappear()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        rend.enabled = false;
        gameObject.layer = 8;
    }

}
