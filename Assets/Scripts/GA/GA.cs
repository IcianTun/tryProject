using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class FloatListWrapper
{
    public List<float> myList;
    public FloatListWrapper(List<float> value)
    {
        myList = value;
    }
}
[System.Serializable]
public class BossDataWrapper
{
    public List<BossStatisticData> myList;
    public BossDataWrapper(List<BossStatisticData> value)
    {
        myList = value;
    }
}

public class GA : MonoBehaviour {

    public List<GameInstanceManager> gameInstanceManagerList;

    const int CROSSOVER_POINT_AMOUNT = 3;

    public static int endCount = 0;
    public int public_endCount;
    public static int generation_Number = 1;
    public static int mark_number = 0;

    public List<FloatListWrapper> generationsFitness;
    public List<BossDataWrapper> generationsData;
    public List<float> generationsAVG;
    public List<float> generationsSD;
    public const float acceptable_avg_diff = 0.00001f;
    public const float acceptable_SD_difference = 0.2f;

    public const float survive_rate = 0.5f;

    public const float expect_timeUsed_toBeat = 120;
    public const float expect_attackUptimePercentages = 0.4f;
    public const int expect_playerHP_left = 80;

    public const float weight_timeUsed = 4;
    public const float weight_attackUptimePercent = 5;
    public const float weight_playerHP_left = 1;
    public const float weight_isBeaten = 3;

    public const float sum_weight = weight_timeUsed + weight_attackUptimePercent + weight_playerHP_left + weight_isBeaten;

    public const float mutation_rate = 0.04f;

    public bool next = false;
    public bool auto = true;

    public List<GameObject> selected;

