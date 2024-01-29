using UnityEngine;
using UnityEngine.UI;

internal class ProgressWithNumbers : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    public int MaxValue {  get; private set; }

    private void Awake()
    {
        Enable();
    }

    public void UpdateValue(int _currentValue)
    {
        _slider.value = ((float)_currentValue)/((float)MaxValue);
        _text.text = $"{_currentValue}/{MaxValue}";
    }
    public void SetMaxValue(int _currentValue)
    {
        MaxValue = _currentValue;
    }

    public void Disable()
    {
        _slider.value = 0;
        _slider.gameObject.SetActive(false);
    }

    public void Enable()
    {
        _slider.gameObject.SetActive(true);
    }

    internal void OnMaxLevelReached()
    {
        _slider.value = 1;
        _text.text = $"Max level";
    }
}
