using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Generator : MonoBehaviour {

    public const int ATTACK_SIZE = 5;
    public const int SIZE_OF_ACTION_IN_ATTACK = 7;

    public GameObject bossTemplate;
    public List<GameObject> actionTemplates;
    public SubAttackAtCoordinate tstSubattack;

    GameInstanceManager gameInstanceManager;
    GameObject myGeneratedBoss;

    private void Awake()
    {
        gameInstanceManager = GetComponent<GameInstanceManager>();
    }

    private void Start()
    {
        //GenerateABoss();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateABoss();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(System.DateTime.Now.ToString("HH:mm:ss_dd-MM-yyyy"));
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            string localPath = "Assets/Boss/" + myGeneratedBoss.name + System.DateTime.Now.ToString("_HH:mm:ss_dd-MM-yyyy") + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            PrefabUtility.CreatePrefab(localPath, myGeneratedBoss);
        }

    }

    public void GenerateABoss()
    {
        GameObject generatedBoss = Instantiate(bossTemplate, transform);
        BossAttackController bossAttackCon = generatedBoss.GetComponent<BossAttackController>();
        bossAttackCon.gameInstanceManager = GetComponent<GameInstanceManager>();

        for(int i = 0; i < ATTACK_SIZE; i++)
        {
            GameObject anAttackGameObject = GenerateAnAttack();
            bossAttackCon.attackList.Add(anAttackGameObject.GetComponent<Attack>());
            anAttackGameObject.transform.SetParent(generatedBoss.transform);
            anAttackGameObject.name = "Attack " + i;
        }
        gameInstanceManager.boss = generatedBoss;
        generatedBoss.GetComponent<BossMovementController>().gameInstanceTransform = transform;
        myGeneratedBoss = generatedBoss;
    }

    GameObject GenerateAnAttack()
    {
        GameObject empty = new GameObject();
        Attack attackScript = empty.AddComponent<Attack>();
        attackScript.delayAfterAttack = 1.5f;

        for(int i = 0; i < SIZE_OF_ACTION_IN_ATTACK; i++)
        {
            GameObject anActionGameObject = GenerateRandomAction();
            attackScript.actionsList.Add(anActionGameObject.GetComponent<IAction>());
            attackScript.MyAwake();
            anActionGameObject.transform.SetParent(empty.transform);
        }
        return empty;
    }

    GameObject GenerateRandomAction()
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
                Debug.Log("Impossible");
                return null;
        }
    }

    GameObject GenerateRandomAttack()
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
                Debug.Log("Impossible");
                return null;
        }
    }

    GameObject GenerateRandomMove()
    {
        int type = Random.Range(0, 2);
        switch (type)
        {
            case 0:
                return GenerateBasicMove();
            case 1:
                return GenerateMoveToward();
            default:
                Debug.Log("Impossible");
                return null;
        }
    }

    GameObject GenerateSubAttackAtBoss()
    {
        GameObject empty = new GameObject();
        SubAttackAtBoss tstSubattack = empty.AddComponent<SubAttackAtBoss>();
        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);
        empty.name = "Attack At Boss";
        return empty;
    }

    GameObject GenerateSubAttackAtCoordinate()
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

    GameObject GenerateSubAttackAtPlayer()
    {
        GameObject empty = new GameObject();
        SubAttackAtPlayer tstSubattack = empty.AddComponent<SubAttackAtPlayer>();
        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);
        empty.name = "Attack At Player";
        return empty;
    }

    GameObject GenerateSubAttackTowardPlayer()
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

    GameObject GenerateBasicMove()
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

    GameObject GenerateMoveToward()
    {
        GameObject empty = new GameObject();
        MoveTowardPlayer tstMove = empty.AddComponent<MoveTowardPlayer>();

        RandomBasicParams(tstMove);
        tstMove.distanceOffset = Random.Range(-10f, 10f);
        empty.name = "MoveToward";
        return empty;
    }

    GameObject GenerateBlankAction()
    {
        GameObject empty = new GameObject();
        empty.AddComponent<BlankAction>();
        empty.name = "Blank";
        return empty;
    }


    private void RandomBasicParams(SubAttack subAttack)
    {
        subAttack.delayBeforeActive = Random.Range(0f, 2f);
        subAttack.delayBeforeNext = Random.Range(0f, 2f);
        subAttack.aoeTimer = Random.Range(2f, 4f);
        subAttack.damage = Random.Range(5, 25);
    }

    private void RandomBasicParams(BaseBossMoveAction moveAction)
    {
        moveAction.delaybefore = Random.Range(0f, 2f);
        moveAction.timeToMove = Random.Range(0f, 2f);
        moveAction.delayAfter = Random.Range(0f, 2f);

    }

    private void RandomAoeTypeParams(SubAttack subattack)
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

    private void RandomSquareAoeParams(SubAttack subattack)
    {
        subattack.xSize = Random.Range(4f, 25f);
        subattack.zSize = Random.Range(4f, 25f);
        subattack.rotation = Random.Range(0, 180f);
    }

    private void RandomCircleAoeParams(SubAttack subattack)
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