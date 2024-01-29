using UnityEngine;
using UnityEngine.UI;

public class SpawnerZoneView : MonoBehaviour
{
    [SerializeField] private PickupAction _pickupAction;
    [SerializeField] private Image PickupIcon;

    private PickupSpawner _pickupSpawner;
    private AlcoholPickupSpawner _alcoholSpawner;

    private Animator _anim;

    private int _animIDCanBeTaken;

    private void Start()
    {
        _alcoholSpawner = GetComponentInParent<AlcoholPickupSpawner>();

        if (_alcoholSpawner == null)
            _pickupSpawner = GetComponentInParent<PickupSpawner>();
        else
        {
            _pickupSpawner = _alcoholSpawner;
        }

        _anim = GetComponent<Animator>();

        _animIDCanBeTaken = Animator.StringToHash("CanBeTaken");
    }

    private void Update()
    {
        ControlAnim();
        TimerView();
    }

    private void ControlAnim()
    {
        if (_pickupSpawner.Pickups.Count == 0 ||
            _pickupSpawner.Pickups is null ||
            _pickupAction.PickedUp ||
            _alcoholSpawner != null && _alcoholSpawner.CurrentAlcoholCapacity <= 0 && _pickupSpawner.Pickups.Count == 0)
        {
            _anim.SetBool(_animIDCanBeTaken, false);
        }
        else
        {
            _anim.SetBool(_animIDCanBeTaken, true);
        }
    }

    private void TimerView()
    {
        if (_alcoholSpawner != null)
        {
            if (_pickupSpawner.Pickups.Count <= 0 && _alcoholSpawner.CurrentAlcoholCapacity > 0)
            {
                PickupIcon.fillAmount = _pickupSpawner.Cooldown / _pickupSpawner.SpawnCooldown;
            }
            else
            {
                PickupIcon.fillAmount = 1;
            }
        }
        else
        {
            if (_pickupSpawner.Pickups.Count <= 0)
            {
                PickupIcon.fillAmount = _pickupSpawner.Cooldown / _pickupSpawner.SpawnCooldown;
            }
            else
            {
                PickupIcon.fillAmount = 1;
            }
        }
    }
}
