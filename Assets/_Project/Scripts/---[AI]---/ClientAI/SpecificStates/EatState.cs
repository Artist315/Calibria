using UnityEngine;

public class EatState : State<ClientStateManager>
{
    float _eatTime;

    public override void EnterState(ClientStateManager stateManager)
    {
        _eatTime = Random.Range(stateManager.MinEatTime, stateManager.MaxEatTime);
    }

    public override void UpdateState(ClientStateManager stateManager)
    {
        RotationHelper.SmoothLookAtTarget(stateManager.transform, stateManager.TargetSitPoint.transform, 240f);
        
        _eatTime -= Time.deltaTime;
        if (_eatTime <= 0f) ExitState(stateManager);
    }

    public override void ExitState(ClientStateManager stateManager)
    {
        stateManager.ClientPickup.Pickup.DestroyPickup();
        stateManager.TargetSitPoint.CreateGarbage();

        stateManager.SetState(stateManager.ExitBarState);
    }
}
