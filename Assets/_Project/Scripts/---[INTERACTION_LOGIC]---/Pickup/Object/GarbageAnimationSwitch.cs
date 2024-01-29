using UnityEngine;

public class GarbageAnimationSwitch : MonoBehaviour
{
    public Animator Anim;
    
    private PickupController _pickupController;
    
    private int _animIDStart;
    private int _animIDEnd;

    private void Start()
    {
        _pickupController = GetComponent<PickupController>();

        _animIDStart = Animator.StringToHash("Start");
        _animIDEnd = Animator.StringToHash("End");
        
        Anim.SetBool(_animIDEnd, false);
        Anim.SetBool(_animIDStart, true);

        _pickupController.OnPickup += (x) =>
        {
            Anim.SetBool(_animIDEnd, true);
            Anim.SetBool(_animIDStart, false);
        };
    }
}
