public class VipClientStateManager : ClientStateManager
{
    public override void FilterFreeSitPoints()
    {
        FreeSitPoints.Clear();
        
        foreach (ClientSitPoint sitPoint in SitPoints)
        {
            if (sitPoint.IsAvailable && sitPoint.IsVip)
            {
                FreeSitPoints.Add(sitPoint);
            }
        }
    }
}
