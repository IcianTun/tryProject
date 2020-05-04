using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankAction : IAction
{

    public override float GetTotalDelay()
    {
        return 0;
    }

    public override void MyAwake()
    {

    }

    public override void Perform(GameInstanceManager gameInstance)
    {

    }

}
