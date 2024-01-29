using UnityEngine;

public class RotationHelper : MonoBehaviour
{
    public static void SmoothLookAtTarget(Transform transform, Transform target, float rotationSpeed)
    {
        Quaternion targetRotation = target.rotation;
        
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationStep);
    }
}