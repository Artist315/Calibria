using UnityEngine;

[CreateAssetMenu(fileName = "Client", menuName = "ScriptableObjects/ClientSpawnerSettingsScriptableObject", order = 4)]
public class ClientSpawnersSO : ScriptableObject
{
    public int[] LvlScale;
    public int[] MaxClientNumber;
    public int SpawnCooldown;

}