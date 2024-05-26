using UnityEngine;

public class BartenderUpgrade : UpgradeAbstract
{
    public GameObject Bartender;

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
            Bartender.SetActive(true);
            PlayerPrefs.SetInt(PlayerPrefsConstants.BartenderUpgrade, 1);

            ResourcesEvent.ResourceValueUpdated -= ButtonUpdate;
            MoneyManager.TrySubtractResource(MoneyCost, out var resource);
            
            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
    }

    public override void LoadUpgrade()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.BartenderUpgrade, 0) == 1)
        {
            Bartender.SetActive(true);
            
            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
        else
        {
            Bartender.SetActive(false);
            ButtonUpdate();
        };
    }

    protected override void ChangeButtonToDone()
    {
        base.ChangeButtonToDone();
        _upgradeUI.BartenderLvlRequirement.transform.parent.transform.parent.gameObject.SetActive(false);
        _upgradeUI.BartenderMoneyCost.transform.parent.gameObject.SetActive(false);
    }

    protected override void ButtonIsNotAvailable()
    {
        base.ButtonIsNotAvailable();

        if (ReputationManager.LevelManager.CurrentLevel + 1 < LvlRequirement)
        {
            _upgradeUI.BartenderMoneyCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _upgradeUI.BartenderMoneyCost.transform.parent.gameObject.SetActive(true);
            _upgradeUI.BartenderLvlRequirement.transform.parent.gameObject.SetActive(false);
        }
    }

    protected override void ButtonIsAvailable()
    {
        base.ButtonIsAvailable();
        _upgradeUI.BartenderMoneyCost.transform.parent.gameObject.SetActive(true);
        _upgradeUI.BartenderLvlRequirement.transform.parent.gameObject.SetActive(false);
    }
}
