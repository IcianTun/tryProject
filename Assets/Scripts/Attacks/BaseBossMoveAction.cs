using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBossMoveAction : MonoBehaviour,IAction {

    public CoordinateName coordinateName;

    [Range(-15f, 15f)]
    public float xPos;
    [Range(-15f, 15f)]
    public float zPos;

    public float delaybefore;
    public float timeToMove = 1f;
    public float delayAfter;

    virtual public void Perform(GameInstanceManager gameInstance)
    {
        BossMovementController bossMovementScript = gameInstance.boss.GetComponent<BossMovementController>();
        Vector3 targetPos = gameInstance.transform.position;
        if (coordinateName != CoordinateName.none)
        { 
            targetPos += Coordinate.getCoordinate(coordinateName).transform.position;
        }
        else
            targetPos += new Vector3(xPos, 2, zPos);
        bossMovementScript.MoveToPosition(targetPos,timeToMove, delaybefore);
    }

    virtual public void MyAwake()
    {

    }

    public float GetTotalDelay()
    {
        return delaybefore + timeToMove + delayAfter;
    }


}
