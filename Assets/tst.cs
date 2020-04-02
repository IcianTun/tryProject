using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour {

    public GameObject boss;
    public GameObject gameInstance;
    public Vector3 rotation;

    [Range(0f,5f)]
    public float tstFloat = 0;

    private float _tstGetSet;
    public float TstGetSet
    {
        get { return _tstGetSet; }
        set
        {
            if (value > 5.0f)
                value = 5.0f;
            _tstGetSet = value;
        }
    }

    public Vector3 start;
    public Vector3 end;
    public float distance;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.(start, end);
        transform.rotation = Quaternion.Euler(rotation);
        if (Input.GetKeyDown(KeyCode.P))
        {
            TstGetSet = 10f;
            Destroy(transform.parent);
        }
        // get gameObject by name!!!
        //boss = gameInstance.transform.Find("boss").gameObject;

        // get gameobject in child 0
        //boss = gameInstance.transform.GetChild(0).gameObject;

    }
}
