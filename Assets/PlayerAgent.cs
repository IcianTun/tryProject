using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;

public class PlayerAgent : Agent {
    [Header("Agent")]
    public GameObject gameInstance;
    GameInstanceManager gameInstanceManager;

    public GameObject boss;
    BossHealth bossHealth;
    int lastBossHealth;

    PlayerHealth playerHealth;
    Rigidbody playerRigidbody;
//    ResetParameters m_ResetParams;

    [Header("From PlayerMovement")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 90f;
    public int aoeTouchingCount = 0;
    List<Collider> aoeTouching = new List<Collider>();

    [Header("From PlayerAttack")]
    public GameObject shot;
    public Transform[] shotSpawn;

    Sword swordScript;
    float myTime = 0.0F;
    float attackDelay = 0.8f;
    float attackingTime = 0.8f;
    float nextFire;
    bool attacking;

    public float angle;
    public float dot;
    public override void InitializeAgent()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        swordScript = GetComponentInChildren<Sword>();
        attacking = false;

        //boss = gameInstance.transform.Find("Boss").gameObject;
        gameInstanceManager = gameInstance.GetComponent<GameInstanceManager>();
        boss = gameInstanceManager.boss;
        bossHealth = boss.GetComponent<BossHealth>();
        lastBossHealth = bossHealth.getCurrentHealth();

        //var academy = FindObjectOfType<Academy>();
        //m_ResetParams = academy.resetParameters;
        SetResetParameters();
    }

    public override void CollectObservations()
    {
        // player rotation
        AddVectorObs(gameObject.transform.rotation.y);
        // player position on Arena
        AddVectorObs(gameObject.transform.position - gameInstance.transform.position);
        // vector to boss
        AddVectorObs(boss.transform.position - gameObject.transform.position);       // Later: Changing boss object affect this
        AddVectorObs(aoeTouchingCount/10.0f);
        AddVectorObs(angle/180f);
    }


    public void SetResetParameters()
    {
        //Set the attributes by fetching the information from the academy
    }

    public override void AgentAction(float[] vectorAction)
    {
        int v = 0, h = 0;
        bool  isTurnRight = false, isTurnLeft = false ;
        var forwardMotionAction = (int)vectorAction[0];
        var sideMotionAction = (int)vectorAction[1];
        var rotateAction = (int)vectorAction[2];
        var attackAction = (int)vectorAction[3]; // 0 = no attack , 1 = melee , 2 = range 

        if (forwardMotionAction == 1)
            v = 1;
        else if (forwardMotionAction == 2)
            v = -1;
        if (sideMotionAction == 1)
            h = 1;
        else if (sideMotionAction == 2)
            h = -1;
        switch (rotateAction)
        {
            case 1:
                isTurnRight = true;
                break;
            case 2:
                isTurnLeft = true;
                break;
        }
        if (!attacking)
        {
            Move(v, h);
            Turning(isTurnLeft, isTurnRight);        
        }
        if (myTime > nextFire)
        {
            if (attackAction == 1)
                meleeAttack();
            else if (attackAction == 2)
                rangeAttack();
        }
        AddReward(CalculateAngleReward());
        // Reduce reward player hp is at player Health
        AddReward(-aoeTouchingCount/400.0f); // 0.0025f per aoe
        AddReward(-0.0005f); //   1/2000
        if (lastBossHealth > bossHealth.getCurrentHealth())
        {
            AddReward( (lastBossHealth-bossHealth.getCurrentHealth())/10.0f ); // 0.1 per 1 hp
        }
            
        if (playerHealth.currentHealth <= 0)
        {
            AddReward(-1.0f);
            Done();
        }
        if (bossHealth.getCurrentHealth() <= 0)
        {
            AddReward(1.0f);
            Done();
        }
    }

    public override void AgentReset()
    {

        gameObject.transform.rotation = Quaternion.identity;
      ////gameObject.transform.Rotate(new Vector3(0, 1, 1), Random.Range(-10f, 10f));
        gameObject.transform.position = new Vector3(0, 1f, -5)
            + gameInstance.transform.position;

        boss.transform.position = gameInstance.transform.position;
        boss.transform.position += new Vector3(0, 2f, 0);
        bossHealth.resetHealth();
        playerHealth.resetHealth();
        gameInstanceManager.DestroyAllAoe();


        myTime = 0.0F;
        attackDelay = 0.8f;
        attackingTime = 0.8f;
        nextFire = 0.0f;
        attacking = false;

        //Reset the parameters when the Agent is reset.
        SetResetParameters();
    }

    float CalculateAngleReward()
    {
        Vector3 bossPos = boss.transform.position;
        bossPos.y = 0;
        dot = Vector3.Dot(transform.forward, (bossPos - transform.position).normalized);
        angle = Mathf.Acos(dot) * Mathf.Rad2Deg;  // 0-180     0-90 = + , 90-180 = -
                                                  //          close to 0 is + much 
        if (angle < 90.0f)
        {
            return (90f - angle) / (90f * 500f);
        }
        else // 90-180
        {
            return -(angle - 90f) / (90f * 500f);
        }
    }


    //------ Player Movement ------------------------------------------------------------------------------

    //private void FixedUpdate()
    //{
    //    float h = Input.GetAxisRaw("Horizontal");
    //    float v = Input.GetAxisRaw("Vertical");
    //    Move(h, v);
    //    Turning(Input.GetKey(KeyCode.Q), Input.GetKey(KeyCode.E));
    //}

    void Move(float v, float h)
    {
        // horizontal = right left
        // vertical = forward backward
        Vector3 vertical = transform.forward;
        Vector3 horizontal = transform.right;

        Vector3 movement = (vertical * v + horizontal * h).normalized * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(bool isTurnLeft, bool isTurnRight)
    {
        if (isTurnRight && !isTurnLeft)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
        }
        else if (isTurnLeft && !isTurnRight)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
        }
    }

    // -- PlayerAttack ------------------------------------------------------------------------------
    void meleeAttack()
    {
        nextFire = myTime + attackDelay;
        swordScript.PerformAttack();

        nextFire = nextFire - myTime;
        myTime = 0.0f;
        attacking = true;
        //playerMovement.enabled = false;
    }

    void rangeAttack()
    {   
        nextFire = myTime + attackDelay;
        Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation).transform.parent = gameInstanceManager.transform;
        Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation).transform.parent = gameInstanceManager.transform;
        nextFire = nextFire - myTime;
        myTime = 0.0f;
        attacking = true;
        //playerMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > attackingTime)
        {
            attacking = false;
        }
        
        for (int i = aoeTouching.Count - 1; i >= 0; i--)
        {
            if (aoeTouching[i] == null)
            {
                aoeTouchingCount -= 1;
                aoeTouching.RemoveAt(i);
            }
        }
    }

}
