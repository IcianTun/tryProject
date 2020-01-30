using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public GameObject[] PresettedAttacks;
    List<Attack> attackList;

    public float delay = 5.0f;

    private float myTime = 0.0f;
    private float waitDelayForNextAttack;

    // Use this for initialization
    void Start () {

        attackList = new List<Attack>();
        foreach (GameObject AttackObject in PresettedAttacks)
        {
            attackList.Add((Attack) AttackObject.GetComponent<MonoBehaviour>());
        }
        waitDelayForNextAttack = delay;
        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        //attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > waitDelayForNextAttack)
        {
            PerformAttack();
            myTime = 0.0f;
        }
    }

    void PerformAttack () {

        // get random attack from IAttack[] array and perform it
        int a = Random.Range(0, attackList.Count);
        Debug.Log("Controller Perform attack");
        Debug.Log(a);
        Attack attack = attackList[a];
        StartCoroutine(attack.PerformSubAttacks());
        waitDelayForNextAttack = attack.executeTime + 3.0f;

    }
}
