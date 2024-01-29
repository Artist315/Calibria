using UnityEngine;
using UnityEngine.AI;

public class ClientSettings : MonoBehaviour
{
    [SerializeField] private ClientSettingsSO _clientSettings;

    private NavMeshAgent _agent;
    private ClientStateManager _clientManager;
    private ClientOrderReward _orderReward;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _clientManager = GetComponent<ClientStateManager>();
        _orderReward = GetComponent<ClientOrderReward>();

        _agent.speed = _clientSettings.Speed;
        _agent.angularSpeed = _clientSettings.RotationSpeed;
        
        _clientManager.OrderTypes.Clear();
        _clientManager.OrderTypesDropRates.Clear();
        _clientManager.OrderTypes.AddRange(_clientSettings.OrderTypes);
        _clientManager.OrderTypesDropRates.AddRange(_clientSettings.OrderTypesDropRates);
        
        _clientManager.MinEatTime = _clientSettings.MinEatTime;
        _clientManager.MaxEatTime = _clientSettings.MaxEatTime;

        _clientManager.HappyTime = _clientSettings.HappyTime;
        _clientManager.SadTime = _clientSettings.SadTime;
        _clientManager.MoodChangeDelay = _clientSettings.MoodChangeDelay;

        _orderReward.ReputationReward = _clientSettings.ReputationReward;
        _orderReward.SadRewardFactor = _clientSettings.SadRewardFactor;
        _orderReward.AngryMoneyRewardFactor = _clientSettings.AngryMoneyRewardFactor;
    }
}
