using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    private Sword swordScript;
    private PlayerMovement playerMovement;

    public Image cooldownImage; 
    public Text cooldownText; 

    private float attackDelay = 0.8f;
    public float nextFire;
    public float myTime = 0.0F;
    private float attackingTime = 0.8f;
    public bool attacking;

    public GameObject shot;
    public Transform[] shotSpawn;


    // Use this for initialization
    void Start () {
        swordScript = GetComponentInChildren<Sword>();
        playerMovement = GetComponent<PlayerMovement>();
        attacking = false;
    }

    // Update is called once per frame
    void Update ()
    {
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + attackDelay;
            swordScript.PerformAttack();

            nextFire = nextFire - myTime;
            myTime = 0.0f;
            attacking = true;
            cooldownImage.fillAmount = 0;
            playerMovement.enabled = false;
        }
        else if (Input.GetButton("Fire2") && myTime > nextFire)
        {
            nextFire = myTime + attackDelay;
            Debug.Log("FIRE RANGE");
            Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation);
            Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation);
            nextFire = nextFire - myTime;
            myTime = 0.0f;
            attacking = true;
            cooldownImage.fillAmount = 0;
            playerMovement.enabled = false;
        }




        if (myTime > attackingTime)
        {
            cooldownImage.fillAmount = 0;
            cooldownText.text = "";
            attacking = false;
            playerMovement.enabled = true;
        } else
        {
            cooldownImage.fillAmount += 1 / attackDelay * Time.deltaTime;
            cooldownText.text = Mathf.Max(attackDelay - myTime, 0).ToString("#0.#");
        }

    }
}
