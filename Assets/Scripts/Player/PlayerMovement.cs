using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Enable Here")]
    public bool enableManualControl = true;

    [Header("For Testing")]
    public float movementSpeed = 6.5f;
    public float rotationSpeed = 150f;
    Rigidbody playerRigidbody;

    //public GameObject boss;
    //public bool isToRight;
    //public bool isToLeft;
    //public bool isFront;

    //public int aoeCount;

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
        //Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, LayerMask.GetMask("Aoe"));
        //aoeCount = hitColliders.Length;
    }

    //private void Update()
    //{
        //isToRight = IsBossObjectToTheRight();
        //isToLeft = !isToRight;
        //isFront = IsBossObjectToTheFront();
        //Debug.Log(Time.time);
    //}

    public void Move(float v, float h)
    {
        Vector3 vertical = transform.forward;
        Vector3 horizontal = transform.right;

        Vector3 movement = (vertical * v + horizontal * h).normalized * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
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

    //bool IsBossObjectToTheRight()
    //{
    //    Vector3 bossPosition = boss.transform.position;
    //    bossPosition.y = 0;
    //    Vector3 playerPosition = transform.position;
    //    playerPosition.y = 0;

    //    return Vector3.Dot(transform.right, bossPosition - playerPosition) > 0;
    //}

    //bool IsBossObjectToTheFront()
    //{
    //    Vector3 bossPosition = boss.transform.position;
    //    bossPosition.y = 0;
    //    Vector3 playerPosition = transform.position;
    //    playerPosition.y = 0;

    //    return Vector3.Dot(transform.forward, bossPosition - playerPosition) > 0;
    //}
    
}
