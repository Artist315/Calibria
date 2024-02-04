using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomizationUpgrade", order = 6)]
public class CustomizationUpgradeSO : ScriptableObject
{

    [Header("Object1")]
    public int MoneyCost;
    public int LevelRequirement;
    public List<GameObject> ObjectModels;
    public string ObjectName;
}
