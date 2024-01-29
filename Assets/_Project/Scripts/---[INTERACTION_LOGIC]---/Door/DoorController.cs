using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool _canPlayerInteract;

    private Animator _anim;
    private List<Collider> _objectsInTrigger;

    private int _animIDIsClosed, _animIDIsOpened;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _objectsInTrigger = new();

        _animIDIsClosed = Animator.StringToHash("IsClosed");
        _animIDIsOpened = Animator.StringToHash("IsOpened");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagConstants.Client) ||
            other.CompareTag(TagConstants.Player) && _canPlayerInteract ||
            other.CompareTag(TagConstants.Staff))
        {
            _objectsInTrigger.Add(other);

            _anim.SetBool(_animIDIsOpened, true);
            _anim.SetBool(_animIDIsClosed, false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagConstants.Client) ||
            other.CompareTag(TagConstants.Player) && _canPlayerInteract ||
            other.CompareTag(TagConstants.Staff))
        {
            _objectsInTrigger.Remove(other);

            if (_objectsInTrigger.Count == 0)
            {
                _anim.SetBool(_animIDIsClosed, true);
                _anim.SetBool(_animIDIsOpened, false);
            }
        }
    }
}
