using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 200f;
    public float steeringSensitivity = 0.02f;
    public float maxSteeringAngle = 25f;
    public float grip = 0.9f;
    public float downforce = 10f;

    private float screenWidth;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        Steer();
    }

    void FixedUpdate()
    {
        Accelerate();
        ApplyGrip();
        ApplyDownforce();
    }

    void Accelerate()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
            }
        }
    }

    void Steer()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.touchCount > 0 ?
                (Vector3)Input.GetTouch(0).position :
                Input.mousePosition;

            float steeringInput = (touchPosition.x - screenWidth / 2) / (screenWidth / 2);
            steeringInput = Mathf.Clamp(steeringInput, -1f, 1f);

            float steeringAngle = steeringInput * maxSteeringAngle;

            float speedFactor = rb.velocity.magnitude / maxSpeed;
            steeringAngle *= Mathf.Lerp(1f, 0.5f, speedFactor);

            Vector3 rotation = Vector3.up * steeringAngle * steeringSensitivity * Time.deltaTime;
            transform.Rotate(rotation);
        }
    }

    void ApplyGrip()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(rb.velocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * grip;
    }

    void ApplyDownforce()
    {
        rb.AddForce(-transform.up * downforce * rb.velocity.magnitude);
    }
}
