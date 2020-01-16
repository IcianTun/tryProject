using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    ISubAttack[] subAttacks;

    public IEnumerable Perform()
    {
        foreach (ISubAttack subattack in subAttacks)
        {
            subattack.Perform();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
