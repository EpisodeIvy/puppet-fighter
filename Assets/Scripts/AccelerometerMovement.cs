using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class AccelerometerMovement : MonoBehaviour
{
    public float speed = .010f; // Multiplier for movement speed
    public float XZsensitivity = .2f; // Tilt sensitivity
    public float Ysensitivity = 4f; // Tilt sensitivity
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Read accelerometer input
        Vector3 tilt = Input.acceleration;

        // Adjust sensitivity and map to XZ plane (commonly used for ground movement)
        float moveX = tilt.x * XZsensitivity;
        float moveY = -tilt.y * Ysensitivity;
        float moveZ = tilt.z * XZsensitivity; // Note: Input.acceleration.y maps to the device's portrait Y axis

        //print the values
        print("movex: " + moveX);
        print("movey: " + moveY);
        print("movez: " + moveZ);   

        // Apply force to move the object's transform directly
        transform.Translate(new Vector3(moveX, moveY, moveZ) * speed * Time.deltaTime); 
    }
}