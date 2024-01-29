using UnityEngine;

[RequireComponent(typeof(MoveAction))]
[RequireComponent(typeof(PlayerControlsInputs))]
public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    private PlayerSettingsSO PlayerSettingsSO;
    
    private MoveAction _moveAction;
    
    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();

        SetMoveSettings();
    }

    private void SetMoveSettings()
    {   
        _moveAction.MoveSpeed = PlayerSettingsSO.Speed;
        _moveAction.RotationSmoothTime = PlayerSettingsSO.RotationSpeed;
    }
}