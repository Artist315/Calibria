using UnityEngine;

public class KitchenUpgrade : UpgradeAbstract
{
    public GameObject KitchenPrefab;

    [SerializeField] private ClientSpawner _clientSpawner;

    private UpgradeUI _upgradeUI;

    protected override void Start()
    {
        _upgradeUI = GetComponent<UpgradeUI>();
        base.Start();
    }
    
    public override void ApplyUpgrade()
    {
        if (IsAvailable && !IsUpgraded)
        {
            KitchenPrefab.SetActive(true);
            _clientSpawner.FindSitPoints();
            PlayerPrefs.SetInt(PlayerPrefsConstants.KitchenUpgrade, 1);
            
            
            MoneyManager.ResourceValueUpdated -= ButtonUpdate;
            MoneyManager.TrySubtractResource(MoneyCost, out var resource);
            
            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
    }

    public override void LoadUpgrade()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.KitchenUpgrade, 0) == 1)
        {
            KitchenPrefab.SetActive(true);
            _clientSpawner.FindSitPoints();
            
            ChangeButtonToDone();

            IsAvailable = false;
            IsUpgraded = true;
        }
        else
        {
            KitchenPrefab.SetActive(false);
            ButtonUpdate();
        };
    }

    protected override void ChangeButtonToDone()
    {
        base.ChangeButtonToDone();
        _upgradeUI.KitchenLvlRequirement.transform.parent.transform.parent.gameObject.SetActive(false);
        _upgradeUI.KitchenMoneyCost.transform.parent.gameObject.SetActive(false);
    }

    protected override void ButtonIsNotAvailable()
    {
        base.ButtonIsNotAvailable();

        if (ReputationManager.LevelManager.CurrentLevel + 1 < LvlRequirement)
        {
            _upgradeUI.KitchenMoneyCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _upgradeUI.KitchenMoneyCost.transform.parent.gameObject.SetActive(true);
            _upgradeUI.KitchenLvlRequirement.transform.parent.gameObject.SetActive(false);
        }
    }

    protected override void ButtonIsAvailable()
    {
        base.ButtonIsAvailable();
        _upgradeUI.KitchenMoneyCost.transform.parent.gameObject.SetActive(true);
        _upgradeUI.KitchenLvlRequirement.transform.parent.gameObject.SetActive(false);
    }
}
