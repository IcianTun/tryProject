using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour {

    public float delay;
    public int damagePoint = 10;


    public GameObject AoeTimer;
    PlayerHealth playerHealth;

    private bool isHit;

    private float myTime = 0.0f;

    // Use this for initialization
    void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(DealDamageDelayed());
    }
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        AoeTimer.transform.localScale = new Vector3(myTime/delay,1,myTime/delay);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            isHit = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isHit = false;
        }
    }
    IEnumerator DealDamageDelayed()
    {
        yield return new WaitForSeconds(delay);
        if (isHit)
        {
            playerHealth.takeDamage(damagePoint);
        }
        else
        {
        }
        Destroy(gameObject);
    }
    public void setDelay(float delay)
    {
        this.delay = delay;
    }
}
