using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveForward : MonoBehaviour {

    public float delayActiveMovement = 0.0f;
    public float moveSpeed;

    private bool hasDelay = false;
    private float myTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myTime > delayActiveMovement)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
        } else
        {
            myTime += Time.deltaTime;
        }
	}
}
