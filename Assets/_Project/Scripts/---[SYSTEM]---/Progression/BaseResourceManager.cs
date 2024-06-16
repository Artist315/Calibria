using UnityEngine;

public abstract class BaseResourceManager : MonoBehaviour
{
    private int resource;

    public int Resource {
        get
        {
            if (!_isLoaded)
            {
                LoadResource();
            }
            return resource;
        }
        internal set => resource = value; 
    }
    public abstract string Name { get; }

    private bool _isLoaded = false;
    
    internal virtual void Awake()
    {
        if (!_isLoaded)
        {
            LoadResource();
        }
    }

    internal void SaveResource()
    {
        ResourcesEvent.CallResourceValueUpdated();
        PlayerPrefs.SetInt(Name, Resource);
    }

    private void LoadResource()
    {
        Resource = PlayerPrefs.GetInt(Name, 0);
        _isLoaded = true;
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
