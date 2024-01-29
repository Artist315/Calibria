using UnityEngine;

public class ClientSpawnerSettings : MonoBehaviour
{
    [SerializeField] private ClientSpawnersSO _clientSpawnerSO;

    private ClientSpawner _clientSpawner;
    private ClientLevelScale _clientLevelScale;
    
    private void Awake()
    {
        _clientSpawner = GetComponent<ClientSpawner>();
        _clientLevelScale = GetComponent<ClientLevelScale>();

        _clientLevelScale.LvlScale = _clientSpawnerSO.LvlScale;
        _clientLevelScale.MaxClientNumber = _clientSpawnerSO.MaxClientNumber;

        _clientSpawner.SpawnCooldown = _clientSpawnerSO.SpawnCooldown;
    }
}
