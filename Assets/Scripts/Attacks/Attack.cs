using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject[] PresettedSubAttack;
    List<SubAttack> subAttacks;

    public string myString;
//    [System.NonSerialized]
    public float executeTime = 0.0f;
    public float delayAfterAnSubAttack = 1.5f;
    /*
    private void Awake()
    {
        Debug.Log("Awake");
        subAttacks = new List<ISubAttack>();
        foreach(GameObject subAttackObject in PresettedSubAttack)
        {
            ISubAttack subAttackScript = (ISubAttack)subAttackObject.GetComponent<MonoBehaviour>();
            subAttacks.Add(subAttackScript);
            executeTime += subAttackScript.GetExecuteTime();
        }
    }
    */
    
    public void myAwake()
    {
        executeTime = 0;
        subAttacks = new List<SubAttack>();
        foreach (GameObject subAttackObject in PresettedSubAttack)
        {
            SubAttack subAttackScript = (SubAttack) subAttackObject.GetComponent<MonoBehaviour>();
            subAttacks.Add(subAttackScript);
            executeTime += subAttackScript.GetExecuteTime();
        }
    }

    public IEnumerator PerformSubAttacks(GameInstanceManager gameInstanceManager)
    {
        foreach (SubAttack subattack in subAttacks)
        {
            subattack.Perform(gameInstanceManager);
            yield return new WaitForSeconds(subattack.GetExecuteTime() + delayAfterAnSubAttack);
        }
    }

    public List<SubAttack> GetSubAttacks()
    {
        return subAttacks;
    }

    public void SetAttackList(List<SubAttack> newSubAttack)
    {
        subAttacks = newSubAttack;
    }
    virtual public void Hello()
    {
        Debug.Log("hello from base Attack");
    }
}
