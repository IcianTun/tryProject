using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float smoothing = 5f;

    public Vector3 positionOffset;
    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    public bool moving = false;
    // Use this for initialization
    /*
    void Start()
    {
        positionOffset = transform.position - player.position;

        distance = positionOffset.magnitude;
        playerPrevPos = player.transform.position;
    }

    void LateUpdate()
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
    */
    private void Update()
    {
        Vector3 movement = Vector3.zero;
        bool isInput = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.z += 50;
            isInput = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.z -= 50;
            isInput = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.x += 50;
            isInput = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.x -= 50;
            isInput = true;
        }
        if(!moving && isInput)
        MoveToPosition(transform.position + movement,0.35f);
        //transform.position += movement;
    }

    public void MoveToPosition(Vector3 targetPosition, float timeToMove)
    {
        StartCoroutine(_MoveToPosition(targetPosition, timeToMove));
    }

    private IEnumerator _MoveToPosition(Vector3 targetPosition, float timeToMove)
    {
        moving = true;
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, targetPosition, t);
            yield return null;
        }
        moving = false;
        yield return null;
    }
}
