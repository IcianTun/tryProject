using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Enable Here")]
    public bool enableManualControl = true;

    [Header("For Testing")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 90f;
    Rigidbody playerRigidbody;

    public GameObject boss;
    public bool isToRight;
    public bool isToLeft;
    public bool isFront;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (enableManualControl) {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");
            Move(v, h);
            Turning(Input.GetKey(KeyCode.Q), Input.GetKey(KeyCode.E));
        }
    }

    private void Update()
    {
        isToRight = IsBossObjectToTheRight();
        isToLeft = !isToRight;
        isFront = IsBossObjectToTheFront();
    }

    public void Move(float v, float h)
    {
        Vector3 vertical = transform.forward;
        Vector3 horizontal = transform.right;

        Vector3 movement = (vertical * v + horizontal * h).normalized * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        ////playerRigidbody.AddForce(movement - playerRigidbody.velocity, ForceMode.VelocityChange);
    }

    public void Turning(bool isTurnLeft, bool isTurnRight)
    {
        if (isTurnRight&&!isTurnLeft)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
        }
        else if (isTurnLeft&&!isTurnRight)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
        }
    }

    bool IsBossObjectToTheRight()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;

        return Vector3.Dot(transform.right, bossPosition - playerPosition) > 0;
    }

    bool IsBossObjectToTheFront()
    {
        Vector3 bossPosition = boss.transform.position;
        bossPosition.y = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.y = 0;

        return Vector3.Dot(transform.forward, bossPosition - playerPosition) > 0;
    }
}
