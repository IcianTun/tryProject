using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Generator : MonoBehaviour {

    public const int ATTACK_SIZE = 5;
    public const int SIZE_OF_ACTION_IN_ATTACK = 4;

    public GameObject bossTemplate;
    public List<GameObject> actionTemplates;
    public SubAttackAtCoordinate tstSubattack;

    GameInstanceManager gameInstanceManager;
    //GameObject myGeneratedBoss;

    private void Awake()
    {
        gameInstanceManager = GetComponent<GameInstanceManager>();
    }
    

    public GameObject GenerateABoss(int markNumber)
    {
        GameObject generatedBoss = Instantiate(bossTemplate, transform);
        gameInstanceManager.AssignBossToThisInstance(generatedBoss);
        BossAttackController bossAttackCon = generatedBoss.GetComponent<BossAttackController>();
        //myGeneratedBoss = generatedBoss;

        for(int i = 0; i < ATTACK_SIZE; i++)
        {
            GameObject anAttackGameObject = GenerateAnAttack();
            bossAttackCon.attackList.Add(anAttackGameObject.GetComponent<Attack>());
            anAttackGameObject.transform.SetParent(generatedBoss.transform);
            anAttackGameObject.name = "Attack " + i;
        }
        generatedBoss.name = "Initial " + markNumber;
        // set active false is in GA.cs
        return generatedBoss;
    }

    GameObject GenerateAnAttack()
    {
        GameObject empty = new GameObject();
        Attack attackScript = empty.AddComponent<Attack>();
        //attackScript.delayAfterAttack = 1.5f;

        for(int i = 0; i < SIZE_OF_ACTION_IN_ATTACK; i++)
        {
            GameObject anActionGameObject = GenerateRandomAction();
            attackScript.actionsList.Add(anActionGameObject.GetComponent<IAction>());
            attackScript.MyAwake();
            anActionGameObject.transform.SetParent(empty.transform);
        }
        return empty;
    }

    public static GameObject GenerateRandomAction()
    {
        int type = Random.Range(0, 3);
        switch (type)
        {
            case 0:
                return GenerateRandomAttack();
            case 1:
                return GenerateRandomMove();
            case 2:
                return GenerateBlankAction();
            default:
                Debug.Log("Impossible GENERATE ACTION!");
                return null;
        }
    }

    static GameObject GenerateRandomAttack()
    {
        int type = Random.Range(0, 4);
        switch (type)
        {
            case 0:
                return GenerateSubAttackAtBoss();
            case 1:
                return GenerateSubAttackAtCoordinate();
            case 2:
                return GenerateSubAttackAtPlayer();
            case 3:
                return GenerateSubAttackTowardPlayer();
            default:
                Debug.Log("Impossible GENERATE ATTACK!!");
                return null;
        }
    }

    static GameObject GenerateRandomMove()
    {
        int type = Random.Range(0, 2);
        switch (type)
        {
            case 0:
                return GenerateBasicMove();
            case 1:
                return GenerateMoveToward();
            default:
                Debug.Log("Impossible GENERATE RANDOM MOVE");
                return null;
        }
    }

    static GameObject GenerateSubAttackAtBoss()
    {
        GameObject empty = new GameObject();
        SubAttackAtBoss tstSubattack = empty.AddComponent<SubAttackAtBoss>();
        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);
        empty.name = "Attack At Boss";
        return empty;
    }

    static GameObject GenerateSubAttackAtCoordinate()
    {
        GameObject empty = new GameObject();
        SubAttackAtCoordinate tstSubattack = empty.AddComponent<SubAttackAtCoordinate>();

        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);

        if (Random.value > 0.5f)
        {
            tstSubattack.coordinateName = CoordinateName.none;
            tstSubattack.xPos = Random.Range(-15f, 15f);
            tstSubattack.zPos = Random.Range(-15f, 15f);
        }
        else
        {
            tstSubattack.coordinateName = (CoordinateName)Random.Range(0, System.Enum.GetValues(typeof(CoordinateName)).Length - 1);
        }
        empty.name = "Attack At Coordinate";
        return empty;
    }

    static GameObject GenerateSubAttackAtPlayer()
    {
        GameObject empty = new GameObject();
        SubAttackAtPlayer tstSubattack = empty.AddComponent<SubAttackAtPlayer>();
        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);
        empty.name = "Attack At Player";
        return empty;
    }

    static GameObject GenerateSubAttackTowardPlayer()
    {
        GameObject empty = new GameObject();
        SubAttackTowardPlayer tstSubattack = empty.AddComponent<SubAttackTowardPlayer>();

        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);
        tstSubattack.plusDistance = Random.Range(-10f, 10f);
        tstSubattack.minLength = Random.Range(5f, 10f);
        tstSubattack.aoeType = AoeType.Square;
        empty.name = "Attack Toward Player";
        return empty;
    }

    static GameObject GenerateBasicMove()
    {
        GameObject empty = new GameObject();
        BaseBossMoveAction tstMove = empty.AddComponent<BaseBossMoveAction>();

        RandomBasicParams(tstMove);

        if (Random.value > 0.5f)
        {
            tstMove.coordinateName = CoordinateName.none;
            tstMove.xPos = Random.Range(-15f, 15f);
            tstMove.zPos = Random.Range(-15f, 15f);
        }
        else
        {
            tstMove.coordinateName = (CoordinateName)Random.Range(0, System.Enum.GetValues(typeof(CoordinateName)).Length - 1);
        }
        empty.name = "Move Coordinate";
        return empty;
    }

    static GameObject GenerateMoveToward()
    {
        GameObject empty = new GameObject();
        MoveTowardPlayer tstMove = empty.AddComponent<MoveTowardPlayer>();

        RandomBasicParams(tstMove);
        tstMove.distanceOffset = Random.Range(-10f, 10f);
        empty.name = "MoveToward";
        return empty;
    }

    static GameObject GenerateBlankAction()
    {
        GameObject empty = new GameObject();
        empty.AddComponent<BlankAction>();
        empty.name = "Blank";
        return empty;
    }


    static private void RandomBasicParams(SubAttack subAttack)
    {
        subAttack.delayBeforeActive = Random.Range(0f, 2f);
        subAttack.delayBeforeNext = Random.Range(0f, 2f);
        subAttack.aoeTimer = Random.Range(2f, 4f);
        subAttack.damage = Random.Range(5, 20);
    }

    static private void RandomBasicParams(BaseBossMoveAction moveAction)
    {
        moveAction.delaybefore = Random.Range(0f, 2f);
        moveAction.timeToMove = Random.Range(0f, 2f);
        moveAction.delayAfter = Random.Range(0f, 2f);

    }

    private static void RandomAoeTypeParams(SubAttack subattack)
    {
        subattack.aoeType = (AoeType)Random.Range(0, System.Enum.GetValues(typeof(AoeType)).Length);
        if (subattack.aoeType == AoeType.Square)
        {
            RandomSquareAoeParams(subattack);
        }
        else if (subattack.aoeType == AoeType.Circle)
        {
            RandomCircleAoeParams(subattack);
        }
    }

    private static  void RandomSquareAoeParams(SubAttack subattack)
    {
        subattack.xSize = Random.Range(4f, 25f);
        subattack.zSize = Random.Range(4f, 25f);
        subattack.rotation = Random.Range(0, 180f);
    }

    private static void RandomCircleAoeParams(SubAttack subattack)
    {
        subattack.diameter = Random.Range(4f, 25f);
    }


}


public enum ActionName
{
    SubAttackAtBoss,
    SubAttackAtCoordinate,
    SubAttackAtPlayer,
    SubAttackTowardPlayer,
    MoveToCoordinate,
    MoveTowardPlayer,
    Blank
}