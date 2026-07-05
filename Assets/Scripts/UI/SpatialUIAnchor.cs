using UnityEngine;

public class SpatialUIAnchor : MonoBehaviour
{
    public Transform userCamera;
    public float distance = 1.0f;
    public float smoothSpeed = 5.0f;
    public bool followUser = true;

    void LateUpdate()
    {
        if (userCamera == null) userCamera = Camera.main.transform;
        if (userCamera == null || !followUser) return;

        // Position the UI in front of the user
        Vector3 targetPosition = userCamera.position + userCamera.forward * distance;
        
        // Face the user
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - userCamera.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
