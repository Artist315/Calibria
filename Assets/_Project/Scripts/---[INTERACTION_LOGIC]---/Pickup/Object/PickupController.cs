using UnityEngine;

public class PickupController : MonoBehaviour
{
    public delegate void PickupCallback(GameObject pickup);
    public event PickupCallback OnPickup;

    [SerializeField]
    private AudioManager _audioManager;

    public PickupOwners PickupOwner { get; private set; }
    public PickupsEnum PickupName;
    
    [HideInInspector] public bool PickedUp = false;
    
    void Awake()
    {
        PickupOwner = PickupOwners.Nobody;
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }
    }

    public void Pickup(PickupOwners owner)
    {
        if (_audioManager && owner != PickupOwners.Sink)
        {
            _audioManager.PlayPredefinedAudio();
        }
        PickedUp = true;
        PickupOwner = owner;
        OnPickup?.Invoke(gameObject);
    }

    public void DestroyPickup()
    {
        OnPickup = null;
        Destroy(gameObject);
    }
}
