using UnityEngine;

public class BartenderAnimationSwitch : AIAnimationSwitch
{
    private BartenderPickupAction _pickupAction;

    private int _animIDSpeed;
    private int _animIDIsCarryingTray, _animIDIsCarryingKeg;

    protected override void Awake()
    {
        _pickupAction = GetComponent<BartenderPickupAction>();
        base.Awake();
    }

    protected override void Update()
    {
        SetAnimationBlend(_animIDSpeed);

        if (_pickupAction.CurrentPickup == PickupsEnum.None)
        {
            Anim.SetBool(_animIDIsCarryingTray, false);
            Anim.SetBool(_animIDIsCarryingKeg, false);
        }
        else if (_pickupAction.CurrentPickup == PickupsEnum.Keg)
        {
            Anim.SetBool(_animIDIsCarryingKeg, true);
            Anim.SetBool(_animIDIsCarryingTray, false);
        }
        else if (_pickupAction.CurrentPickup != PickupsEnum.Keg && _pickupAction.CurrentPickup != PickupsEnum.None)
        {
            Anim.SetBool(_animIDIsCarryingTray, true);
            Anim.SetBool(_animIDIsCarryingKeg, false);
        }
    }

    protected override void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDIsCarryingTray = Animator.StringToHash("IsCarryingTray");
        _animIDIsCarryingKeg = Animator.StringToHash("IsCarryingKeg");
    }
}
