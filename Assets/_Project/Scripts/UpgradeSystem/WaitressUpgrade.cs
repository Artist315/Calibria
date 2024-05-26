using UnityEngine;

public class WaitressUpgrade : UpgradeAbstract
{
    public GameObject Waitress;

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
            Waitress.SetActive(true);
            PlayerPrefs.SetInt(PlayerPrefsConstants.WaitressUpgrade, 1);

            ResourcesEvent.ResourceValueUpdated -= ButtonUpdate;
            MoneyManager.TrySubtractResource(MoneyCost, out var resource);
            
            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
    }

    public override void LoadUpgrade()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.WaitressUpgrade, 0) == 1)
        {
            Waitress.SetActive(true);

            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
        else
        {
            Waitress.SetActive(false);
            ButtonUpdate();
        };
    }

    protected override void ChangeButtonToDone()
    {
        base.ChangeButtonToDone();
        _upgradeUI.WaitressLvlRequirement.transform.parent.transform.parent.gameObject.SetActive(false);
        _upgradeUI.WaitressMoneyCost.transform.parent.gameObject.SetActive(false);
    }

    protected override void ButtonIsNotAvailable()
    {
        base.ButtonIsNotAvailable();

        if (ReputationManager.LevelManager.CurrentLevel + 1 < LvlRequirement)
        {
            _upgradeUI.WaitressMoneyCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _upgradeUI.WaitressMoneyCost.transform.parent.gameObject.SetActive(true);
            _upgradeUI.WaitressLvlRequirement.transform.parent.gameObject.SetActive(false);
        }
    }

    protected override void ButtonIsAvailable()
    {
        base.ButtonIsAvailable();
        _upgradeUI.WaitressMoneyCost.transform.parent.gameObject.SetActive(true);
        _upgradeUI.WaitressLvlRequirement.transform.parent.gameObject.SetActive(false);
    }
}
