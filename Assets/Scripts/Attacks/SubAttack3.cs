using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack3 : MonoBehaviour,ISubAttack {

    // Spawn +,  interval

    public GameObject AoeObject;
    public float executeTime;
    
    public void Perform ()
    {
        //StartCoroutine(SpawnAoe());
        Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation).GetComponent<AoeAttack>().setDelay(executeTime);

    }
    public float GetExecuteTime()
    {
        return executeTime;
    }

    public void myAwake()
    {

    }

}
