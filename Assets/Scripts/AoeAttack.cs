using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour {

    public float delay = 3;
    public int damagePoint = 10;

    public GameObject player;
    PlayerHealth playerHealth;

    private bool isHit;

    // Use this for initialization
    void Start () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(DealDamageDelayed());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            //Debug.Log("Enter");
            isHit = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Exit");
            isHit = false;
        }
    }
    IEnumerator DealDamageDelayed()
    {
        yield return new WaitForSeconds(delay);
        if (isHit)
        {
            Debug.Log("Hit");
            playerHealth.takeDamage(damagePoint);
        }
        else
        {
            Debug.Log("Miss");
        }
        Destroy(gameObject);
    }
}
