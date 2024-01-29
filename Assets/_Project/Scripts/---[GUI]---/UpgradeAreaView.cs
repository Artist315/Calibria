using UnityEngine;

public class UpgradeAreaView : MonoBehaviour
{
    [SerializeField] private UpgradeUI _upgradeUI;

    private Animator _anim;

    private int _animIDUpgradeAvailable;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _animIDUpgradeAvailable = Animator.StringToHash("UpgradeAvailable");
    }

    private void Update()
    {
        if (_upgradeUI.KitchenUpgrade.IsAvailable ||
            _upgradeUI.WaitressUpgrade.IsAvailable ||
            _upgradeUI.BartenderUpgrade.IsAvailable ||
            _upgradeUI.VipUpgrade.IsAvailable)
        {
            _anim.SetBool(_animIDUpgradeAvailable, true);
        }
        else
        {
            _anim.SetBool(_animIDUpgradeAvailable, false);
        }
    }
}
