using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject[] PresettedSubAttack;
    List<ISubAttack> subAttacks;

    private void Start()
    {
        subAttacks = new List<ISubAttack>();
        foreach(GameObject subAttackObject in PresettedSubAttack)
        {
            subAttacks.Add((ISubAttack) subAttackObject.GetComponent<MonoBehaviour>());
        }
        StartCoroutine(PerformAttack());
    }

    //public void Update()
    //{
    //    Debug.Log("asdf");
    //    Perform();
    //}

    public IEnumerator PerformAttack()
    {
        Debug.Log("tst1234");
        foreach (ISubAttack subattack in subAttacks)
        {
            subattack.Perform();
            yield return new WaitForSeconds(0.5f);
        }
    }

}
