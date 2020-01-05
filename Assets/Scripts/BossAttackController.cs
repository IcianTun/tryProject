using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    IAttack attack1;

    public float attackRate = 1.0f;
    public float delay = 5.0f;

    // Use this for initialization
    void Start () {

        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        attack1 = (IAttack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        InvokeRepeating("PerformAttack", delay, attackRate);
    }
	

	void PerformAttack () {
        attack1.Perform();
	}
}
