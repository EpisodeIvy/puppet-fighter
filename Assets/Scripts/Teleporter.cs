using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportOutPosition; // Assign the transform of the "fall-out" box's desired exit point in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (teleportOutPosition == null)
        {
            Debug.LogError("Teleport Out Position is not assigned.");
            return;
        }   
        // Check if the entering object is the one intended for teleportation (e.g., by tag)
        //if (other.CompareTag("TeleportableObject")) // Ensure your teleporting object has this tag
        {
            print("Teleporting object: " + other.name);
            // Teleport the object to the specified exit position
            other.transform.position = teleportOutPosition.position;

            // Optionally, reset velocity if the object had momentum before teleporting
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}