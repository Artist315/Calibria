using UnityEngine;
using UnityEngine.UI;

public class UltimatumView : MonoBehaviour
{
    [SerializeField] private Slider _moneySlider;
    [SerializeField] private Text _moneyText;

    [SerializeField] private Image _timer;

    private UltimatumManager _ultimatumManager;

    private void Start()
    {
        _ultimatumManager = GetComponent<UltimatumManager>();
    }

    private void Update()
    {
        _moneySlider.value = _ultimatumManager.MoneyCollected/_ultimatumManager.MoneyGoal;
        _moneyText.text = $"{_ultimatumManager.MoneyCollected}/{_ultimatumManager.MoneyGoal}"; 

        _timer.fillAmount = _ultimatumManager.Timer/_ultimatumManager.UltimatumTimer;
    }
}
