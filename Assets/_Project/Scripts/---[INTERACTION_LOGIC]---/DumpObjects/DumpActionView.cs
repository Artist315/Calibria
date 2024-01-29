using UnityEngine;

public class DumpActionView : MonoBehaviour
{
    [SerializeField] private PickupAction _playerPickupAction;
    
    private DumpPickupAction _dumpPickupAction;
    private Animator _anim;

    private int _animIDStart, _animIDEnd; 
    
    private void Start()
    {
        _dumpPickupAction = GetComponent<DumpPickupAction>();
        _anim = GetComponentInChildren<Animator>();

        _animIDStart = Animator.StringToHash("Start");
        _animIDEnd = Animator.StringToHash("End");
    }

    private void Update()
    {
        if (_playerPickupAction.PickedUp && _dumpPickupAction.TakablePickups.Contains(_playerPickupAction.CurrentPickup))
        {
            _anim.SetBool(_animIDStart, true);
            _anim.SetBool(_animIDEnd, false);
        }
        else
        {
            _anim.SetBool(_animIDStart, false);
            _anim.SetBool(_animIDEnd, true);
        }
    }
}
