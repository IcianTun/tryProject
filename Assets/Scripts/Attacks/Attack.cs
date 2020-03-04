using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject[] PresettedSubAttack;
    List<ISubAttack> subAttacks;

    public float executeTime = 0.0f;

    public Attack()
    {

    }


    private void Start()
    {
        subAttacks = new List<ISubAttack>();
        foreach(GameObject subAttackObject in PresettedSubAttack)
        {
            ISubAttack subAttackScript = (ISubAttack)subAttackObject.GetComponent<MonoBehaviour>();
            subAttacks.Add(subAttackScript);
            executeTime += subAttackScript.GetExecuteTime();
        }
    }
    

    public IEnumerator PerformSubAttacks()
    {
        foreach (ISubAttack subattack in subAttacks)
        {
            Debug.Log("ExecuteTime of subattack: " + subattack.GetExecuteTime());
            subattack.Perform();
            yield return new WaitForSeconds(subattack.GetExecuteTime() + 1.5f);
        }
    }

    
    virtual public void Hello()
    {
        Debug.Log("hello from base Attack");
    }
}
