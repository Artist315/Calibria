using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public GameObject UpgradeElements;
    public CinemachineVirtualCamera UpgradeView;

    [SerializeField] private UpgradeAction _upgradeAction;
    [SerializeField] private GameObject _noUpgradesText;

    [Header("Kitchen")]
    public Text KitchenLvlRequirement;
    public Text KitchenMoneyCost;

    [Header("VIP")]
    public Text VipZoneLvlRequirement;
    public Text VipZoneMoneyCost;

    [Header("Waitress")]
    public Text WaitressLvlRequirement;
    public Text WaitressMoneyCost;

    [Header("Bartender")]
    public Text BartenderLvlRequirement;
    public Text BartenderMoneyCost;

    [HideInInspector] public VIPZoneUpgrade VipUpgrade;
    [HideInInspector] public KitchenUpgrade KitchenUpgrade;
    [HideInInspector] public WaitressUpgrade WaitressUpgrade;
    [HideInInspector] public BartenderUpgrade BartenderUpgrade;

    private CinemachineBrain _cinemachineBrain;
    //private Animator _anim;

    private int _animIDFadeIn, _animIDFadeOut;
    public bool IsAnimated = false;

    private void Start()
    {
        //_anim = GetComponent<Animator>();
        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        VipUpgrade = GetComponent<VIPZoneUpgrade>();
        KitchenUpgrade = GetComponent<KitchenUpgrade>();
        WaitressUpgrade = GetComponent<WaitressUpgrade>();
        BartenderUpgrade = GetComponent<BartenderUpgrade>();

        _animIDFadeIn = Animator.StringToHash("FadeIn");
        _animIDFadeOut = Animator.StringToHash("FadeOut");

        //KitchenLvlRequirement.text = KitchenUpgrade.LvlRequirement.ToString();
        //KitchenMoneyCost.text = KitchenUpgrade.MoneyCost.ToString();

        //VipZoneLvlRequirement.text = VipUpgrade.LvlRequirement.ToString();
        //VipZoneMoneyCost.text = VipUpgrade.MoneyCost.ToString();

        //WaitressLvlRequirement.text = WaitressUpgrade.LvlRequirement.ToString();
        //WaitressMoneyCost.text = WaitressUpgrade.MoneyCost.ToString();

        //BartenderLvlRequirement.text = BartenderUpgrade.LvlRequirement.ToString();
        //BartenderMoneyCost.text = BartenderUpgrade.MoneyCost.ToString();

    }

    private void Update()
    {
        if (VipUpgrade.IsUpgraded && KitchenUpgrade.IsUpgraded && WaitressUpgrade.IsUpgraded && BartenderUpgrade.IsUpgraded)
        {
            _noUpgradesText.SetActive(true);
        }
    }

    public void Close()
    {
        Debug.Log("Upgrade Ui closed");

        UpgradeView.Priority = -1;
        //_anim.SetTrigger(_animIDFadeOut);
        EventsManager.OnUpgradeClosed?.Invoke();

    }

    public void Open()
    {
        Debug.Log("Upgrade Ui opened");

        UpgradeView.Priority = 1;
        StartCoroutine(WaitCameraBlend());
    }

    IEnumerator WaitCameraBlend()
    {
        yield return new WaitUntil(() => _cinemachineBrain.ActiveBlend != null);
        CinemachineBlend currentBlend = _cinemachineBrain.ActiveBlend;

        if (currentBlend != null)
        {
            yield return new WaitUntil(() => currentBlend.IsComplete);

            if (_upgradeAction.PlayerInTrigger)
            {
                EventsManager.OnUpgradeOpened?.Invoke();
                //_anim.SetTrigger(_animIDFadeIn);
            }
        }
        else
        {
            StartCoroutine(WaitCameraBlend());
        }
    }
}
