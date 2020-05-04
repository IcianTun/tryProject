using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tst : MonoBehaviour {

    public List<float> tstList;

    public List<int> indexList;

    public List<float> selected;

    public int survived_amount;
    public float HP_fitness;

    public BossStatisticData bossStatsData;

    // Use this for initialization
    void Awake () {
        tstList = new List<float>();
        for (int i = 0; i < 8; i++)
        {
            tstList.Add(i);
        }
        ////survived_amount = (int)(82 * 0.5f);
        ////HP_fitness = (float)Mathf.Abs(80 - 20) / 80;
        ////selected = tstList.GetRange(0, 3);
        ////tstList = tstList.GetRange(3,tstList.Count-(3));
        ////indexList = indexList.OrderBy(f => f).Reverse().ToList();


    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(GA.Fitness(bossStatsData));
            //float avg = tstList.Average();
            //float thisGen_SD = Mathf.Sqrt(tstList.Average(v => (v - avg) * (v - avg)));
            //Debug.Log(thisGen_SD);
            //Debug.Log(Time.time);
            //selected = tstList.Select(3);
            //Debug.Log(Time.time);

            ////List<int> crossoverPoint = new List<int>();
            ////crossoverPoint = crossoverPoint.TunRandomRanges(Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK,3);
            ////indexList = crossoverPoint;
            //for (int i = 0; i < 100; i++)
            //{
            //    float randomed = Random.value;
            //    if (randomed < 0.01f)
            //    {
            //        Debug.Log(randomed);
            //    }
            //}

        }
    }
    

}
