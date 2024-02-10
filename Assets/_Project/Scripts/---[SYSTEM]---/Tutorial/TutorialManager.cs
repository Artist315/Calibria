using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PickupAction _playerPickupAction;
    [SerializeField] private UpgradeUI _upgrades;
    [SerializeField] private GameObject _clientSpawner;

    [Header("Beginning Tutorial")]
    [SerializeField] private PlayableDirector _kegSpawnerScene;
    [SerializeField] private PlayableDirector _kegeratorScene;
    [SerializeField] private PlayableDirector _visitorScene;
    [SerializeField] private PlayableDirector _garbageScene;
    [SerializeField] private PlayableDirector _sinkScene;
    [SerializeField] private PlayableDirector _finalTutorialScene;
    [SerializeField] private ClientPickupAction _beginningTutorialClient;

    [Header("Upgrades Tutorial")]
    [SerializeField] private PlayableDirector _pastaScene;
    [SerializeField] private PlayableDirector _pastaVisitorScene;
    [SerializeField] private ClientPickupAction _pastaTutorialClient;
    [SerializeField] private Animator _pastaClientArrow;

    [SerializeField] private PlayableDirector _whiskeyScene;
    [SerializeField] private PlayableDirector _whiskeyVisitorScene;
    [SerializeField] private ClientPickupAction _whiskeyTutorialClient;
    [SerializeField] private Animator _whiskeyClientArrow;

    private int _animIDClose;

    private void Start()
    {
        _animIDClose = Animator.StringToHash("Close");
        MoneyManager moneyInstance = MoneyManager.Instance;
        ReputationManager reputationInstance = ReputationManager.Instance;
        
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.TutorialComplete, 0) == 0)
        {
            moneyInstance.Resource = 0;
            moneyInstance.MoneyUI.UpdateValue(moneyInstance.Resource);
            
            reputationInstance.Resource = 0;
            reputationInstance.ReputationUI.UpdateValue(reputationInstance.Resource);

            StartKegScene();
        }
        else
        {
            _clientSpawner.SetActive(true);
        }

        StartCoroutine(StartPastaScene());
        StartCoroutine(StartWhiskeyScene());
    }

    #region Beginning Tutorial
    private void StartKegScene()
    {
        StartCoroutine(PlayTimeline(_kegSpawnerScene));
        
        _playerPickupAction.OnPickup += StartKegeratorScene;
    }

    private void StartKegeratorScene(PickupsEnum pickup)
    {
        if (pickup == PickupsEnum.Keg)
        {
            StartCoroutine(PlayTimeline(_kegeratorScene));
            _playerPickupAction.OnPickup -= StartKegeratorScene;

            StartCoroutine(StartVisitorScene());
        }
    }

    private IEnumerator StartVisitorScene()
    {
        yield return new WaitUntil(() => !_playerPickupAction.PickedUp);
        StartCoroutine(PlayTimeline(_visitorScene));

        StartCoroutine(StartGarbageScene());
    }

    private IEnumerator StartGarbageScene()
    {
        yield return new WaitUntil(() => MoneyManager.Instance.Resource > 0);
        StartCoroutine(PlayTimeline(_garbageScene));

        StartCoroutine(StartSinkScene());
    }

    private IEnumerator StartSinkScene()
    {
        yield return new WaitUntil(() => _playerPickupAction.CurrentPickup == PickupsEnum.Garbage);
        StartCoroutine(PlayTimeline(_sinkScene));

        StartCoroutine(FinishMainTutorial());
    }

    private IEnumerator FinishMainTutorial()
    {
        yield return new WaitUntil(() => !_playerPickupAction.PickedUp);
        StartCoroutine(PlayTimeline(_finalTutorialScene));

        PlayerPrefs.SetInt(PlayerPrefsConstants.TutorialComplete, 1);
    }
    #endregion

    #region Upgrades Tutorial
    private IEnumerator StartPastaScene()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (_upgrades.KitchenUpgrade.IsUpgraded == false)
        {
            yield return new WaitUntil(() => _upgrades.KitchenUpgrade.IsUpgraded);
            StartCoroutine(PlayTimeline(_pastaScene));

            yield return new WaitUntil(() => _playerPickupAction.CurrentPickup == PickupsEnum.Pasta);
            StartCoroutine(StartPastaVisitorScene());
        }
    }

    private IEnumerator StartPastaVisitorScene()
    {
        yield return new WaitUntil(() => _playerPickupAction.CurrentPickup == PickupsEnum.Pasta);
        StartCoroutine(PlayTimeline(_pastaVisitorScene));

        StartCoroutine(EndPastaScene());
    }

    private IEnumerator EndPastaScene()
    {
        yield return new WaitUntil(() => _pastaTutorialClient.Pickup != null);
        yield return new WaitUntil(() => _pastaTutorialClient.Pickup.PickupName == PickupsEnum.Pasta);
        _pastaClientArrow.SetTrigger(_animIDClose);
    }
    
    private IEnumerator StartWhiskeyScene()
    {
        yield return new WaitForSeconds(0.1f);

        if (_upgrades.VipUpgrade.IsUpgraded == false)
        {
            yield return new WaitUntil(() => _upgrades.VipUpgrade.IsUpgraded);
            StartCoroutine(PlayTimeline(_whiskeyScene));

            yield return new WaitUntil(() => _playerPickupAction.CurrentPickup == PickupsEnum.Whiskey);
            StartCoroutine(StartWhiskeyVisitorScene());
        }
    }

    private IEnumerator StartWhiskeyVisitorScene()
    {
        yield return new WaitUntil(() => _playerPickupAction.CurrentPickup == PickupsEnum.Whiskey);
        StartCoroutine(PlayTimeline(_whiskeyVisitorScene));

        StartCoroutine(EndWhiskeyScene());
    }

    private IEnumerator EndWhiskeyScene()
    {
        yield return new WaitUntil(() => _whiskeyTutorialClient.Pickup != null);
        yield return new WaitUntil(() => _whiskeyTutorialClient.Pickup.PickupName == PickupsEnum.Whiskey);
        _whiskeyClientArrow.SetTrigger(_animIDClose);
    }
    #endregion

    private IEnumerator PlayTimeline(PlayableDirector timeline)
    {
        yield return new WaitForSeconds(0.1f);
        timeline.Play();
    }
}