using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackAtPlayer : SubAttack
{

    override public void Perform(GameInstanceManager gameInstanceManager)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        Vector3 targetPosition = gameInstanceManager.player.transform.position;
        targetPosition.y = 0;

        GameObject newAoeObject = Instantiate(MyUtility.GetAoePrefabObject(aoeType), targetPosition, targetRotation, gameInstanceManager.transform);
        AoeAttack aoeAttackScript = newAoeObject.transform.GetChild(0).GetComponent<AoeAttack>();
        aoeAttackScript.SettingsAoe(aoeTimer, lingerTime,damage);
        SettingAoeSizeByType(newAoeObject, aoeType);
    }


}
