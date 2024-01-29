using System.Collections;
using DG.Tweening;
using UnityEngine;

public class KegeratorPickupAction : PickupActionAbstract
{
    public int KegFillAmount;

    [SerializeField] private AlcoholPickupSpawner _beerPickupSpawner;
    
    protected override void GrabPickup(PickupController pickup, Collider pickupColl)
    {
        if (pickup.PickupOwner == PickupOwners.Staff || pickup.PickupOwner == PickupOwners.Player)
        {
            if (_audioManager != null)
            {
                _audioManager.PlayPredefinedAudio();
            }

            pickup.Pickup(PickupOwners.Other);

            Sequence sequence = MovePickup(pickup.gameObject, PickupTargetPos);
            
            StartCoroutine(UseKeg(pickup, sequence));
        }
    }

    private IEnumerator UseKeg(PickupController pickup, Sequence sequence)
    {
        yield return sequence.WaitForPosition(PickupGrabTimer);
        
        _beerPickupSpawner.FillKegerator(KegFillAmount);
        
        yield return sequence.WaitForCompletion();
        pickup.DestroyPickup();
    }
}
