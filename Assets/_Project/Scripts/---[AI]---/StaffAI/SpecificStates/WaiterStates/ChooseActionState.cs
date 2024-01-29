using UnityEngine;
using UnityEngine.AI;

public class ChooseActionState : State<WaitressStateManager>
{
    private bool _isAfterGarbage = false;
    private bool _isAfterOrder = false;

    public override void EnterState(WaitressStateManager stateManager)
    {
        stateManager.CurrentClient = null;
        stateManager.CurrentOrder = PickupsEnum.None;

        stateManager.Move(stateManager.IdlePos.position);
    }
    
    public override void UpdateState(WaitressStateManager stateManager)
    {
        if (stateManager.Agent.pathStatus == NavMeshPathStatus.PathComplete &&
            stateManager.Agent.remainingDistance == 0f)
        {
            RotationHelper.SmoothLookAtTarget(stateManager.transform, stateManager.IdlePos, 240f);
        }
        
        if (ChooseClient(stateManager))
        {
            _isAfterOrder = true;
        }
        else if (ChooseGarbage(stateManager))
        {
            _isAfterGarbage = true;
        }
        else return;
        
        ExitState(stateManager);
    }

    public override void ExitState(WaitressStateManager stateManager)
    {
        if (_isAfterOrder)
        {
            _isAfterOrder = false;
            stateManager.SetState(stateManager.GoAfterPickupState);
        }
        else if (_isAfterGarbage)
        {
            _isAfterGarbage = false;
            stateManager.SetState(stateManager.GoAfterGarbageState);
        }
    }

    private bool ChooseClient(WaitressStateManager stateManager)
    {
        foreach (GameObject client in stateManager.AllClientsColl)
        {
            if (client == null) continue;

            if (client.TryGetComponent(out ClientStateManager currentClient))
            {
                if (stateManager.OrdersToTake.Contains(currentClient.Order))
                {
                    stateManager.CurrentClient = currentClient;
                    stateManager.CurrentOrder = currentClient.Order;
                    return true;
                }
            }
        }
        return false;
    }

    public bool ChooseGarbage(WaitressStateManager stateManager)
    {
        foreach (ClientSitPoint sitPoint in stateManager.SitPoints)
        {
            if (sitPoint == null)
            {
                continue;
            }
            
            if (sitPoint.SpawnedGarbage != null && !sitPoint.IsAvailable)
            {
                stateManager.CurrentOrder = PickupsEnum.Garbage;
                stateManager.GarbageTarget = sitPoint;
                return true;
            }
        }
        return false;
    }
}
