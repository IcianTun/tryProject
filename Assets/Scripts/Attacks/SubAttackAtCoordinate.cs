using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackAtCoordinate : SubAttack {
    
    override public void Perform (GameInstanceManager gameInstanceManager)
    {
        Transform coordinate = Coordinate.Instance.getCoordinate(presettedCoordinate);
        Vector3 targetPosition = gameInstanceManager.transform.position + coordinate.position;
        Quaternion targetRotation = gameInstanceManager.transform.rotation * coordinate.rotation;

        Instantiate(AoeObject, targetPosition, targetRotation).GetComponent<AoeAttack>().setDelay(executeTime);
    }

}
