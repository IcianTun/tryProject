using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour {

    bool isHit;
    float delay = 3;
    int damagePoint = 10;

    GameObject player;
    PlayerHealth playerHealth;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(DealDamageDelayed());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Debug.Log("Enter");
            isHit = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
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
        gameObject.SetActive(false);
    }
}
