using UnityEngine;

public class WaitOrderState : State<ClientStateManager>
{
    public float HappyTimer, SadTimer;
    public bool OrderIsDelivered;

    private PickupOwners _pickupOwner;
    private float _delayTimer;
    
    public override void EnterState(ClientStateManager stateManager)
    {
        stateManager.VisitorAnim.SetBool("IsSitting", true);

        if (stateManager.TargetSitPoint.IsPoker)
        {
            if (stateManager.OrderTypes.Contains(PickupsEnum.Pasta))
            {
                int pastaIndex = stateManager.OrderTypes.IndexOf(PickupsEnum.Pasta);
                stateManager.OrderTypes.Remove(PickupsEnum.Pasta);
                stateManager.OrderTypesDropRates.RemoveAt(pastaIndex);
            }
        }
        
        if (stateManager.Order == PickupsEnum.None)
        {
            stateManager.Order = OrderFactory.RandomOrderItem(stateManager.OrderTypes, stateManager.OrderTypesDropRates);
        }
        stateManager.TargetSitPoint.OrderGarbage = stateManager.Order;

        stateManager.ClientPickup.OnDelivered += OrderDelivered;
        stateManager.ClientPickup.IsAbleToPickUp = true;

        stateManager.MoodState = MoodStates.Happy;
        HappyTimer = stateManager.HappyTime;
        SadTimer = stateManager.SadTime;
        _delayTimer = stateManager.MoodChangeDelay;
    }

    public override void UpdateState(ClientStateManager stateManager)
    {
        if (OrderIsDelivered == true) ExitState(stateManager);
        ManageTimers(stateManager);

        RotationHelper.SmoothLookAtTarget(stateManager.transform, stateManager.TargetSitPoint.transform, 240f);
    }

    public override void ExitState(ClientStateManager stateManager)
    {
        stateManager.ClientPickup.OnDelivered -= OrderDelivered;
        
        stateManager.OrderReward.CalculateReward(stateManager.ClientPickup.Pickup.MoneyReward,
                                                 stateManager.MoodState,
                                                 _pickupOwner == PickupOwners.Player);

        stateManager.MoodState = MoodStates.None;
        stateManager.Order = PickupsEnum.None;
        
        stateManager.SetState(stateManager.EatState);
    }

    public void OrderDelivered(PickupController pickup)
    {
        OrderIsDelivered = true;
        _pickupOwner = pickup.PickupOwner;
    }

    private void ManageTimers(ClientStateManager stateManager)
    {
        _delayTimer -= Time.deltaTime;
        if (_delayTimer >= 0) return;

        if (stateManager.MoodState == MoodStates.Happy)
        {
            HappyTimer -= Time.deltaTime;
            if (HappyTimer <= 0)
            {
                stateManager.MoodState = MoodStates.Sad;
                _delayTimer = stateManager.MoodChangeDelay;
            }
        }
        else if (stateManager.MoodState == MoodStates.Sad)
        {
            SadTimer -= Time.deltaTime;
            if (SadTimer <= 0) stateManager.MoodState = MoodStates.Angry;
        }
    }
}
