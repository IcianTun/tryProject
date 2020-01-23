using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public GameObject[] PresettedAttacks;
    List<Attack> attackList;

    public float attackRate = 1.0f;
    public float delay = 5.0f;

    // Use this for initialization
    void Start () {

        attackList = new List<Attack>();
        foreach (GameObject AttackObject in PresettedAttacks)
        {
            attackList.Add((Attack) AttackObject.GetComponent<MonoBehaviour>());
        }   

        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        //attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        //InvokeRepeating("PerformAttack", delay, attackRate);
    }


    void PerformAttack () {
        // TODO 
        // get random attack from IAttack[] array and perform it
        // 
        //attackList[0].Perform();
        int a = Random.Range(0, attackList.Count);
        Debug.Log(a);
        Attack attack = attackList[a];
	}
}
