using UnityEngine;

public class GyroscopeRotation : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public bool useLocalRotation = false; // Set to true to use local rotation
    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Input.gyro.attitude provides a Quaternion representing the device's rotation
            // You may need to adjust the axis mapping depending on your game's world space
            //Quaternion newRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w);
            transform.rotation = gyro.attitude;
            SwapAxis();
        }
    }

void SwapAxis()
    {
        Vector3 currentRotation;

        if (useLocalRotation)
        {
            currentRotation = transform.localEulerAngles;
        }
        else
        {
            currentRotation = transform.eulerAngles;
        }

        // Store the original Y and Z values
        float originalY = currentRotation.y;
        float originalZ = currentRotation.z;

        // Assign the original Z value to Y and the original Y value to Z
        currentRotation.y = originalZ;
        currentRotation.z = originalY;

        if (useLocalRotation)
        {
            transform.localEulerAngles = currentRotation;
        }
        else
        {
            transform.eulerAngles = currentRotation;
        }

        Debug.Log("Y and Z rotation values swapped for " + gameObject.name);
    }
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }
}