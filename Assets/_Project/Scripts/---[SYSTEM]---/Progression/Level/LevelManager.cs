using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelSO levelSO;
    [SerializeField] private LevelUI _levelUI;

    private int _currentLevel;

    public bool IsOnMaxLevel
    {
        get
        {
            return bool.Parse(PlayerPrefs.GetString(PlayerPrefsConstants.MaxLevel, "false"));
        }
        set
        {
            PlayerPrefs.SetString(PlayerPrefsConstants.MaxLevel, value.ToString());
        }
    }

    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
        private set
        {
            _currentLevel = value;
            UpdateLevel();
            _levelUI.UpdateValue(CurrentLevel);
        }
    }
    internal int? GetNextLevelRequirement()
    {
        if (levelSO.NextLevelRequirements.Count > CurrentLevel + 1)
        {
            return levelSO.NextLevelRequirements[CurrentLevel];
        }
        else return null;
    }

    void Awake()
    {
        Instance = this;
        GetCurrentLevel();
    }

    private void Start()
    {
        _levelUI.UpdateValue(CurrentLevel);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt(PlayerPrefsConstants.PlayerLevel, 0);
    }

    public void GetCurrentLevel()
    {
        CurrentLevel = PlayerPrefs.GetInt(PlayerPrefsConstants.PlayerLevel, 0);
    }

    private void UpdateLevel()
    {
        PlayerPrefs.SetInt(PlayerPrefsConstants.PlayerLevel, CurrentLevel);
    }

    internal bool TryGoToNextLevel()
    {
        if (!IsOnMaxLevel)
        {
            CurrentLevel += 1;

            if (!IsNextLevelAvailable())
            {
                IsOnMaxLevel = true;
                return true;
            }
        }
        return !IsOnMaxLevel;
    }

    public bool IsNextLevelAvailable()
    {
        return levelSO.NextLevelRequirements.Count > _currentLevel + 1;
    }
}
