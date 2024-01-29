using UnityEngine;

public class ReputationUI : MonoBehaviour
{
    private ProgressWithNumbers _progressWithNumbers;

    private void Awake()
    {
        _progressWithNumbers = GetComponent<ProgressWithNumbers>();
    }

    internal void UpdateValue(int resource)
    {
        _progressWithNumbers.UpdateValue(resource);
    }

    public void SetMaxValue(int maxValue)
    {
        _progressWithNumbers.SetMaxValue(maxValue);
    }
    
    public void OnMaxLevelReached()
    {
        _progressWithNumbers.OnMaxLevelReached();
    }
}
