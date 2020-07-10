using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {

    public int damage;
    public bool isRemovedAfterHit;
    public bool isPlayer;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Player Attacks
        if (other.tag == "Boss") {
            other.gameObject.GetComponent<BossHealth>().takeDamage(damage);
            if (isRemovedAfterHit)
            {
                Destroy(gameObject);
            }
        }
        else if (other.tag == "Boundary" && isRemovedAfterHit)
        {
            StartCoroutine(DestroyAfter());
        }

        // Enemy Attacks
        if (!isPlayer && other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            if (isRemovedAfterHit)
            {
                Destroy(gameObject);
            }
        }

    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
