using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack1 : MonoBehaviour,ISubAttack {

    public GameObject AoeObject;
    private float executeTime;

    private void Start()
    {
        executeTime = 3.0f;
    }
    public void Perform ()
    {
        Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
        Instantiate(AoeObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation).GetComponent<AoeAttack>().setDelay(executeTime);
    }
    public float GetExecuteTime()
    {
        return executeTime;
    }
}
