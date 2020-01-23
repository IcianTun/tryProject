using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack1 : MonoBehaviour,ISubAttack {

    public GameObject AoEObject;

    public void Perform ()
    {
        Debug.Log("Hello from subattack1");
        Instantiate(AoEObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation);
        Instantiate(AoEObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation);
        Instantiate(AoEObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation);
        Instantiate(AoEObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation);
    }
}
