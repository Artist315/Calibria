using UnityEngine;
using UnityEngine.AI;

public class WaitressAnimationSwitch : AIAnimationSwitch
{
    private WaitressPickupAction _pickupAction;

    private int _animIDSpeed;
    private int _animIDIsCarrying;

    protected override void Awake()
    {
        _pickupAction = GetComponent<WaitressPickupAction>();
        base.Awake();
    }
    
    protected override void Update()
    {
        SetAnimationBlend(_animIDSpeed);
        
        if (_pickupAction.CurrentPickup != PickupsEnum.None)
        {
            Anim.SetBool(_animIDIsCarrying, true);
        }
        else
        {
            Anim.SetBool(_animIDIsCarrying, false);
        }
    }

    protected override void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDIsCarrying = Animator.StringToHash("IsCarrying");
    }
}
