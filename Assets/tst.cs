using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour {

    public GameObject boss;
    public GameObject gameInstance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        // get gameObject by name!!!
        boss = gameInstance.transform.Find("boss").gameObject;

        // get gameobject in child 0
        //boss = gameInstance.transform.GetChild(0).gameObject;

    }
}
