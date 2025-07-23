using UnityEngine;

public class SpawnCylinder : MonoBehaviour
{
    public GameObject cylinderPrefab;   // Assign this in the Inspector
    public Transform playerHead;        // The XR Camera or main camera

    public float distanceInFront = 2f;

    public void Spawn()
    {
        if (cylinderPrefab == null || playerHead == null)
        {
            Debug.LogWarning("Prefab or Player Head not assigned.");
            return;
        }

        Vector3 spawnPosition = playerHead.position + playerHead.forward * distanceInFront;
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(cylinderPrefab, spawnPosition, spawnRotation);
    }
}
