
public class ReputationManager : BaseResourceManager
{
    public LevelManager LevelManager;
    public ReputationUI ReputationUI;

    public static ReputationManager Instance;
    public override string Name => "Reputation";

    private int? _nextLevelRequirement;

    internal override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start()
    {
        _nextLevelRequirement = LevelManager.GetNextLevelRequirement();
        if (_nextLevelRequirement.HasValue)
        {
            ReputationUI.SetMaxValue(_nextLevelRequirement.Value);
            ReputationUI.UpdateValue(Resource);
        }
        else
        {
            ReputationUI.OnMaxLevelReached();
        }
    }

    public override void AddResource(int addAmount)
    {
        Resource += addAmount;
        if (_nextLevelRequirement != null && Resource >= _nextLevelRequirement.Value && !LevelManager.IsOnMaxLevel)
        {
            if (LevelManager.TryGoToNextLevel())
            {
                Resource -= _nextLevelRequirement.Value;
                _nextLevelRequirement = LevelManager.GetNextLevelRequirement();
            }
            if (LevelManager.IsOnMaxLevel)
            {
                ReputationUI.OnMaxLevelReached();
            }
        }
        SaveResource();

        if (!LevelManager.IsOnMaxLevel)
        {
            ReputationUI.SetMaxValue(_nextLevelRequirement.Value);
            ReputationUI.UpdateValue(Resource);
        }
    }
}