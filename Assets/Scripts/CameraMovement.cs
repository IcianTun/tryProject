using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float smoothing = 5f;

    Vector3 positionOffset;

    // Use this for initialization
    void Start()
    {
        positionOffset = transform.position - player.position;
        
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = player.position + positionOffset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothing * Time.deltaTime);
    }
}
