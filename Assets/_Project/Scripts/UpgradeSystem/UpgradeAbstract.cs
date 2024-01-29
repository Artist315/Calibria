using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeAbstract : MonoBehaviour
{
    [HideInInspector] public int MoneyCost;
    [HideInInspector] public int LvlRequirement;
    
    public bool IsUpgraded { get; protected set; }
    public bool IsAvailable { get; protected set; } = false;
    
    protected MoneyManager MoneyManager;
    protected ReputationManager ReputationManager;
    
    [SerializeField] private Button _button;
    
    protected virtual void Start()
    {
        MoneyManager = MoneyManager.Instance;
        ReputationManager = ReputationManager.Instance;

        MoneyManager.ResourceValueUpdated += ButtonUpdate;

        LoadUpgrade();
    }

    public abstract void LoadUpgrade();

    public abstract void ApplyUpgrade();

    protected void ButtonUpdate()
    {
        if (IsUpgraded) return;

        IsAvailable = MoneyCost <= MoneyManager.Resource &&
                       ReputationManager.LevelManager.CurrentLevel + 1 >= LvlRequirement;

        if (IsAvailable)
        {
            ButtonIsAvailable();
        }
        else
        {
            ButtonIsNotAvailable();
        }
    }

    protected virtual void ChangeButtonToDone()
    {
        _button.interactable = false;
    }

    protected virtual void ButtonIsAvailable()
    {
        _button.interactable = true;
    }

    protected virtual void ButtonIsNotAvailable()
    {
        _button.interactable = false;
    }
}
