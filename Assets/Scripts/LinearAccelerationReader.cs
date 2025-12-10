using UnityEngine;
using UnityEngine.InputSystem;

public class LinearAccelerationReader : MonoBehaviour
{
    void Start()
    {
        // Ensure the LinearAccelerationSensor is available and enabled
        if (LinearAccelerationSensor.current == null)
        {
            Debug.LogError("LinearAccelerationSensor not found.");
        }
        else
        {
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }
    }   
    
    private void OnEnable()
    {
        // Enable the LinearAccelerationSensor when the script is enabled
        if (LinearAccelerationSensor.current != null)
        {
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }
        else
        {
            Debug.LogWarning("LinearAccelerationSensor not found.");
        }
    }

    private void OnDisable()
    {
        // Disable the LinearAccelerationSensor when the script is disabled
        if (LinearAccelerationSensor.current != null)
        {
            InputSystem.DisableDevice(LinearAccelerationSensor.current);
        }
    }

    void Update()
    {
         InputSystem.EnableDevice(LinearAccelerationSensor.current);
        print("Linear Acceleration Reader enabled: " + LinearAccelerationSensor.current.enabled );
        if (LinearAccelerationSensor.current != null && LinearAccelerationSensor.current.enabled)
        {
            // Read the linear acceleration value
            Vector3 linearAcceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
            Debug.Log($"Linear Acceleration: {linearAcceleration}");

            // You can use this value for game logic, e.g., moving an object
            // transform.Translate(linearAcceleration * Time.deltaTime);
        }
    }
}