using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA : MonoBehaviour {

    public List<GameInstanceManager> gameInstanceManagerList;

    const int CROSSOVER_POINT_AMOUNT = 3;

    private void Awake()
    {
        gameInstanceManagerList[0].GetComponent<Generator>().GenerateABoss();
        gameInstanceManagerList[1].GetComponent<Generator>().GenerateABoss();
        //MixBoss();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MixBoss();
        }

    }

    private void MixBoss()
    {
        List<int> indexNumberList = new List<int>();
        List<GameInstanceManager> randomedBossPair = gameInstanceManagerList.Select(2, indexNumberList);
        
        GameObject boss1 = randomedBossPair[0].boss;
        GameObject boss2 = randomedBossPair[1].boss;
        //BossAttackController bossScript1 = boss1.GetComponent<BossAttackController>();
        //BossAttackController bossScript2 = boss2.GetComponent<BossAttackController>();

        //List<Attack> attackList1 = bossScript1.attackList;
        //List<Attack> attackList2 = bossScript2.attackList;
        //var tmp = attackList1;
        //bossScript1.attackList = attackList2;
        //bossScript2.attackList = tmp;

        Crossover(boss1, boss2);


        


    }

    public void Crossover(GameObject boss1, GameObject boss2)
    {
        BossAttackController boss1Script = boss1.GetComponent<BossAttackController>();
        BossAttackController boss2Script = boss2.GetComponent<BossAttackController>();

        List<Attack> attackList1 = boss1Script.attackList;
        List<Attack> attackList2 = boss2Script.attackList;

        List<IAction> boss1ActionList = new List<IAction>();
        List<IAction> boss2ActionList = new List<IAction>();
        foreach (Attack atk in attackList1)
        {
            boss1ActionList.AddRange(atk.actionsList);
        }
        foreach (Attack atk in attackList2)
        {
            boss2ActionList.AddRange(atk.actionsList);
        }


        int iterator = 0;
        List<int> crossoverPoint = new List<int>();
        crossoverPoint = crossoverPoint.TunRandomRanges(Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK, CROSSOVER_POINT_AMOUNT);
        int crossoverPointIterator = 0;
        bool isCrossing = true;
        string print = "";
        foreach( var e in crossoverPoint)
        {
            print += e + " ";
        }
        Debug.Log(print);

        while(iterator < Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK)
        {
            if (crossoverPointIterator != crossoverPoint.Count && iterator >= crossoverPoint[crossoverPointIterator])
            {
                //Debug.Log("iterator" + iterator);
                //Debug.Log("crossoverP IT"+crossoverPointIterator);
                isCrossing = !isCrossing;
                crossoverPointIterator += 1;
                //iterator = crossoverPoint[crossoverPointIterator];
            }
            if (isCrossing)
            {
                //Debug.Log("cross " + iterator);
                var tmp = boss1ActionList[iterator];
                boss1ActionList[iterator] = boss2ActionList[iterator];
                boss2ActionList[iterator] = tmp;
            }

            iterator += 1;
        }

        for(int i = 0; i < Generator.ATTACK_SIZE; i++)
        {
            for(int j = 0; j < Generator.SIZE_OF_ACTION_IN_ATTACK; j++)
            {
                boss1Script.attackList[i].actionsList[j] = boss1ActionList[i * Generator.ATTACK_SIZE + j];
                boss2Script.attackList[i].actionsList[j] = boss2ActionList[i * Generator.ATTACK_SIZE + j];
            }
        }

    }

}


