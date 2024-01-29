using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsInputs : MonoBehaviour
{
    [field: SerializeField] public Vector2 MoveInput { get; private set; }

    [Header("Movement Settings")]
    public bool AnalogMovement;

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
