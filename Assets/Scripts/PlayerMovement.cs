using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float rotationSpeed = 90f;
    Rigidbody playerRigidbody;


    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning(Input.GetKey(KeyCode.Q), Input.GetKey(KeyCode.E));
    }

    void Move(float h, float v)
    {
        Vector3 vertical = transform.forward;
        Vector3 horizontal = transform.right;

        Vector3 movement = (vertical * v + horizontal * h).normalized * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        ////playerRigidbody.AddForce(movement - playerRigidbody.velocity, ForceMode.VelocityChange);
    }

    void Turning(bool isTurnLeft, bool isTurnRight)
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
    
}
