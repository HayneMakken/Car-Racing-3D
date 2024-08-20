using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 200f;
    public float steeringSpeed = 1f;  
    public float maxSteeringAngle = 30f;

    private float screenWidth;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            Accelerate();
            Steer();
        }
    }

    void Accelerate()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }
    }

    void Steer()
    {
        Vector3 touchPosition;

        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
        }
        else
        {
            touchPosition = Input.mousePosition;
        }

        float steeringInput = (touchPosition.x - screenWidth / 2) / (screenWidth / 2);

        steeringInput = Mathf.Clamp(steeringInput, -1f, 1f);

    
        float steeringAngle = steeringInput * maxSteeringAngle;
        Vector3 rotation = Vector3.up * steeringAngle * steeringSpeed * Time.deltaTime;
        transform.Rotate(rotation);
    }

}
