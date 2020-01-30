using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack4 : MonoBehaviour,ISubAttack {

    // Line to Player

    public GameObject AoeObject;
    private float executeTime;

    private void Start()
    {

        executeTime = 1.5f;
    }
    public void Perform ()
    {
        float aoeDelay = 1.0f;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 bossPosition = GameObject.FindGameObjectWithTag("Boss").transform.position;
        Vector3 positionBetween = (bossPosition + playerPosition) / 2;
        Instantiate(AoeObject, positionBetween , Coordinate.Instance.North.rotation).GetComponent<AoeAttack>().setDelay(aoeDelay);
    }
    public float GetExecuteTime()
    {
        return executeTime;
    }
}
