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
    public bool IsUpgraded { get; private set; } = false;
    public bool CanBeUpgraded { get; private set; } = false;

    private Button Button;
    [SerializeField]
    private Text MoneyCost;
    [SerializeField]
    private Text LevelRequirement;

    [SerializeField]
    private List<GameObject> ObjectModels;
    private UpgradePage upgradePage;

    private void Awake()
    {
        IsUpgraded = PlayerPrefs.GetInt(CustomizationUpgradeSettings.ObjectName, 0) == 1;

        Button = GetComponentInChildren<Button>();
        upgradePage = GetComponentInParent<UpgradePage>();

        UpdateText();
        HideObject();


        MoneyManager = FindObjectOfType<MoneyManager>();
        LevelManager = FindObjectOfType<LevelManager>();

        UpdateButton();

        ResourcesEvent.ResourceValueUpdated += UpdateButton;

        if (IsUpgraded)
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
    private void Start()
    {
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
        IsUpgraded = PlayerPrefs.GetInt(CustomizationUpgradeSettings.ObjectName, 0) == 1;
        var isEnoughResources  = MoneyManager.Resource     >= CustomizationUpgradeSettings.MoneyCost;
        var isLevelrequirement = LevelManager.CurrentLevel >= CustomizationUpgradeSettings.LevelRequirement;
        CanBeUpgraded = !IsUpgraded && isEnoughResources && isLevelrequirement;
        Button.interactable = CanBeUpgraded;
        //Debug.Log($"{CustomizationUpgradeSettings.ObjectName} is{!IsUpgraded && isEnoughResources && isLevelrequirement} ");
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
            if (objectModel.TryGetComponent<IActivation>(out var t))
            {
                t.Activate();
            }
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
