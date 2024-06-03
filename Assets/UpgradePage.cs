using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradePage : MonoBehaviour
{
    public GameObject AllUpgradedPage;
    public List<Upgrade> Upgrades {  get; private set; }
    public bool IsAllUpgraded { get; private set; } = false;

    private void Awake()
    {
        Upgrades = GetComponentsInChildren<Upgrade>().ToList();
    }

    private void Start()
    {
        AllUpgradedPage.SetActive(false);

        IsAllUpgraded = Upgrades.All(x => x.IsUpgraded);
        HideElements();

        EventsManager.OnUpgradeClosed += HideElements;

    }

    public void HideElements()
    {
        if (!IsAllUpgraded)
        {
            Upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(false);
            });
        }
        else
        {
            Upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(false);
            });

            AllUpgradedPage.SetActive(false);
        }
    }
    public void ShowElements()
    {
        if (!IsAllUpgraded)
        {
            Upgrades.ForEach(x =>
            {
                if (!x.IsUpgraded)
                {
                    x.gameObject.SetActive(true);
                }
                else
                {
                    x.gameObject.SetActive(false);
                }    
            });
        }
        else
        {
            AllUpgradedPage.SetActive(true );
        }
    }

    public void CheckIfAllUpgraded()
    {
        if (Upgrades.All(x => x.IsUpgraded))
        {
            Debug.Log("All was upgraded");
            Upgrades.ForEach(x =>
            {
                x.gameObject.SetActive(false);
            });
            IsAllUpgraded = true;
            AllUpgradedPage.SetActive(true);
            //EventsManager.OnCustomizationUpgraded -= CheckIfAllUpgraded;
        }
    }
}
