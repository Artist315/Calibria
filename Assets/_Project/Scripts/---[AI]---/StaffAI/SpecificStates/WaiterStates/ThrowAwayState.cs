using UnityEngine;

public class ThrowAwayState : State<WaitressStateManager>
{
    private float _checkGarbageTime = 3f;
    private float _timer;

    public override void EnterState(WaitressStateManager stateManager)
    {
        _timer = _checkGarbageTime;
        
        if (stateManager.PickupAction.CurrentPickup == PickupsEnum.Garbage)
        {
            stateManager.Move(stateManager.SinkPos.position);
        }
        else
        {
            stateManager.Move(stateManager.DumpPos.position);
        }
    }

    public override void UpdateState(WaitressStateManager stateManager)
    {
        CheckOnMoreGarbage(stateManager);
        
        if (!stateManager.PickupAction.PickedUp)
        {
            ExitState(stateManager);
        }
    }

    public override void ExitState(WaitressStateManager stateManager)
    {
        stateManager.SetState(stateManager.ChooseActionState);
    }

    public void CheckOnMoreGarbage(WaitressStateManager stateManager)
    {
        _timer -= Time.deltaTime;

        if (_timer > 0)
        {
            if (stateManager.PickupAction.CurrentGarbageNumber < stateManager.PickupAction.MaxGarbageNumber)
            {
                if (stateManager.ChooseActionState.ChooseGarbage(stateManager))
                {
                    stateManager.SetState(stateManager.GoAfterGarbageState);
                }
            }
        }
    }
}