    void Populate()
    {
        foreach(GameInstanceManager gameinstanceManager in gameInstanceManagerList)
        {
            gameinstanceManager.GetComponent<Generator>().GenerateABoss(mark_number);
            gameinstanceManager.boss.SetActive(false);
            gameinstanceManager.player.SetActive(false);
            mark_number += 1;
        }
        mark_number = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Populate();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // LOAD
            foreach (GameInstanceManager gameinstanceManager in gameInstanceManagerList)
            {
                gameinstanceManager.AssignBossToThisInstance(gameinstanceManager.boss);
                gameinstanceManager.boss.SetActive(false);
                gameinstanceManager.player.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (GameInstanceManager gameinstanceManager in gameInstanceManagerList)
            {
                gameinstanceManager.SaveBoss();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartThisGeneration();
        }
        public_endCount = endCount;
    }

    public void StartThisGeneration()
    {
        Debug.Log("Start of generation" + generation_Number);
        foreach (GameInstanceManager gameinstanceManager in gameInstanceManagerList)
        {
            gameinstanceManager.Resetkub();
            gameinstanceManager.StartFight();
        }
    }

    public void AnInstanceEnd()
    {
        endCount += 1;
        if(endCount == gameInstanceManagerList.Count)
        {
            Debug.Log("All Instance END!");
            if (next || auto)
            {
                next = false;
                SelectionPhase();
            }
            //SelectionPhase();
            endCount = 0;
        }
    }

    private void SelectionPhase()
    {
        List<GameObject> bossList = new List<GameObject>();
        foreach (GameInstanceManager gameinstanceManager in gameInstanceManagerList)
        {
            bossList.Add(gameinstanceManager.boss);
        }

        // Calculate Converge Here---
        List<float> thisGeneration_fitness = new List<float>();
        List<BossStatisticData> thisGeneration_data = new List<BossStatisticData>();
        foreach (GameObject boss in bossList)
        {
            thisGeneration_fitness.Add(Fitness(boss.GetComponent<BossStatistic>().data[0]));
            thisGeneration_data.Add(boss.GetComponent<BossStatistic>().data[0]);
        }
        float thisGen_avg = thisGeneration_fitness.Average();
        float thisGen_SD = Mathf.Sqrt(thisGeneration_fitness.Average(v => (v - thisGen_avg)* (v - thisGen_avg)));

        if(generationsAVG.Count > 1 && Mathf.Abs(thisGen_avg - generationsAVG[generationsAVG.Count-1]) <= acceptable_avg_diff)
        {
            generationsAVG.Add(thisGen_avg);
            generationsSD.Add(thisGen_SD);
            generationsData.Add(new BossDataWrapper(thisGeneration_data));
            generationsFitness.Add(new FloatListWrapper(thisGeneration_fitness));
            Debug.Log("Convergeeeeed");
            return;
        }
        //if (generationsSD.Count > 1 && Mathf.Abs(thisGen_SD - generationsSD[generationsSD.Count - 1]) <= acceptable_SD_difference)
        //{
        //    generationsAVG.Add(thisGen_avg);
        //    generationsFitness.Add(new ListWrapper(thisGeneration_fitness));
        //    generationsSD.Add(thisGen_SD);
        //    return;
        //}
        generationsAVG.Add(thisGen_avg);
        generationsSD.Add(thisGen_SD);
        generationsData.Add(new BossDataWrapper(thisGeneration_data));
        generationsFitness.Add(new FloatListWrapper(thisGeneration_fitness));

        Debug.Log("Selection Phase! G" + generation_Number);
        bossList = bossList.OrderBy(
            boss => Fitness(boss.GetComponent<BossStatistic>().data[0])
        ).Reverse().ToList();
        
        int survived_amount = (int)(bossList.Count()*survive_rate);
        foreach(GameObject boss_not_survived in bossList.GetRange(survived_amount, bossList.Count - survived_amount))
        {
            Destroy(boss_not_survived);
        }
        bossList = bossList.GetRange(0, survived_amount); // Survived
        selected = bossList;
        CrossoverPhase(bossList);

    }

    public static float Fitness(BossStatisticData bossData)
    {
        float fitness = 0;
        if (!(bossData.timeUsed > expect_timeUsed_toBeat * 2))
        {
            fitness += weight_timeUsed *
                ((expect_timeUsed_toBeat - Mathf.Abs(expect_timeUsed_toBeat - bossData.timeUsed)) / expect_timeUsed_toBeat);
            // expect(180) - [0-360] , low distance high fitness (if timeUsed > 2* expect ) it's 0 in this part
        }
        float divider = 0.5f + Mathf.Abs(0.5f - expect_attackUptimePercentages);
        fitness += weight_attackUptimePercent *
            (expect_attackUptimePercentages - Mathf.Abs(expect_attackUptimePercentages - bossData.attackUptimePercentages)) / divider; // expect - [0-1]
        int divider2 = 50 + Mathf.Abs(50 - expect_playerHP_left);
        fitness += weight_playerHP_left *
            (expect_playerHP_left - Mathf.Abs(expect_playerHP_left - bossData.playerHPLeft)) / (float)divider2;
        fitness += bossData.IsBeaten ? weight_isBeaten : 0;
        fitness = fitness / sum_weight;
        return fitness;
    }

    private void CrossoverPhase(List<GameObject> bossSurvived)
    {
        List<GameObject> nextGeneration = new List<GameObject>(bossSurvived);
        int amount = 2;
        while ( nextGeneration.Count < gameInstanceManagerList.Count)
        {
            List<GameObject> randomedBossPair = bossSurvived.TunSelect(2);

            GameObject boss1 = randomedBossPair[0];
            GameObject boss2 = randomedBossPair[1];
            if (nextGeneration.Count + 1 == gameInstanceManagerList.Count)
            {
                amount = 1;
            }
            List<GameObject> offSprings = GenerateOffSprings(boss1, boss2, amount);

            nextGeneration.Add(offSprings[0]);
            if(nextGeneration.Count != gameInstanceManagerList.Count)
            {
                nextGeneration.Add(offSprings[1]);
            }
        }
        
        for(int i = 0; i < gameInstanceManagerList.Count; i++)
        {
            gameInstanceManagerList[i].AssignBossToThisInstance(nextGeneration[i]);
        }
        // START NEXT GENEARTION HERE (?)
        if (next || auto)
        {
            next = false;
            generation_Number += 1;
            mark_number = 0;
            StartThisGeneration();
        }

    }

    public List<GameObject> GenerateOffSprings(GameObject boss1, GameObject boss2, int amount)
    {
        string[] boss1_nameSplited = boss1.name.Split(null);
        string[] boss2_nameSplited = boss2.name.Split(null);
        string boss1_mark = boss1_nameSplited[boss1_nameSplited.Length-1];
        string boss2_mark = boss2_nameSplited[boss2_nameSplited.Length-1];
        List<GameObject> offSprings = new List<GameObject>();
        GameObject offspring1 = Instantiate(boss1);
        GameObject offspring2 = Instantiate(boss2);
        offspring1.SetActive(false);
        offspring2.SetActive(false);
        offspring1.name = $"Generation[{generation_Number}]+P[{boss1_mark}_{boss2_mark}] {mark_number}";
        offspring2.name = $"Generation[{generation_Number}]+P[{boss1_mark}_{boss2_mark}] {mark_number+1}";
        mark_number += 2;
        BossAttackController offspring1Script = offspring1.GetComponent<BossAttackController>();
        BossAttackController offspring2Script = offspring2.GetComponent<BossAttackController>();

        List<IAction> offspring1ActionList = new List<IAction>();
        List<IAction> offspring2ActionList = new List<IAction>();
        foreach (Attack atk in offspring1Script.attackList)
        {
            offspring1ActionList.AddRange(atk.actionsList);
        }
        foreach (Attack atk in offspring2Script.attackList)
        {
            offspring2ActionList.AddRange(atk.actionsList);
        }

        int iterator = 0;
        List<int> crossoverPoint = new List<int>();
        crossoverPoint = crossoverPoint.TunRandomRanges(Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK, CROSSOVER_POINT_AMOUNT);
        int crossoverPointIterator = 0;
        bool isCrossing = true;
        //string print = "";
        //foreach( var e in crossoverPoint)
        //{
        //    print += e + " ";
        //}
        //Debug.Log(print);

        while (iterator < Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK)
        {
            if (crossoverPointIterator != crossoverPoint.Count && iterator >= crossoverPoint[crossoverPointIterator])
            {
                //Debug.Log("iterator" + iterator);
                //Debug.Log("crossoverPITR"+crossoverPointIterator);
                isCrossing = !isCrossing;
                crossoverPointIterator += 1;
                //iterator = crossoverPoint[crossoverPointIterator];
            }
            if (isCrossing)
            {
                //Debug.Log("cross " + iterator);
                var tmp = offspring1ActionList[iterator];
                offspring1ActionList[iterator] = offspring2ActionList[iterator];
                offspring2ActionList[iterator] = tmp;
            }

            iterator += 1;
        }
        // MUTATION
        for (int i = 0; i < offspring1ActionList.Count; i++)
        {
            if(Random.value < mutation_rate)
            {
                GameObject newAction = Generator.GenerateRandomAction();
                offspring1ActionList[i] = newAction.GetComponent<IAction>();
                newAction.name += " M" + generation_Number;
            }
            if (Random.value < mutation_rate)
            {
                GameObject newAction = Generator.GenerateRandomAction();
                offspring2ActionList[i] = newAction.GetComponent<IAction>();
                newAction.name += " M" +generation_Number;
            }
        }


        for (int i = 0; i < Generator.ATTACK_SIZE; i++)
        {
            for(int j = 0; j < Generator.SIZE_OF_ACTION_IN_ATTACK; j++)
            {
                offspring1Script.attackList[i].actionsList[j] = offspring1ActionList[i * Generator.SIZE_OF_ACTION_IN_ATTACK + j];
                offspring2Script.attackList[i].actionsList[j] = offspring2ActionList[i * Generator.SIZE_OF_ACTION_IN_ATTACK + j];

                offspring1ActionList[i * Generator.SIZE_OF_ACTION_IN_ATTACK + j].transform.parent = offspring1Script.attackList[i].transform;
                offspring2ActionList[i * Generator.SIZE_OF_ACTION_IN_ATTACK + j].transform.parent = offspring2Script.attackList[i].transform;
            }
        }

        if(amount == 1)
        {
            offSprings.Add(offspring1);
            Destroy(offspring2);
        }
        else
        {
            offSprings.Add(offspring1);
            offSprings.Add(offspring2);
        }

        return offSprings;

    }

}


