using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private GameObject _moneyPanel;
    [SerializeField] private GameObject _playerCanvas;
    [SerializeField] private Animator _moneyUI;

    private MoneyManager _moneyManager;

    private int _animIDIncome, _animIDExpanse;

    private void Start()
    {
        _moneyManager = GetComponent<MoneyManager>();

        _animIDIncome = Animator.StringToHash("Income");
        _animIDExpanse = Animator.StringToHash("Expanse");

        _moneyManager.OnChangeMoney += SpawnMoneyPanel;
        _moneyManager.OnChangeMoney += MoneyUIAnimate;
    }

    private void OnDisable()
    {
        _moneyManager.OnChangeMoney -= SpawnMoneyPanel;
        _moneyManager.OnChangeMoney -= MoneyUIAnimate;
    }

    private void SpawnMoneyPanel(int amount, bool isAdding)
    {
        GameObject spawnedObject = Instantiate(_moneyPanel, _playerCanvas.transform);

        Animator anim = spawnedObject.GetComponent<Animator>();
        Text text = spawnedObject.GetComponentInChildren<Text>();

        if (isAdding)
        {
            anim.SetTrigger(_animIDIncome);
            text.text = $"+{amount}";
        }
        else
        {
            anim.SetTrigger(_animIDExpanse);
            text.text = $"-{amount}";
        }
    }

    private void MoneyUIAnimate(int amount, bool isAdding)
    {
        if (isAdding)
        {
            _moneyUI.SetTrigger(_animIDIncome);
        }
        else
        {
            _moneyUI.SetTrigger(_animIDExpanse);
        }
    }
}
