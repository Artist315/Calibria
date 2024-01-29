using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void UpdateValue(int value)
    {
        _text.text = value.ToString();
    }
}
