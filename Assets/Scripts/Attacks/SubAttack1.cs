using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack1 : MonoBehaviour,ISubAttack {

    public GameObject AoeObject;
    public float executeTime;
    
    public void Perform ()
    {
        Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
    }

    public void myAwake()
    {

    }

    public float GetExecuteTime()
    {
        return executeTime;
    }
}
