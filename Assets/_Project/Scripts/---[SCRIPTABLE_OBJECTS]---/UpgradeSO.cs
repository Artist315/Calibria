using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeSettingsScriptableObject", order = 5)]
public class UpgradeSO : ScriptableObject
{
    public int MoneyCost;
    public int LvlRequirement;

}