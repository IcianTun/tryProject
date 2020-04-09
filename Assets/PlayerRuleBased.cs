using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuleBased : MonoBehaviour {


    [Header("Game SetUp")]
    public GameInstanceManager gameInstanceManager;
    //GameObject gameInstance;
    GameObject boss;
    BossHealth bossHealth;
    int lastBossHealth;

    PlayerHealth playerHealth;

    [Header("From PlayerMovement")]
    PlayerMovement playerMovement;
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
    bool isInAttackAnimation;

    [Header("My Rule-Based Things")]
    public List<Detector> detectorList;
    private Queue<List<DetectorData>> queueLeastCountDetectors;
    //private List<Data> 
    private Queue<float> times;
    public float distance;
    public float angle;

    private void Start()
    {
        //detectorList = new List<Detector>();
        queueLeastCountDetectors = new Queue<List<DetectorData>>();
        times = new Queue<float>();
        //boss = gameInstance.transform.Find("Boss").gameObject;
        //gameInstance = gameInstanceManager.gameObject;
        boss = gameInstanceManager.boss;
        bossHealth = boss.GetComponent<BossHealth>();
        lastBossHealth = bossHealth.getCurrentHealth();
        
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enableManualControl = false;
        swordScript = GetComponentInChildren<Sword>();
        isInAttackAnimation = false;
        
    }
    private void FixedUpdate()
    {
        if (!isInAttackAnimation) {

        }
    }



    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > attackingTime)
        {
            isInAttackAnimation = false;
        }
        for (int i = aoeTouching.Count - 1; i >= 0; i--)
        {
            if (aoeTouching[i] == null)
            {
                aoeTouchingCount -= 1;
                aoeTouching.RemoveAt(i);
            }
        }
        distance = CalculateDistanceBetweenPlayerBoss();
        angle = CalculateAngle();

        //queueLeastCountDetectors.Enqueue(currentData);
        times.Enqueue(Time.time);
        while (times.Peek() < Time.time - 2)
        {
            //queueLeastCountDetectors.Dequeue();
            times.Dequeue();
        }

        if (!isInAttackAnimation)
        {
            DecisionMove();
            DecisionAttack();
        }


    }
    void DecisionMove()
    {
        List<DetectorData> dataList = LeastCountDetectorsNow();
        DetectorData target = dataList[0];
        float v, h;
        if (IsToTheFront(target.transformData))
            v = 1;
        else
            v = -1;
        if (IsToTheRight(target.transformData))
            h = 1;
        else
            h = -1;
        //float v = Input.GetAxisRaw("Vertical");
        //float h = Input.GetAxisRaw("Horizontal");
        playerMovement.Move(v,h);
        if (angle > 15)
        {
            playerMovement.Turning(!IsBossObjectToTheRight(), IsBossObjectToTheRight());
        }
    }

    void DecisionAttack()
    {

        if (myTime > nextFire && aoeTouchingCount == 0)
        {
            if (IsBossObjectToTheFront() && distance < 4)
                meleeAttack();
            else if( angle < 30)
                rangeAttack();
        }

    }

    // ---------------------------------------------------------------------------------

    List<DetectorData> LeastCountDetectorsNow()
    {
        List<DetectorData> result = new List<DetectorData>();
        int currCount = 512;
        for (int i = 0; i < detectorList.Count; i++)
        {
            if (!detectorList[i].isOutOfBoundary)
            {
                if (currCount == 512)
                {
                    result.Add(detectorList[i].GetData());
                    currCount = detectorList[i].aoeTouchingCount;
                }
                else if (detectorList[i].aoeTouchingCount < currCount)
                {
                    result.Clear();
                    result.Add(detectorList[i].GetData());
                    currCount = detectorList[i].aoeTouchingCount;
                }
                else if (detectorList[i].aoeTouchingCount == currCount)
                {
                    result.Add(detectorList[i].GetData());
                }
            }
            
        }
        return result;
    }

    bool IsToTheRight(Transform targetTransform)
    {
        return Vector3.Dot(transform.right, targetTransform.position - transform.position) > 0;
    }
    bool IsToTheFront(Transform targetTransform)
    {
        return Vector3.Dot(transform.forward, targetTransform.position - transform.position) > 0;
    }

    bool IsBossObjectToTheRight()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;

        return Vector3.Dot(transform.right, bossPosition - playerPosition) > 0;
    }

    bool IsBossObjectToTheFront()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;

        return Vector3.Dot(transform.forward, bossPosition - playerPosition) > 0;
    }

    float CalculateAngle()
    {
        Vector3 bossPos = boss.transform.position;
        bossPos.y = 0;
        var dot = Vector3.Dot(transform.forward, (bossPos - transform.position).normalized);
        return Mathf.Acos(dot) * Mathf.Rad2Deg;
    }

    float CalculateDistanceBetweenPlayerBoss()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;
        return Vector3.Distance(bossPosition, playerPosition);
    }


    //------  ------------------------------------------------------------------------------


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
        isInAttackAnimation = true;
        //playerMovement.enabled = false;
    }

    void rangeAttack()
    {
        nextFire = myTime + attackDelay;
        Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation).transform.parent = gameInstanceManager.transform;
        Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation).transform.parent = gameInstanceManager.transform;
        nextFire = nextFire - myTime;
        myTime = 0.0f;
        isInAttackAnimation = true;
        //playerMovement.enabled = false;
    }
    // -- ------------------------------------------------------------------------------------------
}
