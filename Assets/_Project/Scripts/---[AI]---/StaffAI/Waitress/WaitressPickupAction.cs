using UnityEngine;

public class WaitressPickupAction : PickupAction
{
    private WaitressStateManager _stateManager;

    protected override void Start()
    {
        MaxGarbageNumber = 3;
        PickupOwner = PickupOwners.Staff;
        
        _stateManager = GetComponent<WaitressStateManager>();
    }

    protected override void GrabPickup(PickupController pickup, Collider pickupColl)
    {
        if (pickup.PickupName == _stateManager.CurrentOrder)
        {
            base.GrabPickup(pickup, pickupColl);
        }
    }
}
