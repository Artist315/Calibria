using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private Image _happyMoodIcon, _sadMoodIcon, _orderIcon;
    [SerializeField] private Sprite _beerIcon, _pastaIcon, _whiskeyIcon;
    [SerializeField] private Animator _anim;

    private ClientStateManager _clientManager;

    private int _startIDAnim, _endIDAnim;

    private void Start()
    {
        _clientManager = GetComponent<ClientStateManager>();

        _startIDAnim = Animator.StringToHash("Start");
        _endIDAnim = Animator.StringToHash("End");
    }

    private void Update()
    {
        if (_clientManager.MoodState == MoodStates.None)
        {
            if (_clientManager.WaitOrderState.OrderIsDelivered)
            {
                _anim.SetBool(_endIDAnim, true);
                _anim.SetBool(_startIDAnim, false);
            }
        }
        else if (_clientManager.MoodState == MoodStates.Happy)
        {
            SetOrderIcon();
            _anim.SetBool(_endIDAnim, false);
            _anim.SetBool(_startIDAnim, true);

            _happyMoodIcon.fillAmount = _clientManager.WaitOrderState.HappyTimer / _clientManager.HappyTime;
        }
        else if (_clientManager.MoodState == MoodStates.Sad)
        {
            _sadMoodIcon.fillAmount = _clientManager.WaitOrderState.SadTimer / _clientManager.SadTime;
        }
    }

    private void SetOrderIcon()
    {
        if (_clientManager.Order == PickupsEnum.Beer)
        {
            _orderIcon.sprite = _beerIcon;
        }
        else if (_clientManager.Order == PickupsEnum.Pasta)
        {
            _orderIcon.sprite = _pastaIcon;
        }
        else if (_clientManager.Order == PickupsEnum.Whiskey)
        {
            _orderIcon.sprite = _whiskeyIcon;
        }
    }
}
