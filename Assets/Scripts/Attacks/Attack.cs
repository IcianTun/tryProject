using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject[] PresettedSubAttack;
    List<SubAttack> subAttacks;

    public List<IAction> actions;

    //[Header("For Testing")]
    //public GameInstanceManager gameInstanceManager;

    public string myString;
//    [System.NonSerialized]
    public float totalSubAttacksExecuteTime = 0.0f;

    /// Have to generate too when using GA
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
        //Debug.Log("Awake");
        //subAttacks = new List<IAction>();
        //foreach(GameObject subAttackObject in PresettedSubAttack)
        //{
        //    IAction subAttackScript = (IAction)subAttackObject.GetComponent<MonoBehaviour>();
        //    subAttacks.Add(subAttackScript);
        //    executeTime += subAttackScript.GetExecuteTime();
        //}
        MyAwake();
    }
    
    
    public void MyAwake()
    {
        totalSubAttacksExecuteTime = 0.0f;
        subAttacks = new List<SubAttack>();
        actions = new List<IAction>();
        foreach (GameObject subAttackObject in PresettedSubAttack)
        {
            IAction subAttackScript = (IAction) subAttackObject.GetComponent<MonoBehaviour>();
            //SubAttack a = (SubAttack) subAttackScript;
            actions.Add(subAttackScript);
            totalSubAttacksExecuteTime += subAttackScript.GetTotalDelay();
        }

    }

    public IEnumerator PerformSubAttacks(GameInstanceManager gameInstanceManager)
    {
        foreach (IAction action in actions)
        {
            action.Perform(gameInstanceManager);
            yield return new WaitForSeconds(action.GetTotalDelay());
        }
    }
/*
    public  GetIAction()
    {
        return new IAction[7];
    }
*/
    public List<SubAttack> GetSubAttacks()
    {
        return subAttacks;
    }

    public void SetAttackList(List<SubAttack> newSubAttack)
    {
        subAttacks = newSubAttack;
    }
}
