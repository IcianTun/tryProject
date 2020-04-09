using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject bossTemplate;
    public List<GameObject> actionTemplates;
    public GameObject subAttackAtBossTemplate;

    public SubAttackAtBoss subattack1;
    public SubAttackAtCoordinate tstSubattack;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateSubAttackAtBoss();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            subattack1.Perform(gameObject.GetComponent<GameInstanceManager>());
        }

    }

    GameObject GenerateSubAttackAtBoss()
    {
        GameObject empty = new GameObject();
        subattack1 = empty.AddComponent<SubAttackAtBoss>();
        RandomBasicParams(subattack1);
        RandomAoeTypeParams(subattack1);
        return empty;
    }

    GameObject GenerateSubAttackAtCoordinate()
    {
        GameObject empty = new GameObject();
        tstSubattack = empty.AddComponent<SubAttackAtCoordinate>();

        RandomBasicParams(tstSubattack);
        RandomAoeTypeParams(tstSubattack);

        if (Random.value > 0.5f)
        {
            tstSubattack.coordinateName = CoordinateName.none;
        } else
        {
            tstSubattack.coordinateName = (CoordinateName )Random.Range(0, System.Enum.GetValues(typeof(CoordinateName)).Length - 1);
        }

        return empty;
    }

    private void RandomBasicParams(SubAttack subAttack)
    {
        subAttack.delayBeforeActive = Random.Range(0f, 2f);
        subAttack.delayBeforeNext = Random.Range(0f, 2f);
        subAttack.aoeTimer = Random.Range(2f, 4f);
        subAttack.damage = Random.Range(5, 25);
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
