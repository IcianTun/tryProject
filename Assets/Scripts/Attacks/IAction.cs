using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction : MonoBehaviour {
	abstract public void Perform(GameInstanceManager gameInstance);
    abstract public float GetTotalDelay();
    abstract public void MyAwake();
}
