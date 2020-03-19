using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubAttack : MonoBehaviour {

    public GameObject AoeObject;
    public string presettedCoordinate;

    public float executeTime;
    public string description;

    [Header("Square")]
    public float xSize;
    public float zSize;

    [Header("Circle")]
    public float diameter;

    [Header("Donut")]
    public float minRadius;
    public float maxRadius;

    
    virtual public void Perform(GameInstanceManager gameInstance)
    {

    }

    virtual public void myAwake()
    {

    }

    public float GetExecuteTime()
    {
        return executeTime;
    }

    public void setCoordinate(string newCoordinate)
    {

    }

}
