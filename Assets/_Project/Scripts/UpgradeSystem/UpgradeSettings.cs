using UnityEngine;

public class UpgradeSettings : MonoBehaviour
{
    [SerializeField] private UpgradeSO _kitchenUpgradeSettings;
    [SerializeField] private UpgradeSO _vipUpgradeSettings;
    [SerializeField] private UpgradeSO _waitressUpgradeSettings;
    [SerializeField] private UpgradeSO _bartenderUpgradeSettings;

    private KitchenUpgrade _kitchenUpgrade;
    private VIPZoneUpgrade _vipUpgrade;
    private WaitressUpgrade _waitressUpgrade;
    private BartenderUpgrade _bartenderUpgrade;

    void Awake()
    {
        _kitchenUpgrade = GetComponent<KitchenUpgrade>();
        _vipUpgrade = GetComponent<VIPZoneUpgrade>();
        _waitressUpgrade = GetComponent<WaitressUpgrade>();
        _bartenderUpgrade = GetComponent<BartenderUpgrade>();

        _kitchenUpgrade.MoneyCost = _kitchenUpgradeSettings.MoneyCost;
        _kitchenUpgrade.LvlRequirement = _kitchenUpgradeSettings.LvlRequirement;

        _vipUpgrade.MoneyCost = _vipUpgradeSettings.MoneyCost;
        _vipUpgrade.LvlRequirement = _vipUpgradeSettings.LvlRequirement;

        _waitressUpgrade.MoneyCost = _waitressUpgradeSettings.MoneyCost;
        _waitressUpgrade.LvlRequirement = _waitressUpgradeSettings.LvlRequirement;

        _bartenderUpgrade.MoneyCost = _bartenderUpgradeSettings.MoneyCost;
        _bartenderUpgrade.LvlRequirement = _bartenderUpgradeSettings.LvlRequirement;
    }
}
