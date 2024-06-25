using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class UltimatumManager : MonoBehaviour
{
    public float UltimatumTimer;
    public int MoneyGoal;
    [HideInInspector] public float MoneyCollected;
    [HideInInspector] public float Timer;

    [SerializeField] private UltimatumComicsUI _ultimatumComics;
    [SerializeField] private FinalComicsUI _finalComics;

    [SerializeField] private GameObject _playerConfirmationUI;
    [SerializeField] private GameObject _ultimatumUI;
    [SerializeField] private GameObject _upgradeArea;
    [SerializeField] private GameObject _bossArrow;

    private LevelManager _levelManager;
    private MoneyManager _moneyManager;
    private PlayableDirector _playableDirector;

    private bool _ultimatumIsStarted, _ultimatumIsFinished = false;
    private bool _missionComplete;
    private int _moneyBeforeUltimatum;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _moneyManager = MoneyManager.Instance;

        _playableDirector = GetComponent<PlayableDirector>();

        Timer = UltimatumTimer;

        StartCoroutine(CheckOnUltimatum());
    }

    void OnTriggerEnter(Collider other)
    {
        if (_levelManager.IsOnMaxLevel && !_ultimatumIsStarted && other.CompareTag(TagConstants.Player))
        {
            _playerConfirmationUI.SetActive(true);
            _bossArrow.SetActive(false);
            CursorScript.ShowCoursor();

            Time.timeScale = 0;
        }
        else if (_ultimatumIsFinished && other.CompareTag(TagConstants.Player))
        {
            FinishUltimatum();
        }
    }

    void Update()
    {
        if (!_ultimatumIsStarted || _ultimatumIsFinished) return;

        Timer -= Time.deltaTime;
        MoneyCollected = _moneyManager.Resource - _moneyBeforeUltimatum;

        if (Timer <= 0)
        {
            _missionComplete = false;
            FinishUltimatumCutscene();
        }
        else if (MoneyCollected >= MoneyGoal)
        {
            _missionComplete = true;
            FinishUltimatumCutscene();
        }
    }

    private IEnumerator CheckOnUltimatum()
    {
        yield return new WaitUntil(() => _levelManager.IsOnMaxLevel);
        _playableDirector.Play();
    }

    private IEnumerator CheckOnComicsEnd()
    {
        yield return new WaitUntil(() => !_ultimatumComics.gameObject.activeSelf);
        StartUltimatum();
    }

    private void StartUltimatum()
    {
        _moneyBeforeUltimatum = _moneyManager.Resource;
        _upgradeArea.SetActive(false);
        _ultimatumUI.SetActive(true);
        _ultimatumIsStarted = true;
    }

    public void StartUltimatumComics()
    {
        _ultimatumComics.gameObject.SetActive(true);
    }
    
    private void FinishUltimatum()
    {
        _finalComics.IsGoodFinal = _missionComplete;
        _finalComics.gameObject.SetActive(true);
    }

    private void FinishUltimatumCutscene()
    {
        _ultimatumIsFinished = true;
        _ultimatumUI.SetActive(false);

        if (_missionComplete)
        {
            _playableDirector.Play();
        }
        else
        {
            FinishUltimatum();
        }
    }

    public void PlayerDecline()
    {
        Time.timeScale = 1;
        _playerConfirmationUI.SetActive(false);
    }

    public void PlayerAccept()
    {
        _playerConfirmationUI.SetActive(false);
        StartUltimatumComics();

        StartCoroutine(CheckOnComicsEnd());
    }
}
