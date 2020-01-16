using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    private Attack[] attackList;

    public float attackRate = 1.0f;
    public float delay = 5.0f;

    // Use this for initialization
    void Start () {
        attackList = new Attack[8];
        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        InvokeRepeating("PerformAttack", delay, attackRate);
    }

    private void Update()
    {
        Debug.Log(Coordinate.Instance.north.position);
    }

    void PerformAttack () {
        // TODO 
        // get random attack from IAttack[] array and perform it
        // 
        attackList[0].Perform();
	}
}
