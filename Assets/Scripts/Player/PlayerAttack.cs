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

        if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.L)) && myTime > nextFire)
        {
            nextFire = myTime + attackDelay;
            swordScript.PerformAttack();

            nextFire = nextFire - myTime;
            myTime = 0.0f;
            attacking = true;
            if(cooldownImage)
                cooldownImage.fillAmount = 0;
            playerMovement.enabled = false;
        }
        else if ((Input.GetButton("Fire2") || Input.GetKeyDown(KeyCode.Semicolon)) && myTime > nextFire)
        {
            nextFire = myTime + attackDelay;
            Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation);
            Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation);
            nextFire = nextFire - myTime;
            myTime = 0.0f;
            attacking = true;
            if (cooldownImage)
                cooldownImage.fillAmount = 0;
            playerMovement.enabled = false;
        }




        if (myTime > attackingTime)
        {
            if (cooldownImage)
                cooldownImage.fillAmount = 0;
            if (cooldownText)
                cooldownText.text = "";
            attacking = false;
            playerMovement.enabled = true;
        } else
        {
            if(cooldownImage)
            cooldownImage.fillAmount += 1 / attackDelay * Time.deltaTime;
            if(cooldownText)
            cooldownText.text = Mathf.Max(attackDelay - myTime, 0).ToString("#0.#");
        }

    }
}
