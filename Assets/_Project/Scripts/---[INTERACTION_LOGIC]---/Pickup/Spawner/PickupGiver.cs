using UnityEngine;

public class PickupGiver : MonoBehaviour
{
    [HideInInspector] public PickupSpawner PickupSpawner;
    
    private void Start()
    {
        PickupSpawner = GetComponentInParent<AlcoholPickupSpawner>();
        
        if (PickupSpawner == null)
            PickupSpawner = GetComponentInParent<PickupSpawner>();
    }
}
