using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationUpgrade : MonoBehaviour
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
    private void Start()
    {
        Button = GetComponentInChildren<Button>();
        MoneyCost.text = CustomizationUpgradeSettings.MoneyCost.ToString();
        if (LevelRequirement != null)
        {
            LevelRequirement.text = CustomizationUpgradeSettings.LevelRequirement.ToString();
        }

        MoneyManager = MoneyManager.Instance;
        LevelManager = LevelManager.Instance;
        MoneyManager.ResourceValueUpdated += UpdateButton;
        UpdateButton();
        if (IsUpdated)
        {
            Unsubscribe();
            ActivateCustomizationObject();
            EventsManager.OnCustomizationUpgraded?.Invoke();
            return;
        }
        Button.onClick.AddListener(BuyCustomization);
    }

    private void Unsubscribe()
    {
        MoneyManager.ResourceValueUpdated -= UpdateButton;
    }

    private void UpdateButton()
    {
        IsUpdated = PlayerPrefs.GetInt(CustomizationUpgradeSettings.ObjectName, 0) == 1;
        var isEnoughResources = CustomizationUpgradeSettings.MoneyCost <= MoneyManager.Resource;
        var isLevelrequirement = CustomizationUpgradeSettings.LevelRequirement <= LevelManager.CurrentLevel;
        Button.interactable = !IsUpdated && isEnoughResources && isLevelrequirement;
    }

    private void BuyCustomization()
    {
        PlayerPrefs.SetInt(CustomizationUpgradeSettings.ObjectName, 1);
        if (
        MoneyManager.TrySubtractResource(CustomizationUpgradeSettings.MoneyCost, out int _))
        {
            Unsubscribe();
            EventsManager.OnCustomizationUpgraded?.Invoke();
            UpdateButton();
            ActivateCustomizationObject();
        }
    }

    private void CreateCustomizationObject()
    {
        foreach (var objectModel in ObjectModels)
        {
            Instantiate(objectModel);
        }
    }
    private void ActivateCustomizationObject()
    {
        foreach (var objectModel in ObjectModels)
        {
            objectModel.SetActive(true);
        }
    }
}
