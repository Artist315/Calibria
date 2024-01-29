using UnityEngine;

public class ClientPickupAction : PickupActionAbstract
{
    public delegate void OrderDeliveredCallback(PickupController pickup);
    public event OrderDeliveredCallback OnDelivered;

    [HideInInspector] public OrderPickupController Pickup;

    [HideInInspector] public bool IsAbleToPickUp = false;

    private ClientStateManager _clientManager;

    private void Start()
    {
        _clientManager = GetComponent<ClientStateManager>();
    }

    protected override void GrabPickup(PickupController pickup, Collider pickupColl)
    {
        if (IsAbleToPickUp &&
            (pickup.PickupOwner == PickupOwners.Staff || pickup.PickupOwner == PickupOwners.Player) &&
            pickup.PickupName == _clientManager.Order)
        {
            _audioManager.PlayPredefinedAudio();
            IsAbleToPickUp = false;

            // if (pickup is OrderPickupController orderPickup)
            //     Pickup = orderPickup;
            // else
            Pickup = (OrderPickupController)pickup;

            OnDelivered?.Invoke(pickup);

            pickup.Pickup(PickupOwners.Other);

            MovePickup(pickup.gameObject, PickupTargetPos);
        }
    }
}
