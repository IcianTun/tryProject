using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttackAtCoordinate : SubAttack {


    override public void Perform (GameInstanceManager gameInstanceManager)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0,rotation,0));
        Vector3 targetPosition = gameInstanceManager.transform.position;
        if (coordinateName != CoordinateName.none)
        {
            targetPosition += Coordinate.getCoordinate(coordinateName).position;
        }
        else
        {
            targetPosition += new Vector3(xPos, 0, zPos);
        }
        GameObject newAoeObject = Instantiate(MyUtility.GetAoePrefabObject(aoeType), targetPosition, targetRotation, gameInstanceManager.transform);
        AoeAttack aoeAttackScript = newAoeObject.transform.GetChild(0).GetComponent<AoeAttack>();
        aoeAttackScript.SettingsAoe(aoeTimer, lingerTime,damage);
        SettingAoeSizeByType(newAoeObject,aoeType);
    }


}
