using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementController : MonoBehaviour {

    public Transform gameInstanceTransform;

    public void Start()
    {
        if(!gameInstanceTransform)
        gameInstanceTransform = GetComponent<BossAttackController>().gameInstanceManager.transform;
    }


    public void MoveToPosition(Vector3 targetPosition, float timeToMove, float delay)
    {
        StartCoroutine(_MoveToPosition(targetPosition, timeToMove, delay));
    }


    private IEnumerator _MoveToPosition(Vector3 targetPosition, float timeToMove, float delay)
    {
        yield return new WaitForSeconds(delay);
        float GItransformX = gameInstanceTransform.position.x;
        float GItransformZ = gameInstanceTransform.position.z;
        targetPosition.x = Mathf.Clamp(targetPosition.x, GItransformX - 15f, GItransformX + 15f);
        targetPosition.y = 2;
        targetPosition.z = Mathf.Clamp(targetPosition.z, GItransformZ - 15f, GItransformZ + 15f);

        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, targetPosition, t);
            yield return null;
        }
    }

    public void Stop()
    {
        StopAllCoroutines();

    }
}
