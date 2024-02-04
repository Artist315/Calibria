using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradePage : MonoBehaviour
{
    public GameObject AllUpgradedPage;
    private List<CustomizationUpgrade> upgrades;
    private bool _isAllUpgraded = false;

    private void Start()
    {
        AllUpgradedPage.SetActive(false);
        upgrades = GetComponentsInChildren<CustomizationUpgrade>().ToList();
        HideElements();
        EventsManager.OnCustomizationUpgraded += CheckIfAllUpgraded;
        EventsManager.OnUpgradeClosed += HideElements;

    }

    public void HideElements()
    {
        if (!_isAllUpgraded)
        {
            upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(false);
            });
        }
        else
        {
            AllUpgradedPage.SetActive(false);
        }
    }
    public void ShowElements()
    {
        if (!_isAllUpgraded)
        {
            upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(true);
            });
        }
        else
        {
            AllUpgradedPage.SetActive(true );
        }
    }

    public void CheckIfAllUpgraded()
    {
        if (upgrades.All(x => x.IsUpdated))
        {
            upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(false);
            });
            _isAllUpgraded = true;
            AllUpgradedPage.SetActive(true);
            EventsManager.OnCustomizationUpgraded -= CheckIfAllUpgraded;
        }
    }
}
