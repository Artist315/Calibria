using UnityEngine;

public class PickupSpawnPos : MonoBehaviour
{
    [HideInInspector] public Vector3 Position;
    [HideInInspector] public bool IsAvailable;
    [HideInInspector] public bool IsFueled;
    
    protected virtual void Awake()
    {
        IsFueled = false;
        IsAvailable = true;
        Position = transform.position;
    }
}