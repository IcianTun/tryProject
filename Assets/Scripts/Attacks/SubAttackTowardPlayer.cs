using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackTowardPlayer : SubAttack
{
    [Header("Specific for Toward Player Attack")]
    public float plusDistance = 2.0f;
    public float minLength = 5.0f;
    //Use only xSize and plusDistance
    
    override public void Perform(GameInstanceManager gameInstanceManager)
    {
        GameObject playerObject = gameInstanceManager.player;
        GameObject bossObject = gameInstanceManager.boss;

        Vector3 playerPosition = playerObject.transform.position;
        Vector3 bossPosition = bossObject.transform.position;
        playerPosition.y = 0;
        bossPosition.y = 0;
        Vector3 positionBetween = (bossPosition + playerPosition) / 2;
        float distance = Vector3.Distance(playerPosition, bossPosition);
        Vector3 vectorTowardPlayer = (new Vector3(playerPosition.x - bossPosition.x, 0, playerPosition.z - bossPosition.z)).normalized;

        GameObject newAoeObject = Instantiate(MyUtility.GetAoePrefabObject(AoeType.Square), positionBetween, Quaternion.identity, gameInstanceManager.transform);
        newAoeObject.transform.LookAt(playerPosition);
        newAoeObject.transform.rotation *= Quaternion.Euler(new Vector3(0, rotation, 0));

        AoeAttack aoeAttackScript = newAoeObject.transform.GetChild(0).GetComponent<AoeAttack>();
        aoeAttackScript.SettingsAoe(aoeTimer, lingerTime,damage);

        if (distance + plusDistance < minLength)
        {
            newAoeObject.transform.position = bossPosition + vectorTowardPlayer * minLength / 2;
            newAoeObject.transform.localScale = new Vector3(xSize, 1, minLength);
        } else
        {
            newAoeObject.transform.position += vectorTowardPlayer * plusDistance / 2; // = bossposition + vectorTowardplayer *( distance + plusdistance)/2
            newAoeObject.transform.localScale = new Vector3(xSize, 1, distance + plusDistance);
        }

        
    }
    
}
