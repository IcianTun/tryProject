using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    private BossActionType curState = BossActionType.Idle;

    public enum BossActionType
    {
        Idle,
        Attack1,
        Attack2,
        Attack3,
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (curState)
        {
            case BossActionType.Idle:
                //HandleIdle();
                break;
        }
		
	}

    void Attack1()
    {

    }
}
