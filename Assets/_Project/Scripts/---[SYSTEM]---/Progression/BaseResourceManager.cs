using UnityEngine;

public abstract class BaseResourceManager : MonoBehaviour
{
    
    public int Resource {  get; internal set; }
    public abstract string Name { get; }
    
    internal virtual void Awake()
    {
        LoadResource();
    }

    internal void SaveResource()
    {
        ResourcesEvent.CallResourceValueUpdated();
        PlayerPrefs.SetInt(Name, Resource);
    }

    private void LoadResource()
    {
        Resource = PlayerPrefs.GetInt(Name, 0);
    }
    
    public virtual bool TrySubtractResource(int subtractAmount, out int resource)
    {
        if (Resource >= subtractAmount)
        {
            Resource -= subtractAmount;
            SaveResource();

            resource = Resource;
            return true;
        }
        else
        {
            resource = Resource;
            return false;
        }        
    }
    
    public abstract void AddResource(int addAmount);
}
