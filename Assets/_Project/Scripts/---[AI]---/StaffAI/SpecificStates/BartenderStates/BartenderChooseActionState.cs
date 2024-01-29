using UnityEngine;
using UnityEngine.AI;

public class BartenderChooseActionState : State<BartenderStateManager>
{
    private bool _isAfterKeg = false;
    private bool _isAfterOrder = false;

    public override void EnterState(BartenderStateManager stateManager)
    {
        stateManager.CurrentClient = null;
        stateManager.CurrentOrder = PickupsEnum.None;

        stateManager.Move(stateManager.IdlePos.position);
    }

    public override void UpdateState(BartenderStateManager stateManager)
    {
        if (stateManager.Agent.pathStatus == NavMeshPathStatus.PathComplete &&
            stateManager.Agent.remainingDistance == 0f)
        {
            RotationHelper.SmoothLookAtTarget(stateManager.transform, stateManager.IdlePos, 240f);
        }

        if (ChooseKeg(stateManager))
        {
            _isAfterKeg = true;
        }
        else if (ChooseClient(stateManager))
        {
            _isAfterOrder = true;
        }
        else return;

        ExitState(stateManager);
    }

    public override void ExitState(BartenderStateManager stateManager)
    {
        if (_isAfterKeg)
        {
            _isAfterKeg = false;
            stateManager.SetState(stateManager.GoAfterKegState);
        }
        else if (_isAfterOrder)
        {
            _isAfterOrder = false;
            stateManager.SetState(stateManager.BartenderGoAfterPickupState);
        }
    }

    public bool ChooseClient(BartenderStateManager stateManager)
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

    public bool ChooseKeg(BartenderStateManager stateManager)
    {
        if (stateManager.AlcoholPickupSpawner.CurrentAlcoholCapacity <= 0)
        {
            stateManager.CurrentOrder = PickupsEnum.Keg;
            return true;
        }
        else
            return false;
    }


}
