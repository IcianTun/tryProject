using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuleBased : MonoBehaviour {


    [Header("Game SetUp")]
    public GameInstanceManager gameInstanceManager;
    public GameObject boss;

    [Header("GA things")]
    public float bossAttackUptime;
    public int aoeTouchingCount = 0;

    //[Header("From PlayerMovement")]
    PlayerMovement playerMovement;
    List<Collider> aoeTouching = new List<Collider>();

    [Header("From PlayerAttack")]
    public GameObject shot;
    public Transform[] shotSpawn;
    Sword swordScript;
    float myTime = 0.0F;
    float attackDelay = 1.0f;
    float attackingTimer = 0.8f;
    float nextFire;
    bool isInAttackAnimation;

    [Header("My Rule-Based Things")]
    ////public List<Detector> detectorList;
    ////private Queue<List<DetectorData>> queueLeastCountDetectors;
    //private Queue<float> times;
    public float distance;
    public float angle;
    public bool moveFront;
    public bool moveRight;

    [Header("tst")]
    public Vector3 targetPosNow;
    public List<OverlapDetectData> tstOverlapList;
    public List<Vector3> tstVector3List;

    private void Start()
    {
        //detectorList = new List<Detector>();
        ////queueLeastCountDetectors = new Queue<List<DetectorData>>();
        //times = new Queue<float>();
        //boss = gameInstanceManager.boss;
        //if (!boss)
        //{
        //    gameInstanceManager.GetComponent<Generator>().GenerateABoss();
        //    boss = gameInstanceManager.boss;
        //}
        bossAttackUptime = 0;
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enableManualControl = false;
        swordScript = GetComponentInChildren<Sword>();
        isInAttackAnimation = false;
        
    }

    //private void FixedUpdate()
    //{
    //    if (!isInAttackAnimation) {

    //    }
    //}
    

    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (myTime > attackingTimer)
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
        if(aoeTouchingCount != 0)
        {
            bossAttackUptime += Time.deltaTime;
        }

        distance = CalculateDistanceBetweenPlayerBoss();
        angle = CalculateAngle();

        ////queueLeastCountDetectors.Enqueue(currentData);
        //times.Enqueue(Time.time);
        //while (times.Peek() < Time.time - 2)
        //{
        //    ////queueLeastCountDetectors.Dequeue();
        //    times.Dequeue();
        //}

        if (!isInAttackAnimation)
        {
            Move();
            Attack();
        }


    }
    void Move()
    {
        /*
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
        */

        /*
        List<Vector3> possibileTargetPos = GetClosestPositions(LeastOverlapData());
        leastOverlapData = LeastOverlapData();
        possibileTargetPosTst = possibileTargetPos;
        Vector3 targetPos = GetClosestPositionsToBoss(possibileTargetPos);
        //Debug.Log(transform.position == targetPos);
        targetPosNow = targetPos;
        */
        Vector3 targetPos;
        bool isMove = true;
        if(aoeTouchingCount == 0)
        {
            List<OverlapDetectData> innerOverlapData = InnerOverlapData();
            innerOverlapData = FilterNoTouchData(innerOverlapData);
            if(innerOverlapData.Count == 0)
            {
                //Debug.Log("can't find no touch kubbb => yu t derm naaa");
                targetPos = Vector3.zero;
                isMove = false;
            
            } else
            {
                targetPos = SelectPositionClosestToBoss(innerOverlapData);
                targetPosNow = targetPos;
            }
        } else
        {
            List<OverlapDetectData> overlapDatas = OuterOverlapData();
            overlapDatas.AddRange(InnerOverlapData());
            overlapDatas = FilterLeastCountOverlapData(overlapDatas);
            List<Vector3> possibleTargets = GetClosestPositions(overlapDatas);
            tstVector3List = possibleTargets;
            targetPos = GetClosestPositionsToBoss(possibleTargets);
            targetPosNow = targetPos;
        }



        if (isMove)
        {
            float v, h;
            if (IsToTheFront(targetPos))
            {
                moveFront = true;
                v = 1;
            }
            else
            {
                moveFront = false;
                v = -1;
            }
            if (IsToTheRight(targetPos))
            {
                moveRight = true;
                h = 1;
            }
            else
            {
                moveRight = false;
                h = -1;
            }
            playerMovement.Move(v, h);
        }
        

        //Rotate
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;
        var targetRotation = Quaternion.LookRotation(bossPosition - playerPosition);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, playerMovement.rotationSpeed * Time.deltaTime);
    }

    void Attack()
    {

        if (myTime > nextFire && aoeTouchingCount == 0)
        {
            if (IsBossObjectToTheFront() && distance < 3.0f)
                meleeAttack();
            else if( angle < 30)
                rangeAttack();
        }

    }

    #region oldDetector
    //List<DetectorData> LeastCountDetectorsNow()
    //{
    //    List<DetectorData> result = new List<DetectorData>();
    //    int currCount = 512;
    //    for (int i = 0; i < detectorList.Count; i++)
    //    {
    //        if (!detectorList[i].isOutOfBoundary)
    //        {
    //            if (currCount == 512)
    //            {
    //                result.Add(detectorList[i].GetData());
    //                currCount = detectorList[i].aoeTouchingCount;
    //            }
    //            else if (detectorList[i].aoeTouchingCount < currCount)
    //            {
    //                result.Clear();
    //                result.Add(detectorList[i].GetData());
    //                currCount = detectorList[i].aoeTouchingCount;
    //            }
    //            else if (detectorList[i].aoeTouchingCount == currCount)
    //            {
    //                result.Add(detectorList[i].GetData());
    //            }
    //        }

    //    }
    //    return result;
    //}
    // ---------------------------------------------------------------------------------
    #endregion

    List<OverlapDetectData> InnerOverlapData()
    {
        List<OverlapDetectData> result = new List<OverlapDetectData>();
        float spacing = 0.3f;
        for (int range = 1; range < 3; range++)
        {
            #region comment
            ///                     1  1  1  1  2    +Z
            ///                     4  O  O  O  2    ^
            ///                     4  O  O  O  2    ^ 
            ///                     4  O  O  O  2     -> +X
            ///                     4  3  3  3  3
            /// var v1 : Vector3 = Vector3( 1, 2, 3 );
            /// var v2 : Vector3 = Vector3(4, 5, 6);
            /// Vector3.Scale(v1, v2);  // = ( 4, 10, 18 ) maybe multiple with forward vector.
            #endregion
            Vector3 startPosForwardLeft = transform.position + new Vector3(-spacing * range, 0, spacing * range);
            Vector3 startPosForwardRight = transform.position + new Vector3(spacing * range, 0, spacing * range);
            Vector3 startPosBehindRight = transform.position + new Vector3(spacing * range, 0, -spacing * range);
            Vector3 startPosBehindLeft = transform.position + new Vector3(-spacing * range, 0, -spacing * range);
            for (int i = 0; i < range * 2; i++)
            {
                Vector3 castPos1 = startPosForwardLeft + new Vector3(+spacing * i, 0, 0);
                Vector3 castPos2 = startPosForwardRight + new Vector3(0, 0, -spacing * i);
                Vector3 castPos3 = startPosBehindRight + new Vector3(-spacing * i, 0, 0);
                Vector3 castPos4 = startPosBehindLeft + new Vector3(0, 0, spacing * i);

                result.Add(OverlapDataAt(castPos1));
                result.Add(OverlapDataAt(castPos2));
                result.Add(OverlapDataAt(castPos3));
                result.Add(OverlapDataAt(castPos4));
            }
        }
        return result;
    }

    List<OverlapDetectData> OuterOverlapData()
    {
        List<OverlapDetectData> result = new List<OverlapDetectData>();
        float spacing = 0.8f;
        for (int range = 1; range < 15; range++)
        {
            #region comment
            ///                     1  1  1  1  2    +Z
            ///                     4  O  O  O  2    ^
            ///                     4  O  O  O  2    ^ 
            ///                     4  O  O  O  2     -> +X
            ///                     4  3  3  3  3
            /// var v1 : Vector3 = Vector3( 1, 2, 3 );
            /// var v2 : Vector3 = Vector3(4, 5, 6);
            /// Vector3.Scale(v1, v2);  // = ( 4, 10, 18 ) maybe multiple with forward vector.
            #endregion
            Vector3 startPosForwardLeft = transform.position + new Vector3(-spacing * range, 0, spacing * range);
            Vector3 startPosForwardRight = transform.position + new Vector3(spacing * range, 0, spacing * range);
            Vector3 startPosBehindRight = transform.position + new Vector3(spacing * range, 0, -spacing * range);
            Vector3 startPosBehindLeft = transform.position + new Vector3(-spacing * range, 0, -spacing * range);
            for (int i = 0; i < range * 2; i++)
            {
                Vector3 castPos1 = startPosForwardLeft + new Vector3(+spacing * i, 0, 0);
                Vector3 castPos2 = startPosForwardRight + new Vector3(0, 0, -spacing * i);
                Vector3 castPos3 = startPosBehindRight + new Vector3(-spacing * i, 0, 0);
                Vector3 castPos4 = startPosBehindLeft + new Vector3(0, 0, spacing * i);

                result.Add(OverlapDataAt(castPos1));
                result.Add(OverlapDataAt(castPos2));
                result.Add(OverlapDataAt(castPos3));
                result.Add(OverlapDataAt(castPos4));
            }
        }
        return result;
    }

    List<OverlapDetectData> FilterNoTouchData(List<OverlapDetectData> dataList)
    {
        List<OverlapDetectData> result = new List<OverlapDetectData>();

        foreach (OverlapDetectData overlapDetectData in dataList)
        {
            if (!overlapDetectData.isOutOfBoundary && overlapDetectData.aoeTouchingCount == 0)
            {
                result.Add(overlapDetectData);
            }
        }
        return result;
    }

    List<OverlapDetectData> FilterLeastCountOverlapData(List<OverlapDetectData> overlapDetectDataList)
    {
        List<OverlapDetectData> result = new List<OverlapDetectData>();
        int currCount = 512;

        foreach (OverlapDetectData overlapDetectData in overlapDetectDataList)
        {
            if (!overlapDetectData.isOutOfBoundary)
            {
                if (currCount == 512)
                {
                    result.Add(overlapDetectData);
                    currCount = overlapDetectData.aoeTouchingCount;
                }
                else if (overlapDetectData.aoeTouchingCount < currCount)
                {
                    result.Clear();
                    result.Add(overlapDetectData);
                    currCount = overlapDetectData.aoeTouchingCount;
                }
                else if (overlapDetectData.aoeTouchingCount == currCount)
                {
                    result.Add(overlapDetectData);
                }
            }
        }
        return result;
    }

    Vector3 SelectPositionClosestToBoss(List<OverlapDetectData> dataList)
    {
        Vector3 bestTarget = Vector3.zero;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 bossPosition = boss.transform.position;
        foreach (OverlapDetectData data in dataList)
        {
            Vector3 directionToTarget =  bossPosition - data.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = data.position;
            }
        }
        return bestTarget;
    }

    List<Vector3> GetClosestPositions(List<OverlapDetectData> overlapDetectDataList)
    {
        List<Vector3> closestPositions = new List<Vector3>();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = transform.position;
        foreach (OverlapDetectData potentialMoveTarget in overlapDetectDataList)
        {
            Vector3 directionToTarget = potentialMoveTarget.position - playerPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestPositions.Clear();
                closestPositions.Add(potentialMoveTarget.position);
            } else if( Mathf.Approximately(dSqrToTarget,closestDistanceSqr) )
            {
                closestPositions.Add(potentialMoveTarget.position);
            }
        }
        return closestPositions;
    }

    Vector3 GetClosestPositionsToBoss(List<Vector3> targetList)
    {
        Vector3 bestTarget = Vector3.zero;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 bossPosition = boss.transform.position;
        foreach (Vector3 potentialTarget in targetList)
        {
            Vector3 directionToTarget = bossPosition - potentialTarget;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    OverlapDetectData OverlapDataAt(Vector3 targetPos)
    {
        Collider[] hitColliders = Physics.OverlapBox(targetPos, transform.localScale / 2, transform.rotation, LayerMask.GetMask("Aoe"));

        OverlapDetectData data = new OverlapDetectData
        {
            position = targetPos,
            rotation = transform.rotation,
            aoeTouchingCount = hitColliders.Length,
            isOutOfBoundary = Mathf.Abs(targetPos.x - gameInstanceManager.transform.position.x) >= 15.0f ||
                               Mathf.Abs(targetPos.z - gameInstanceManager.transform.position.z) >= 15.0f
        };
        return data;
    }


    // ---------------------------------------------------------------------------------
    bool IsToTheRight(Vector3 targetPos)
    {
        return Vector3.Dot(transform.right, targetPos - transform.position) > 0;
    }
    bool IsToTheFront(Vector3 targetPos)
    {
        return Vector3.Dot(transform.forward, targetPos - transform.position) > 0;
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
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;
        return Vector3.Angle(transform.forward, bossPos - playerPosition);
    }

    float CalculateDistanceBetweenPlayerBoss()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;
        return Vector3.Distance(bossPosition, playerPosition);
    }
    //------ ------------------------------------------------------------------------------


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

}
