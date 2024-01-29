using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveAction : MonoBehaviour
{
    private bool _inControl = true;
    private bool _isMotionStarted = false;

    [Tooltip("Move speed of the character in m/s")]
    [HideInInspector] public float MoveSpeed = 10f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 30f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [HideInInspector] public float RotationSmoothTime = 0.12f;

    private PlayerControlsInputs _input;
    private CharacterController _controller;
    private Animator _animator;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _targetRotation;
    private float _animationBlend;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDMotionSpeed;

    void Awake()
    {
        _input = GetComponent<PlayerControlsInputs>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        AssignAnimationIDs();
    }

    void Update()
    {
        if (_inControl)
        {
            Move();
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Move()
    {
        // set target speed based on move speed
        float targetSpeed = MoveSpeed;

        // if there is no input, set the target speed to 0
        if (_input.MoveInput == Vector2.zero) targetSpeed = 0.0f;

        //defining if motion begins or ends
        if (!_isMotionStarted && targetSpeed != 0)
        {
            _isMotionStarted = true;
        }
        else if (_isMotionStarted && targetSpeed == 0)
        {
            _isMotionStarted = false;
        }

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.AnalogMovement ? _input.MoveInput.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDir = new Vector3(_input.MoveInput.x, 0.0f, _input.MoveInput.y).normalized;

        // if there is a move input rotate player when the player is moving
        if (_input.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            //_mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDir = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.SimpleMove(targetDir.normalized * _speed);

        // update animator
        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    public void SetInControl(bool isInControl)
    {
        _inControl = isInControl;
    }
    
    public void SetAnimationBlend(float animBlend)
    {
        _animator.SetFloat(_animIDSpeed, animBlend);
    }
}
