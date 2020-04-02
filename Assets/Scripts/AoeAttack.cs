using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AoeType
{
    Square,
    Circle
}


public class AoeAttack : MonoBehaviour {

    public float activateTimer;
    float lingerTime = 0;

    public int damagePoint = 10;

    public AoeType aoeType;

    public GameObject aoeTimerParent;
    PlayerHealth playerHealth;

    private bool isHit;
    private bool alreadyHit = false;
    private bool activated = false;

    public Material lingerMaterial;

    private float myTime = 0.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine(DealDamageDelayed());
    }
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime / activateTimer < 1)
            aoeTimerParent.transform.localScale = new Vector3(myTime / activateTimer, 1, myTime / activateTimer);

        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (!alreadyHit && activated)
            {
                playerHealth.takeDamage(damagePoint);
                alreadyHit = true;
            }
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
        yield return new WaitForSeconds(activateTimer);
        activated = true;
        if (isHit)
        {
            alreadyHit = true;
            playerHealth.takeDamage(damagePoint);
            
        }
        if (lingerTime > 0) {
            GetComponent<MeshRenderer>().material = lingerMaterial;
            aoeTimerParent.transform.position -= new Vector3(0,-0.002f,0);
            StartCoroutine(DestroyAfter());
        } else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(lingerTime);
        Destroy(transform.parent.gameObject);
    }

    public void SettingsAoe(float activateTimer, float lingerTime,int damage)
    {
        this.activateTimer = activateTimer;
        this.lingerTime = lingerTime;
        this.damagePoint = damage;
    }

    public void SetDamage(int newDamage)
    {
        damagePoint = newDamage;
    }
}
