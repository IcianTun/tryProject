using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {

    public int damage;
    public bool isRemovedAfterHit;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss") {
            other.gameObject.GetComponent<BossHealth>().takeDamage(damage);
        }
    }

}
