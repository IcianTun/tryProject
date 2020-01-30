using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack2 : MonoBehaviour,ISubAttack {

    // section attack , north east south west

    public GameObject AoeObject;
    private float executeTime;
    private GameObject bossObject;
    private void Start()
    {
        bossObject = GameObject.FindGameObjectWithTag("Boss");
        executeTime = 8.0f;
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
        float intervalDelay = 1.0f;
        float aoeDelay = 5.0f;

        GameObject NorthAoe = Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation);
        NorthAoe.GetComponent<AoeAttack>().setDelay(aoeDelay);
        NorthAoe.transform.localScale = new Vector3(3.0f, 1.0f, 1.5f);
        yield return new WaitForSeconds(intervalDelay);
        GameObject EastAoe = Instantiate(AoeObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation);
        EastAoe.GetComponent<AoeAttack>().setDelay(aoeDelay);
        EastAoe.transform.localScale = new Vector3(1.5f, 1.0f, 3.0f);
        yield return new WaitForSeconds(intervalDelay);
        GameObject SouthAoe = Instantiate(AoeObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation);
        SouthAoe.GetComponent<AoeAttack>().setDelay(aoeDelay);
        SouthAoe.transform.localScale = new Vector3(3.0f, 1.0f, 1.5f);
        yield return new WaitForSeconds(intervalDelay);
        GameObject WestAoe = Instantiate(AoeObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation);
        WestAoe.GetComponent<AoeAttack>().setDelay(aoeDelay);
        WestAoe.transform.localScale = new Vector3(1.5f, 1.0f, 3.0f);
    }
}
