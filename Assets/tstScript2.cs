using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tstScript2 : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

    }
}
