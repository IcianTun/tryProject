using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAttack1 : MonoBehaviour,ISubAttack {

    public GameObject AoeObject;

    public void Perform ()
    {
        Instantiate(AoeObject, Coordinate.Instance.North.position, Coordinate.Instance.North.rotation);
        Instantiate(AoeObject, Coordinate.Instance.East.position, Coordinate.Instance.East.rotation);
        Instantiate(AoeObject, Coordinate.Instance.South.position, Coordinate.Instance.South.rotation);
        Instantiate(AoeObject, Coordinate.Instance.West.position, Coordinate.Instance.West.rotation);
    }
}
