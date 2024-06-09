using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    [SerializeField] private bool _destroyOnPress, _mouseHint;
    [SerializeField] private PlayerControlsInputs _playerInputs;

    private Animator _anim;
    private int _animIDClose;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _animIDClose = Animator.StringToHash("Close");

        if (_playerInputs is null)
        {
            _destroyOnPress = false;
        }
    }

    void Update()
    {
        if (!_destroyOnPress) return;
        
        if (_mouseHint)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _anim.SetTrigger(_animIDClose);
            }
        }
        else
        {
            if (_playerInputs.MoveInput.magnitude > 0)
            {
                _anim.SetTrigger(_animIDClose);
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Destroy()
    {
        _anim.SetTrigger(_animIDClose);
    }
}
