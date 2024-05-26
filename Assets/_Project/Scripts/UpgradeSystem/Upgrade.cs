using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public CustomizationUpgradeSO CustomizationUpgradeSettings;
    protected MoneyManager MoneyManager;
    protected LevelManager LevelManager;
    public bool IsUpdated { get; private set; } = false;

    private Button Button;
    [SerializeField]
    private Text MoneyCost;
    [SerializeField]
    private Text LevelRequirement;

    [SerializeField]
    private List<GameObject> ObjectModels;
    private UpgradePage upgradePage;
    private void Start()
    {
        Button = GetComponentInChildren<Button>();
        upgradePage = GetComponentInParent<UpgradePage>();
        UpdateText();

        MoneyManager = MoneyManager.Instance;
        LevelManager = LevelManager.Instance;
        ResourcesEvent.ResourceValueUpdated += UpdateButton;
        UpdateButton();
        if (IsUpdated)
        {
            Unsubscribe();
            ActivateObject();
            EventsManager.OnCustomizationUpgraded?.Invoke();
            return;
        }
        else
        {
            HideObject();
        }
        Button.onClick.AddListener(BuyCustomization);
    }


    private void UpdateText()
    {
        MoneyCost.text = CustomizationUpgradeSettings.MoneyCost.ToString();
        if (LevelRequirement != null)
        {
            LevelRequirement.text = CustomizationUpgradeSettings.LevelRequirement.ToString();
        }
    }

    private void Unsubscribe()
    {
        ResourcesEvent.ResourceValueUpdated -= UpdateButton;
    }

    private void UpdateButton()
    {
        IsUpdated = PlayerPrefs.GetInt(CustomizationUpgradeSettings.ObjectName, 0) == 1;
        var isEnoughResources  = MoneyManager.Resource     >= CustomizationUpgradeSettings.MoneyCost;
        var isLevelrequirement = LevelManager.CurrentLevel >= CustomizationUpgradeSettings.LevelRequirement;

        Button.interactable = !IsUpdated && isEnoughResources && isLevelrequirement;
        //Debug.Log($"{CustomizationUpgradeSettings.ObjectName} is{!IsUpdated && isEnoughResources && isLevelrequirement} ");
    }

    private void BuyCustomization()
    {
        PlayerPrefs.SetInt(CustomizationUpgradeSettings.ObjectName, 1);
        if (
        MoneyManager.TrySubtractResource(CustomizationUpgradeSettings.MoneyCost, out int _))
        {
            Unsubscribe();
            upgradePage.CheckIfAllUpgraded();
            UpdateButton();
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        foreach (var objectModel in ObjectModels)
        {
            objectModel.SetActive(true);
        }
    }
    private void HideObject()
    {
        foreach (var objectModel in ObjectModels)
        {
            objectModel.SetActive(false);
        }
    }
}
