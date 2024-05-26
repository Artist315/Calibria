using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ResourcesEvent
{
    public delegate void ResourceValueChanged();
    public static event ResourceValueChanged ResourceValueUpdated;

    public static void CallResourceValueUpdated()
    {
        ResourceValueUpdated?.Invoke();
    }
}
