using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour {

    public string myString;
    public GameObject[] PresettedAttacks;
    List<Attack> attackList;

    public GameInstanceManager gameInstanceManager;
    public float delayStart = 5.0f;
    private float myTime = 0.0f;
    private float waitDelayForNextAttack;
    
    // Use this for initialization
    void Start () {
        if (attackList == null)
        {
            attackList = new List<Attack>();
            foreach (GameObject AttackObject in PresettedAttacks)
            {
                Attack anAttack = (Attack) AttackObject.GetComponent<MonoBehaviour>();
                anAttack.MyAwake();
                attackList.Add(anAttack);
            }
            waitDelayForNextAttack = delayStart;
        }

        // This work
        //attack1 = gameObject.AddComponent<Attack1>();

        // This work
        //attackList[0] = (Attack) gameObject.AddComponent(System.Type.GetType("Attack1"));

        // This equals new Attack1(); ???????????
        //// ThisWork2
        ////attackList2.Add((Attack) System.Activator.CreateInstance(System.Type.GetType("Attack1")));
    }

    public void MyAwake()
    {
        attackList = new List<Attack>();
        foreach (GameObject AttackObject in PresettedAttacks)
        {
            Attack anAttack = (Attack) AttackObject.GetComponent<MonoBehaviour>();
            anAttack.MyAwake();
            attackList.Add(anAttack);
        }
        waitDelayForNextAttack = delayStart;

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
        Attack attack = attackList[a];
        //Debug.Log(attack.gameObject.name);
        StartCoroutine(attack.PerformSubAttacks(gameInstanceManager));
        waitDelayForNextAttack = attack.totalSubAttacksExecuteTime + attack.delayAfterAttack;

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

    public void MyReset()
    {
         delayStart = 5.0f;
         myTime = 0.0f;
         waitDelayForNextAttack = delayStart;
         StopAllCoroutines();
    }
}
