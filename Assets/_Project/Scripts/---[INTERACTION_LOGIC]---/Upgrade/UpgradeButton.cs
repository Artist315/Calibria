using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private List<UpgradePage> upgradePages;

    private Animator _anim;
    private int _animIDUpgradeAvailable;
    private MoneyManager moneyManager;
    private LevelManager levelManager;

    public bool IsAvaliable {  get; private set; }

    public bool IsAnythingUpgraded
    {
        get
        {
            return upgradePages?.Any(x => x.Upgrades.Any(upgrade => upgrade.IsUpgraded)) ?? false;
        }
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _animIDUpgradeAvailable = Animator.StringToHash("UpgradeAvailable");

        ResourcesEvent.ResourceValueUpdated += UpdateButton;


    }
    private void Start()
    {
        moneyManager = MoneyManager.Instance;
        levelManager = LevelManager.Instance;
        UpdateButton();
    }
    private void UpdateButton()
    {

        IsAvaliable = upgradePages?.Any(x => x.Upgrades?.Any(upgrade =>
        {
            var IsUpgraded = PlayerPrefs.GetInt(upgrade.CustomizationUpgradeSettings.ObjectName, 0) == 1;
            var isEnoughResources = moneyManager.Resource >= upgrade.CustomizationUpgradeSettings.MoneyCost;
            var isLevelrequirement = levelManager.CurrentLevel >= upgrade.CustomizationUpgradeSettings.LevelRequirement;

            var isAvaliable = !IsUpgraded && isEnoughResources && isLevelrequirement;
            return isAvaliable;
        }) ?? false) ?? false;


        if (IsAvaliable)
        {
            _anim.SetBool(_animIDUpgradeAvailable, true);
        }
        else
        {
            _anim.SetBool(_animIDUpgradeAvailable, false);
        }
    }

        private void OnDestroy()
    {
        ResourcesEvent.ResourceValueUpdated -= UpdateButton;
    }
}
