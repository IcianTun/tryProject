using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour {

    public List<float> tstList;

    public List<int> indexList;

    public List<float> selected;

    // Use this for initialization
    void Awake () {
        tstList = new List<float>();
        for (int i = 0; i < 8; i++)
        {
            tstList.Add(i);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Debug.Log(Time.time);
            //selected = tstList.Select(3);
            //Debug.Log(Time.time);

            List<int> crossoverPoint = new List<int>();
            crossoverPoint = crossoverPoint.TunRandomRanges(Generator.ATTACK_SIZE * Generator.SIZE_OF_ACTION_IN_ATTACK,3);
            indexList = crossoverPoint;
        }
    }
    

}
