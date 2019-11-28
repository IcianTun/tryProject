using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {

    public int damage;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Boss")
        other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
    }

}
