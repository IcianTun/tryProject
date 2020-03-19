using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackTowardPlayer : SubAttack {


    override public void Perform(GameInstanceManager gameInstanceManager)
    {
        GameObject playerObject = gameInstanceManager.player; 
        GameObject bossObject = gameInstanceManager.boss; 
        Debug.Log("Toward");
        GameObject NorthAoe = Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation);
        NorthAoe.GetComponent<AoeAttack>().setDelay(executeTime);
        NorthAoe.transform.localScale = new Vector3(3.0f, 1.0f, 1.5f);
        Vector3 playerPosition = playerObject.transform.position;
        Vector3 bossPosition = bossObject.transform.position;
        Vector3 positionBetween = (bossPosition + playerPosition) / 2;
        GameObject newAoeObject = Instantiate(AoeObject, positionBetween, Coordinate.Instance.North.rotation);
        newAoeObject.GetComponent<AoeAttack>().setDelay(executeTime);
        newAoeObject.transform.LookAt(playerPosition);
    }

}
