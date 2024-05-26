using System.Collections;
using UnityEngine;

public class VIPZoneUpgrade : UpgradeAbstract
{
    public GameObject VipZone;
    public GameObject VipClientsSpawner;
    public GameObject WhiskeySpawner;

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
            VipZone.SetActive(true);
            WhiskeySpawner.SetActive(true);
            VipClientsSpawner.SetActive(true);
            PlayerPrefs.SetInt(PlayerPrefsConstants.VIPZoneUpgrade, 1);

            ResourcesEvent.ResourceValueUpdated -= ButtonUpdate;
            MoneyManager.TrySubtractResource(MoneyCost, out var resource);
            
            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
    }

    public override void LoadUpgrade()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.VIPZoneUpgrade, 0) == 1)
        {
            VipZone.SetActive(true);
            WhiskeySpawner.SetActive(true);
            VipClientsSpawner.SetActive(true);

            ChangeButtonToDone();
            
            IsAvailable = false;
            IsUpgraded = true;
        }
        else
        {
            VipZone.SetActive(false);
            VipClientsSpawner.SetActive(false);
            WhiskeySpawner.SetActive(false);
            
            ButtonUpdate();
        };
    }
    
    protected override void ChangeButtonToDone()
    {
        base.ChangeButtonToDone();
        _upgradeUI.VipZoneLvlRequirement.transform.parent.transform.parent.gameObject.SetActive(false);
        _upgradeUI.VipZoneMoneyCost.transform.parent.gameObject.SetActive(false);

    }

    protected override void ButtonIsNotAvailable()
    {
        base.ButtonIsNotAvailable();

        if (ReputationManager.LevelManager.CurrentLevel + 1 < LvlRequirement)
        {
            _upgradeUI.VipZoneMoneyCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _upgradeUI.VipZoneMoneyCost.transform.parent.gameObject.SetActive(true);
            _upgradeUI.VipZoneLvlRequirement.transform.parent.gameObject.SetActive(false);
        }
    }

    protected override void ButtonIsAvailable()
    {
        base.ButtonIsAvailable();
        _upgradeUI.VipZoneMoneyCost.transform.parent.gameObject.SetActive(true);
        _upgradeUI.VipZoneLvlRequirement.transform.parent.gameObject.SetActive(false);
    }
}

