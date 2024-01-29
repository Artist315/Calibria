using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettingsScriptableObject", order = 1)]
public class PlayerSettingsSO : ScriptableObject
{
    [Header("MoveSettings")]
    
    [Range(0, 100)]
    public float Speed;
    
    [Range(0.0f, 0.3f)]
    public float RotationSpeed;
}