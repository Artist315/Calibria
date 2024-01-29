using System.Collections;
using UnityEngine;

public class ClientOrderReward : MonoBehaviour
{
    [HideInInspector] public int ReputationReward;

    [Header("SadRewardFactor")]
    [HideInInspector] public float SadRewardFactor;

    [Header("AngryRewardFactor")]
    [HideInInspector] public float AngryMoneyRewardFactor;

    private ClientStateManager _stateManager;
    private ReputationManager _reputation;
    private MoneyManager _money;

    void Start()
    {
        _stateManager = GetComponent<ClientStateManager>();

        _reputation = ReputationManager.Instance;
        _money = MoneyManager.Instance;
    }
    
    public void CalculateReward(int moneyReward, MoodStates mood, bool calculateReputation)
    {
        if (mood == MoodStates.Happy)
        {
            if (calculateReputation)
                _reputation.AddResource(ReputationReward);

            _money.AddResource(moneyReward);
        }
        else if (mood == MoodStates.Sad)
        {
            if (calculateReputation)
                _reputation.AddResource(Mathf.RoundToInt(ReputationReward * SadRewardFactor));

            _money.AddResource(Mathf.RoundToInt(moneyReward * SadRewardFactor));
        }
        else if (mood == MoodStates.Angry)
        {
            _money.AddResource(Mathf.RoundToInt(moneyReward * AngryMoneyRewardFactor));
        }
    }
}
