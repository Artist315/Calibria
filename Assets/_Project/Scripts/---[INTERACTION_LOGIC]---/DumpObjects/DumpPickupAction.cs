using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DumpPickupAction : PickupActionAbstract
{
    [SerializeField] private LayerMask ObstacleMask;
    [SerializeField] private int SubtractMoney = 2;

    protected override void GrabPickup(PickupController pickup, Collider pickupColl)
    {
        if (pickup.PickupOwner == PickupOwners.Staff || pickup.PickupOwner == PickupOwners.Player)
        {
            Vector3 dirToTarget = (pickup.transform.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, pickup.transform.position);
            
            if (Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask)) return;
            
            pickup.Pickup(PickupOwners.Sink);

            Sequence sequence = MovePickup(pickup.gameObject, PickupTargetPos);

            if (pickup.PickupName != PickupsEnum.Keg && pickup.PickupName != PickupsEnum.Garbage)
            {
                MoneyManager.Instance.TrySubtractResource(SubtractMoney, out _);
            }

            StartCoroutine(DestroyPickup(pickup, sequence));
        }
    }

    private IEnumerator DestroyPickup(PickupController pickup, Sequence sequence)
    {
        _audioManager.PlayPredefinedAudio();
        yield return sequence.WaitForCompletion();
        pickup.DestroyPickup();
    }
}
