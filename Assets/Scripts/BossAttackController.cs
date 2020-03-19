﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public string myString;
    public GameObject[] PresettedAttacks;
    List<Attack> attackList;
    ////List<Attack> attackList2;

    public GameInstanceManager gameInstanceManager;
    public float delayStart = 5.0f;
    public float delayBetweenAttack = 3.0f;

    private float myTime = 0.0f;
    private float waitDelayForNextAttack;

    // Use this for initialization
    void Start () {
        //player = gameInstance.transform.Find("Player").gameObject;
        if (attackList == null)
        {
            attackList = new List<Attack>();
            foreach (GameObject AttackObject in PresettedAttacks)
            {
                Attack anAttack = (Attack)AttackObject.GetComponent<MonoBehaviour>();
                anAttack.myAwake();
                attackList.Add(anAttack);
            }
            waitDelayForNextAttack = delayStart;
        }

        //Debug.Log(PresettedAttacks.Length);

        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        //attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        // This equals new Attack1(); ???????????
        //// ThisWork2
        ////attackList2.Add((Attack) System.Activator.CreateInstance(System.Type.GetType("Attack1")));
    }

    public void myAwake()
    {
        attackList = new List<Attack>();
        foreach (GameObject AttackObject in PresettedAttacks)
        {
            Attack anAttack = (Attack) AttackObject.GetComponent<MonoBehaviour>();
            anAttack.myAwake();
            attackList.Add(anAttack);
        }
        waitDelayForNextAttack = delayStart;

        Debug.Log(PresettedAttacks.Length);


    }

    private void Update()
    {
        ////ThisWork2 
        ////attackList2[0].Hello();



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
        Debug.Log(attackList.Count);
        Debug.Log("Perform Attack > "+a);
        Attack attack = attackList[a];
        StartCoroutine(attack.PerformSubAttacks(gameInstanceManager));
        waitDelayForNextAttack = attack.executeTime + delayBetweenAttack;

    }

    public void setAttackList(List<Attack> newAttackList)
    {
        attackList = newAttackList;
    }

    public List<Attack> getAttackList()
    {
        return attackList;
    }

    public void setGameInstanceManager(GameInstanceManager gameInstanceManager)
    {
        this.gameInstanceManager = gameInstanceManager;
    }
}
