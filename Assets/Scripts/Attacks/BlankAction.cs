using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankAction : MonoBehaviour, IAction
{

    public float GetTotalDelay()
    {
        return 0;
    }

    public void MyAwake()
    {

    }

    virtual public void Perform(GameInstanceManager gameInstance)
    {

    }

}
