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
    float attackDelay = 1.0f;
    float attackingTime = 0.8f;
    float nextFire;
    bool attacking;

    public float angle;
    public float dot;

    public Detector detectorForward;
    public Detector detectorBehind;
    public Detector detectorLeft;
    public Detector detectorRight;

    public Detector detectorForwardLeft;
    public Detector detectorForwardRight;
    public Detector detectorBehindLeft;
    public Detector detectorBehindRight;

    public Detector detectorForward2;
    public Detector detectorBehind2;
    public Detector detectorLeft2;
    public Detector detectorRight2;

    [Header("Range 3")]
    public Detector detectorForward3;
    public Detector detectorForward2Left;
    public Detector detectorForward2Right;
    public Detector detectorBehind3;
    public Detector detectorBehind2Left;
    public Detector detectorBehind2Right;
    public Detector detectorLeft3;
    public Detector detectorLeft2Forward;
    public Detector detectorLeft2Behind;
    public Detector detectorRight3;
    public Detector detectorRight2Forward;
    public Detector detectorRight2Behind;

    [Header("Range 4")]
    public Detector detectorForward4;
    public Detector detectorForward3Left;
    public Detector detectorForward3Right;
    public Detector detectorBehind4;
    public Detector detectorBehind3Left;
    public Detector detectorBehind3Right;
    public Detector detectorLeft4;
    public Detector detectorLeft3Forward;
    public Detector detectorLeft3Behind;
    public Detector detectorRight4;
    public Detector detectorRight3Forward;
    public Detector detectorRight3Behind;
    public Detector detectorForwardLeftCorner;
    public Detector detectorForwardRightCorner;
    public Detector detectorBehindLeftCorner;
    public Detector detectorBehindRightCorner;

    

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
        Vector3 bossPos = boss.transform.position;
        bossPos.y = 0;
        dot = Vector3.Dot(transform.forward, (bossPos - transform.position).normalized);
        float a = Mathf.Acos(dot) * Mathf.Rad2Deg;  // 0-180     0-90 = + , 90-180 = -
                                                  //          close to 0 is + much 
        AddVectorObs(a/180f);

        AddVectorObs(detectorForward.aoeTouchingCount);
        AddVectorObs(detectorBehind.aoeTouchingCount);
        AddVectorObs(detectorLeft.aoeTouchingCount);
        AddVectorObs(detectorRight.aoeTouchingCount);
        AddVectorObs(detectorForward.isOutOfBoundary);
        AddVectorObs(detectorBehind.isOutOfBoundary);
        AddVectorObs(detectorLeft.isOutOfBoundary);
        AddVectorObs(detectorRight.isOutOfBoundary);

        AddVectorObs(detectorForwardLeft.aoeTouchingCount);
        AddVectorObs(detectorForwardLeft.isOutOfBoundary);
        AddVectorObs(detectorForwardRight.aoeTouchingCount);
        AddVectorObs(detectorForwardRight.isOutOfBoundary);
        AddVectorObs(detectorBehindLeft.aoeTouchingCount);
        AddVectorObs(detectorBehindLeft.isOutOfBoundary);
        AddVectorObs(detectorBehindRight.aoeTouchingCount);
        AddVectorObs(detectorBehindRight.isOutOfBoundary);

        AddVectorObs(detectorForward2.aoeTouchingCount);
        AddVectorObs(detectorBehind2.aoeTouchingCount);
        AddVectorObs(detectorLeft2.aoeTouchingCount);
        AddVectorObs(detectorRight2.aoeTouchingCount);
        AddVectorObs(detectorForward2.isOutOfBoundary);
        AddVectorObs(detectorBehind2.isOutOfBoundary);
        AddVectorObs(detectorLeft2.isOutOfBoundary);
        AddVectorObs(detectorRight2.isOutOfBoundary);

        AddVectorObs(detectorForward3.aoeTouchingCount);
        AddVectorObs(detectorForward2Left.aoeTouchingCount);
        AddVectorObs(detectorForward2Right.aoeTouchingCount);
        AddVectorObs(detectorBehind3.aoeTouchingCount);
        AddVectorObs(detectorBehind2Left.aoeTouchingCount);
        AddVectorObs(detectorBehind2Right.aoeTouchingCount);
        AddVectorObs(detectorLeft3.aoeTouchingCount);
        AddVectorObs(detectorLeft2Forward.aoeTouchingCount);
        AddVectorObs(detectorLeft2Behind.aoeTouchingCount);
        AddVectorObs(detectorRight3.aoeTouchingCount);
        AddVectorObs(detectorRight2Forward.aoeTouchingCount);
        AddVectorObs(detectorRight2Behind.aoeTouchingCount);
        AddVectorObs(detectorForward3.isOutOfBoundary);
        AddVectorObs(detectorForward2Left.isOutOfBoundary);
        AddVectorObs(detectorForward2Right.isOutOfBoundary);
        AddVectorObs(detectorBehind3.isOutOfBoundary);
        AddVectorObs(detectorBehind2Left.isOutOfBoundary);
        AddVectorObs(detectorBehind2Right.isOutOfBoundary);
        AddVectorObs(detectorLeft3.isOutOfBoundary);
        AddVectorObs(detectorLeft2Forward.isOutOfBoundary);
        AddVectorObs(detectorLeft2Behind.isOutOfBoundary);
        AddVectorObs(detectorRight3.isOutOfBoundary);
        AddVectorObs(detectorRight2Forward.isOutOfBoundary);
        AddVectorObs(detectorRight2Behind.isOutOfBoundary);

        AddVectorObs(detectorForward4.aoeTouchingCount);
        AddVectorObs(detectorForward3Left.aoeTouchingCount);
        AddVectorObs(detectorForward3Right.aoeTouchingCount);
        AddVectorObs(detectorBehind4.aoeTouchingCount);
        AddVectorObs(detectorBehind3Left.aoeTouchingCount);
        AddVectorObs(detectorBehind3Right.aoeTouchingCount);
        AddVectorObs(detectorLeft4.aoeTouchingCount);
        AddVectorObs(detectorLeft3Forward.aoeTouchingCount);
        AddVectorObs(detectorLeft3Behind.aoeTouchingCount);
        AddVectorObs(detectorRight4.aoeTouchingCount);
        AddVectorObs(detectorRight3Forward.aoeTouchingCount);
        AddVectorObs(detectorRight3Behind.aoeTouchingCount);
        AddVectorObs(detectorForwardLeftCorner.aoeTouchingCount);
        AddVectorObs(detectorForwardRightCorner.aoeTouchingCount);
        AddVectorObs(detectorBehindLeftCorner.aoeTouchingCount);
        AddVectorObs(detectorBehindRightCorner.aoeTouchingCount);
        AddVectorObs(detectorForward4.isOutOfBoundary);
        AddVectorObs(detectorForward3Left.isOutOfBoundary);
        AddVectorObs(detectorForward3Right.isOutOfBoundary);
        AddVectorObs(detectorBehind4.isOutOfBoundary);
        AddVectorObs(detectorBehind3Left.isOutOfBoundary);
        AddVectorObs(detectorBehind3Right.isOutOfBoundary);
        AddVectorObs(detectorLeft4.isOutOfBoundary);
        AddVectorObs(detectorLeft3Forward.isOutOfBoundary);
        AddVectorObs(detectorLeft3Behind.isOutOfBoundary);
        AddVectorObs(detectorRight4.isOutOfBoundary);
        AddVectorObs(detectorRight3Forward.isOutOfBoundary);
        AddVectorObs(detectorRight3Behind.isOutOfBoundary);
        AddVectorObs(detectorForwardLeftCorner.isOutOfBoundary);
        AddVectorObs(detectorForwardRightCorner.isOutOfBoundary);
        AddVectorObs(detectorBehindLeftCorner.isOutOfBoundary);
        AddVectorObs(detectorBehindRightCorner.isOutOfBoundary);

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
        var attackAction = (int)vectorAction[3]; // 0 = no action , 1 = melee , 2 = range , 3 move 

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
            if (myTime > nextFire)
            {
                if (attackAction == 1)
                    meleeAttack();
                else if (attackAction == 2)
                    rangeAttack();
            }
        }
        AddReward(CalculateAngleReward());
        // Reduce reward player hp is at player Health
        AddReward(-aoeTouchingCount/100.0f);
        AddReward(-0.0005f); //
        if (lastBossHealth > bossHealth.getCurrentHealth())
        {
            AddReward( (lastBossHealth-bossHealth.getCurrentHealth())/20.0f ); // 0.1 per 2 hp
            lastBossHealth = bossHealth.getCurrentHealth();
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
        gameObject.transform.position = new Vector3(0, 1, -5)
            + gameInstance.transform.position;

        boss.transform.position = new Vector3(0, 2, 0) + gameInstance.transform.position;
        bossHealth.ResetHealthAndAttacks();
        playerHealth.ResetHealth();
        gameInstanceManager.DestroyAllAoe();


        myTime = 0.0F;
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
            return (90f - angle) / (90f * 200f);
        }
        else // 90-180
        {
            return -(angle - 90f) / (90f * 200f);
        }
    }


    //------ Player Movement ------------------------------------------------------------------------------


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Aoe")
        {
            aoeTouchingCount += 1;
            aoeTouching.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Aoe")
        {
            aoeTouchingCount -= 1;
            aoeTouching.Remove(other);
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
