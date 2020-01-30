using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack3 : MonoBehaviour,ISubAttack {

    // Spawn +,  interval

    public GameObject AoeObject;
    private float executeTime;

    private void Start()
    {
        executeTime = 3.0f;
    }
    public void Perform ()
    {
        StartCoroutine(SpawnAoe());

    }
    public float GetExecuteTime()
    {
        return executeTime;
    }
    private IEnumerator SpawnAoe()
    {
        float intervalDelay = 0.5f;
        float aoeDelay = 3.0f;
        Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation).GetComponent<AoeAttack>().setDelay(aoeDelay);
        yield return new WaitForSeconds(intervalDelay);
        //Instantiate(AoeObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation).GetComponent<AoeAttack>().setDelay(delay);
        //Instantiate(AoeObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation).GetComponent<AoeAttack>().setDelay(delay);
        //Instantiate(AoeObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation).GetComponent<AoeAttack>().setDelay(delay);
    }
}
