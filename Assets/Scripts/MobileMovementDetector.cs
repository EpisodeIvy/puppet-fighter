  using UnityEngine;
    using UnityEngine.InputSystem; // Required for accessing sensors

    public class MobileMovementDetector : MonoBehaviour
    {
        private Vector3 lastLinearAcceleration;
        public float movementThreshold = 0.1f; // Adjust this value as needed
        public float sensitivity = 1f;

        public float rotationAngle1 = 90f; // Degrees
        public float rotationAngle2 = 90f; // Degrees
        public Vector3 rotationAxis1 = Vector3.up; // Rotate around the Y-axis
        public Vector3 rotationAxis2 = Vector3.right; // Rotate around the X-axis
        public float skipdelay = 1f;
    public bool skipUpdateForDuration = false; // Boolean to control skipping
    private float skipTimer = 0f;
    public float skipDuration = 1f; // Duration to skip (1 second)
    public float moveDuration = 0.5f;

  
    public void SkipUpdate()
        {
             CancelInvoke(nameof(SkipUpdate));
        }   


        void Update()
        {
            if (IsInvoking(nameof(SkipUpdate)))
            {
                return; // Exit Update early if skipping is active
            }
        if (skipUpdateForDuration)
        {
            // If skipping is active, increment the timer
            skipTimer += Time.deltaTime;

            // Check if the skip duration has passed
            if (skipTimer >= skipDuration)
            {
                skipUpdateForDuration = false; // Stop skipping
                skipTimer = 0f; // Reset the timer
                return;
            }
            else if (skipTimer < moveDuration)
            {
                print("Moving");
            }else{
            // If skipping is active and duration hasn't passed,
            // the rest of the Update function is effectively skipped.
            return; // Exit Update early
            }
        }

            if (LinearAccelerationSensor.current != null)
            {
                // Enable the sensor if it's not already enabled
                if (!LinearAccelerationSensor.current.enabled)
                {
                    InputSystem.EnableDevice(LinearAccelerationSensor.current);
                }

                Vector3 currentLinearAcceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
                // Calculate the magnitude of the linear acceleration
                float accelerationMagnitude = currentLinearAcceleration.magnitude;

                // Only consider acceleration if it exceeds a certain threshold
                if (accelerationMagnitude > movementThreshold)
                {
                    // The phone is likely moving
                    Debug.Log("Phone moving with linear acceleration: " + currentLinearAcceleration);
                    Quaternion rotation = Quaternion.AngleAxis(rotationAngle1, rotationAxis1);
                    currentLinearAcceleration  = rotation * currentLinearAcceleration;
                    rotation = Quaternion.AngleAxis(rotationAngle2, rotationAxis2);
                    currentLinearAcceleration  = rotation * currentLinearAcceleration;
                    // Implement your desired actions based on movement here
                    transform.Translate(currentLinearAcceleration * sensitivity * Time.deltaTime, Space.World);   
                                        //Invoke(nameof(SkipUpdate), skipdelay); 
                                        skipUpdateForDuration = true;

                }
                else
                {
                    // The phone is relatively still
                    // Debug.Log("Phone is relatively still.");
                    //skip update for 1 second


                }

                lastLinearAcceleration = currentLinearAcceleration;
            }
            else
            {
                Debug.LogWarning("LinearAccelerationSensor not available.");
            }
        }
    }