using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private MoneyManager      moneyManager;
    private ReputationManager reputationManager;

    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        reputationManager = ReputationManager.Instance;

    }

    public void AddMoney()
    {
        moneyManager.AddResource(50);
    }
    public void AddExp()
    {
        reputationManager.AddResource(25);
    }
}
