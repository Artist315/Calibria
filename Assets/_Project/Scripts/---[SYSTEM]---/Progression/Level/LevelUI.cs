using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void UpdateValue(int value)
    {
        if (value == -1)
        {
            _text.text = "Max";
        }
        else 
        {
            _text.text = (value + 1).ToString();
        }
    }
}