using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelSO : ScriptableObject
{
    public List<int> NextLevelRequirements;
}
