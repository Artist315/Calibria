using UnityEngine;

public class AnimationSwitch : MonoBehaviour
{
    private PickupAction _pickupAction;
    private Animator _animator;
    
    private int _animIDIsHoldingTray;
    private int _animIDIsHoldingKeg;

    void Awake()
    {
        _pickupAction = GetComponent<PickupAction>();
        _animator = GetComponentInChildren<Animator>();
        
        AssignAnimationIDs();
    }
    
    private void Update()
    {
        if (_pickupAction.CurrentPickup == PickupsEnum.None)
        {
            _animator.SetBool(_animIDIsHoldingTray, false);
            _animator.SetBool(_animIDIsHoldingKeg, false);
        }
        else if (_pickupAction.CurrentPickup == PickupsEnum.Beer ||
                 _pickupAction.CurrentPickup == PickupsEnum.Pasta ||
                 _pickupAction.CurrentPickup == PickupsEnum.Whiskey ||
                 _pickupAction.CurrentPickup == PickupsEnum.Garbage)
        {
            _animator.SetBool(_animIDIsHoldingTray, true);
            _animator.SetBool(_animIDIsHoldingKeg, false);
        }
        else if (_pickupAction.CurrentPickup == PickupsEnum.Keg)
        {
            _animator.SetBool(_animIDIsHoldingKeg, true);
            _animator.SetBool(_animIDIsHoldingTray, false);
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDIsHoldingTray = Animator.StringToHash("IsHoldingTray");
        _animIDIsHoldingKeg = Animator.StringToHash("IsHoldingKeg");
    }
}
