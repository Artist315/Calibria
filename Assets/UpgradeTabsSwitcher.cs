using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabsSwitcher : MonoBehaviour
{
    public List<GameObject> Tabs;
    public Button FirstTab;

    private List<Button> TabSelectors;

    private void Start()
    {
        TabSelectors = GetComponentsInChildren<Button>().ToList();
        Close();
    }

    private void Open()
    {
        TabSelectors.ForEach(tab => { tab.gameObject.SetActive(true); });
        EventsManager.OnUpgradeOpened -= Open;
        EventsManager.OnUpgradeClosed += Close;
        FirstTab.onClick.Invoke();
    }

    private void Close()
    {
        TabSelectors.ForEach(tab => { tab.gameObject.SetActive(false); });
        EventsManager.OnUpgradeOpened += Open;
        EventsManager.OnUpgradeClosed -= Close;
    }

    private void OnDestroy()
    {
        EventsManager.OnUpgradeOpened -= Open;
        EventsManager.OnUpgradeClosed -= Close;
    }
}
