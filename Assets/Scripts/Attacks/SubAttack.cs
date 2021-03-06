﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubAttack : IAction {

    /// <summary>
    ///   
    ///           delayBeforeActive        delayBeforeNext
    ///   another------------------->|-----------------> NextSubAttack
    ///                              |--------------------------->|----------> Aoe Destroyed
    ///                           aoeTimer                  linger
    ///                           
    /// </summary>
    
    [Tooltip("Before the aoe start count Timer")]
    public float delayBeforeActive = 1f;

    [Tooltip("Delay Before Next Subattack")]
    public float delayBeforeNext = 2f;

    [Tooltip("Aoe Timer to activate and deal damage")]
    public float aoeTimer = 4f;

    [Tooltip("Linger Time of AoE after activated")]
    public float lingerTime = 0f;

    [Range(5, 25)]
    public int damage = 10;

    // --------------------------------------------------------------------------
    [Header("Transform Things")]
    public CoordinateName coordinateName;

    [Range(-15f, 15f)]
    public float xPos;
    [Range(-15f, 15f)]
    public float zPos;

    public float rotation;

    [Header("Aoe Things")]
    public AoeType aoeType;
    [Header("Square Aoe Size")]
    public float xSize = 4f;
    public float zSize = 4f;

    [Header("Circle Aoe Size")]
    [Range(4f, 30f)]
    public float diameter = 4f;

    // --------------------------------------------------------------------------

    public abstract override void Perform(GameInstanceManager gameInstance);

    public override void MyAwake()
    {

    }

    public override float GetTotalDelay()
    {
        return delayBeforeActive + delayBeforeNext;
    }

    protected void SettingAoeSizeByType(GameObject newAoeObject, AoeType type)
    {
        if (type == AoeType.Square)
        {
            newAoeObject.transform.localScale = new Vector3(xSize, 1, zSize);
        }
        if (type == AoeType.Circle)
        {
            newAoeObject.transform.localScale = new Vector3(diameter, newAoeObject.transform.localScale.y, diameter);
        }
    }

}
