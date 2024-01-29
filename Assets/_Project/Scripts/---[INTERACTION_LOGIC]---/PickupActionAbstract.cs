using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class PickupActionAbstract : MonoBehaviour
{
    public Transform PickupTargetPos;
    public List<PickupsEnum> TakablePickups;

    [SerializeField]
    internal AudioManager _audioManager;

    [HideInInspector] public float PickupGrabTimer = 0.5f;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagConstants.Pickup))
        {
            if (other.TryGetComponent(out PickupController pickup))
            {
                if (TakablePickups.Contains(pickup.PickupName))
                {
                    GrabPickup(pickup, other);
                }
            }
        }
        else if (other.CompareTag(TagConstants.PickupGiver))
        {
            if (other.TryGetComponent(out PickupGiver pickupGiver))
            {
                if (TakablePickups.Contains(pickupGiver.PickupSpawner.Pickup.GetComponent<PickupController>().PickupName))
                {
                    if (pickupGiver.PickupSpawner.Pickups.Count > 0 && pickupGiver.PickupSpawner.Pickups[0] != null)
                        GrabPickup(pickupGiver.PickupSpawner.Pickups[0].GetComponent<PickupController>(), other);
                }
            }
        }
    }
    
    protected virtual void GrabPickup(PickupController pickup, Collider pickupColl) { }

    protected Sequence MovePickup(GameObject pickup, Transform pickupTargetPos)
    {
        pickup.transform.SetParent(pickupTargetPos.parent);
        
        Vector3 midPoint = (pickup.transform.localPosition + pickupTargetPos.localPosition) / 2;

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(pickup.transform.DOLocalMoveX(midPoint.x, PickupGrabTimer / 2).SetEase(Ease.Linear));
        sequence.Join(pickup.transform.DOLocalMoveY(midPoint.y + 0.5f, PickupGrabTimer / 2).SetEase(Ease.OutCubic));
        sequence.Join(pickup.transform.DOLocalMoveZ(midPoint.z, PickupGrabTimer / 2).SetEase(Ease.Linear));

        sequence.Append(pickup.transform.DOLocalMoveX(pickupTargetPos.localPosition.x, PickupGrabTimer / 2).SetEase(Ease.Linear));
        sequence.Join(pickup.transform.DOLocalMoveY(pickupTargetPos.localPosition.y, PickupGrabTimer / 2).SetEase(Ease.InCubic));
        sequence.Join(pickup.transform.DOLocalMoveZ(pickupTargetPos.localPosition.z, PickupGrabTimer / 2).SetEase(Ease.Linear));
        
        return sequence;
    }
}
