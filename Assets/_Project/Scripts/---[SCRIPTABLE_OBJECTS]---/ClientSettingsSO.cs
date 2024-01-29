using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MafiaSettingsScriptableObject", order = 2)]
public class ClientSettingsSO : ScriptableObject
{
    [Header("MoveSettings")]
    [Range(0, 100)] public float Speed;
    [Range(0, 360)] public float RotationSpeed;
    
    [Header("OrderSettings")]
    public List<PickupsEnum> OrderTypes;
    public List<float> OrderTypesDropRates;
    
    [Header("RewardSettings")]
    public int ReputationReward;
    public float SadRewardFactor;
    public float AngryMoneyRewardFactor;

    [Header("WaitOrderStateSettings")]
    public float HappyTime;
    public float SadTime;
    public float MoodChangeDelay;

    [Header("EatStateSettings")]
    public int MinEatTime;
    public int MaxEatTime;
}