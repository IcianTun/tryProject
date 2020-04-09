using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveTowardPlayer : BaseBossMoveAction {

    [Header("Specific for Move Toward Player ")]
    public float distanceOffset = -1f;
    
    override public void Perform(GameInstanceManager gameInstance)
    {
        BossMovementController bossMovementScript = gameInstance.boss.GetComponent<BossMovementController>();
        Vector3 targetPos;

        Vector3 bossPosition = gameInstance.boss.transform.position;
        Vector3 playerPosition = gameInstance.player.transform.position;
        playerPosition.y = 0;
        bossPosition.y = 0;
        float distance = Vector3.Distance(playerPosition, bossPosition);
        Vector3 vectorTowardPlayer = (new Vector3(playerPosition.x - bossPosition.x, 0, playerPosition.z - bossPosition.z)).normalized;
        targetPos = gameInstance.boss.transform.position + vectorTowardPlayer * (distance + distanceOffset);
        bossMovementScript.MoveToPosition(targetPos, timeToMove, delaybefore);
    }
}
