﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //public string currentActionName;

    public GameObject[] PresettedActions;

    public List<IAction> actionsList;

    //[Header("For Testing")]
    //public GameInstanceManager gameInstanceManager;

    public string myString;
    
    public float delayAfterAttack = 1.5f;
    
    ///for testing
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        foreach (SubAttack subattack in subAttacks)
    //        {
    //            subattack.Perform(gameInstanceManager);
    //            //yield return new WaitForSeconds(subattack.GetExecuteTime() + delayAfterAnSubAttack);
    //        }
    //    }
    //}

    private void Awake()
    {
        //Debug.Log("Attack Awake");
        //subAttacks = new List<IAction>();
        //foreach(GameObject subAttackObject in PresettedActions)
        //{
        //    IAction subAttackScript = (IAction)subAttackObject.GetComponent<MonoBehaviour>();
        //    subAttacks.Add(subAttackScript);
        //    executeTime += subAttackScript.GetExecuteTime();
        //}
        MyAwake();
    }
    
    
    public void MyAwake()
    {
        InstantiateActionsList();
        //Debug.Log("Attack MyAwake");
        if (actionsList.Count == 0 && PresettedActions != null)
        {
            //Debug.Log(actionsList.Count);
            foreach (GameObject subAttackObject in PresettedActions)
            {
                IAction action = (IAction)subAttackObject.GetComponent<MonoBehaviour>();
                actionsList.Add(action);
            }
        } 


    }

    public IEnumerator PerformSubAttacks(GameInstanceManager gameInstanceManager,BossAttackController bossAttackController)
    {
        foreach (IAction action in actionsList)
        {
            action.Perform(gameInstanceManager);
            yield return new WaitForSeconds(action.GetTotalDelay());
        }
        //Debug.Log("finish an attack : "+ gameObject.name);
        bossAttackController.nextAttackReady = true;
        yield return null;
    }

    public void InstantiateActionsList()
    {
        if (actionsList == null)
        {
            actionsList = new List<IAction>();
        }
    }
}
