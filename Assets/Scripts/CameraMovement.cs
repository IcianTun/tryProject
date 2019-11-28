using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float smoothing = 5f;

    private Vector3 positionOffset;
    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    // Use this for initialization
    void Start()
    {
        positionOffset = transform.position - player.position;

        distance = positionOffset.magnitude;
        playerPrevPos = player.transform.position;
    }

    void FixedUpdate()
    {
        ////Debug.Log(player.rotation.eulerAngles.y - transform.rotation.eulerAngles.y);
        ////Vector3 targetCamPos = player.position + positionOffset;
        //////transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //////transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothing * Time.deltaTime);
        ////transform.RotateAround(player.position, Vector3.up, player.rotation.eulerAngles.y - transform.rotation.eulerAngles.y);
        playerMoveDir = (player.transform.position - playerPrevPos).normalized;
        if (playerMoveDir != Vector3.zero) { 
            //playerMoveDir.normalize();
            transform.position = player.transform.position - playerMoveDir * distance;

            transform.LookAt(player.transform.position);

            playerPrevPos = player.transform.position;
        }

    }
}
