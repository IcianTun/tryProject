using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack4 : MonoBehaviour,ISubAttack {

    // Line to Player

    public GameObject AoeObject;
    public float executeTime;

    public void Perform ()
    {
        Debug.Log(4);
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 bossPosition = GameObject.FindGameObjectWithTag("Boss").transform.position;
        Vector3 positionBetween = (bossPosition + playerPosition) / 2;
        GameObject newAoeObject = Instantiate(AoeObject, positionBetween, Coordinate.Instance.North.rotation);
        newAoeObject.GetComponent<AoeAttack>().setDelay(executeTime);
        newAoeObject.transform.LookAt(playerPosition);
    }
    public float GetExecuteTime()
    {
        return executeTime;
    }

    public void myAwake()
    {

    }

}
