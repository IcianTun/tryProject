using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public Attack currentAttack;

    public string myString;
    //////////public GameObject[] PresettedAttacks;
    public List<Attack> attackList;

    public GameInstanceManager gameInstanceManager;
    public float delayStart = 1.0f;
    private float myTime = 0.0f;
    //private float waitDelayForNextAttack;
    public bool nextAttackReady = false;
    private bool start = false;

    // Use this for initialization
    void Awake ()
    {
        if (attackList == null)
        {
            attackList = new List<Attack>();
        }
        //////////if (attackList.Count == 0)
        //////////{
        //////////    foreach (GameObject AttackObject in PresettedAttacks)
        //////////    {
        //////////        Attack anAttack = (Attack)AttackObject.GetComponent<MonoBehaviour>();
        //////////        anAttack.MyAwake();
        //////////        attackList.Add(anAttack);
        //////////    }
        //////////}
        //waitDelayForNextAttack = delayStart;
        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        //attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        // This equals new Attack1(); ???????????
        //// ThisWork2
        ////attackList2.Add((Attack) System.Activator.CreateInstance(System.Type.GetType("Attack1")));
        //Debug.Log("BossAttackController Awake");
        nextAttackReady = false;
        foreach (Attack attack in attackList)
        {
            attack.MyAwake();
        }
    }

    //public void MyAwake()
    //{
    //    attackList = new List<Attack>();
    //    foreach (GameObject AttackObject in PresettedAttacks)
    //    {
    //        Attack anAttack = (Attack) AttackObject.GetComponent<MonoBehaviour>();
    //        anAttack.MyAwake();
    //        attackList.Add(anAttack);
    //    }
    //    waitDelayForNextAttack = delayStart;

    //}

    private void Update()
    {
        ////ThisWork2 
        ////attackList2[0].Hello();

        if (myTime > delayStart && !start)
        {
            nextAttackReady = true;
            start = true;
            //PerformAttack();
            //myTime = 0.0f;
        } else
        {
            myTime = myTime + Time.deltaTime;
        }
        if (nextAttackReady)
        {
            nextAttackReady = false;
            PerformAttack();
        }
    }

    void PerformAttack () {
        int a = Random.Range(0, attackList.Count);
        Attack attack = attackList[a];
        //Debug.Log(attack.gameObject.name);
        currentAttack = attack;
        StartCoroutine(attack.PerformSubAttacks(gameInstanceManager,this));
        //waitDelayForNextAttack = attack.totalSubAttacksExecuteTime + attack.delayAfterAttack;

    }

    public void MyReset()
    {
        delayStart = 1.0f;
        myTime = 0.0f;
        start = false;
        //waitDelayForNextAttack = delayStart;
        StopAllCoroutines();
    }
}
