using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFireRight : MonoBehaviour {

    public GameObject turret;
	// Use this for initialization
	void Start () {
        //turret = GameObject.FindGameObjectWithTag("turret");

    }
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("PLAYER IN");
        if (c.gameObject.tag == "Player")
        {
            turret.GetComponent<Turret>().fireRight = true;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            turret.GetComponent<Turret>().fireRight = false;
        }
    }
}
