using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    public Transform gameInstanceTransform;
    public DetectorData data;
    public int aoeTouchingCount = 0;
    List<Collider> aoeTouching = new List<Collider>();

    public bool isOutOfBoundary = false;

    public DetectorData GetData()
    {
        DetectorData result = new DetectorData
        {
            transformData = transform,
            aoeTouchingCount = aoeTouchingCount,
            isOutOfBoundary = isOutOfBoundary
        };
        return result;
    }

    void Update()
    {

        if(Mathf.Abs(transform.position.x - gameInstanceTransform.position.x) >= 15 || 
            (Mathf.Abs(transform.position.z - gameInstanceTransform.position.z) >= 15) )
        {
            isOutOfBoundary = true;
        }
        else
        {
            isOutOfBoundary = false;
        }


        for (int i = aoeTouching.Count - 1; i >= 0; i--)
        {
            if (aoeTouching[i] == null)
            {
                aoeTouchingCount -= 1;
                aoeTouching.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Aoe")
        {
            aoeTouchingCount += 1;
            aoeTouching.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Aoe")
        {
            aoeTouchingCount -= 1;
            aoeTouching.Remove(other);
        }
    }

}
