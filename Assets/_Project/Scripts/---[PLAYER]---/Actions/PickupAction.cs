using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PickupAction : PickupActionAbstract
{
    public delegate void PickupCallback(PickupsEnum pickup);
    public event PickupCallback OnPickup;
    
    public Transform PickupFollowTarget;
    
    [HideInInspector] public PickupsEnum CurrentPickup;
    [HideInInspector] public bool PickedUp = false;
    [HideInInspector] public int CurrentGarbageNumber;
    [HideInInspector] public int MaxGarbageNumber;
    
    [SerializeField] private Transform _kegPickupTargetPos;
    
    protected Transform Pickup;
    protected PickupOwners PickupOwner;

    protected bool SequenceEnded = false;

    private List<GameObject> _garbageColl = new();
    private Sequence _sequence;

    private void Awake()
    {
        CurrentPickup = PickupsEnum.None;
    }

    protected virtual void Start()
    {
        MaxGarbageNumber = 3;
        PickupOwner = PickupOwners.Player;
    }

    private void LateUpdate()
    {
        if (SequenceEnded)
        {
            if (CurrentPickup == PickupsEnum.Garbage)
            {
                foreach (var garbage in _garbageColl)
                {
                    garbage.transform.position = Vector3.MoveTowards(garbage.transform.position, PickupFollowTarget.position, 0.02f);
                }
            }
            else if (Pickup != null)
            {
                Pickup.transform.position = Vector3.MoveTowards(Pickup.transform.position, PickupFollowTarget.position, 0.02f);
            }
        }
    }

    protected override void GrabPickup(PickupController pickup, Collider coll)
    {
        if ((!PickedUp && pickup.PickupOwner == PickupOwners.Nobody) ||
            (CurrentPickup == PickupsEnum.Garbage && CurrentGarbageNumber < MaxGarbageNumber &&
             pickup.PickupName == PickupsEnum.Garbage && pickup.PickedUp == false))
        {
            PickedUp = true;
            OnPickup?.Invoke(pickup.PickupName);

            if (pickup.PickupName == PickupsEnum.Garbage)
            {
                CurrentGarbageNumber++;
            }

            Collider pickupColl = pickup.GetComponent<Collider>();
            pickupColl.enabled = false;

            CurrentPickup = pickup.PickupName;
            Pickup = pickup.transform;

            pickup.Pickup(PickupOwner);
            pickup.OnPickup += CheckPickupState;

            if (pickup.PickupName == PickupsEnum.Keg)
                _sequence = MovePickup(pickup.gameObject, _kegPickupTargetPos);
            else
                _sequence = MovePickup(pickup.gameObject, PickupTargetPos);

            StartCoroutine(EnablePickupColl(pickup, pickupColl, _sequence));
        }
    }

    protected IEnumerator EnablePickupColl(PickupController pickup, Collider pickupColl, Sequence sequence)
    {
        yield return sequence.WaitForCompletion();
        pickupColl.enabled = true;
        SequenceEnded = true;

        if (pickup.PickupName == PickupsEnum.Garbage)
            _garbageColl.Add(pickup.gameObject);
    }

    protected void CheckPickupState(GameObject pickup)
    {
        if (pickup.GetComponent<PickupController>().PickupName == PickupsEnum.Garbage)
        {
            _garbageColl.Remove(pickup);
            CurrentGarbageNumber--;

            if (CurrentGarbageNumber <= 0)
            {
                PickedUp = false;
                CurrentPickup = PickupsEnum.None;
                SequenceEnded = false;
                Pickup = null;
            }
        }
        else if (pickup.GetComponent<PickupController>().PickupOwner != PickupOwners.Staff ||
                 pickup.GetComponent<PickupController>().PickupOwner != PickupOwners.Player)
        {
            PickedUp = false;
            CurrentPickup = PickupsEnum.None;
            SequenceEnded = false;
            Pickup = null;
        }
    }
}