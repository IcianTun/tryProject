using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float rotationSpeed = 90f;
    Rigidbody playerRigidbody;
    public int count = 0;
    List<Collider> others = new List<Collider>();
    public float h;
    public float v;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning(Input.GetKey(KeyCode.Q), Input.GetKey(KeyCode.E));
    }

    private void Update()
    {
        for (int i = others.Count -1; i >= 0 ; i--){
            if (others[i] == null)
            {
                count -= 1;
                others.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Aoe")
        {
            count += 1;
            others.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Aoe")
        {
            count -= 1;
            others.Remove(other);
        }
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
