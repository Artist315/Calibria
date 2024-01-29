using UnityEngine;

public class BartenderPickupAction : PickupAction
{
    private BartenderStateManager _stateManager;

    protected override void Start()
    {
        PickupOwner = PickupOwners.Staff;
        
        _stateManager = GetComponent<BartenderStateManager>();
    }
    
    protected override void GrabPickup(PickupController pickup, Collider pickupColl)
    {
        if (pickup.PickupName == _stateManager.CurrentOrder)
        {
            base.GrabPickup(pickup, pickupColl);
        }
    }
}
