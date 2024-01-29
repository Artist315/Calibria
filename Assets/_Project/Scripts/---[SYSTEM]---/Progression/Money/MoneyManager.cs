
public class MoneyManager : BaseResourceManager
{
    public delegate void ChangeCallback(int amount, bool isAdding);
    public event ChangeCallback OnChangeMoney;
    
    public MoneyUI MoneyUI;
    
    public static MoneyManager Instance;
    public override string Name => "Money";

    internal override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start()
    {
        MoneyUI.UpdateValue(Resource);
    }

    public override void AddResource(int addAmount)
    {
        Resource += addAmount;
        SaveResource();
        
        OnChangeMoney?.Invoke(addAmount, true);
        MoneyUI.UpdateValue(Resource);
    }
    
    public override bool TrySubtractResource(int subtractAmount, out int resource)
    {
        var result = base.TrySubtractResource(subtractAmount, out resource);
        
        OnChangeMoney?.Invoke(subtractAmount, false);
        MoneyUI.UpdateValue(Resource);
        
        return result;
    }
}
