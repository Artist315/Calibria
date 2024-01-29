public class GoAfterGarbageState : State<WaitressStateManager>
{
    public override void EnterState(WaitressStateManager stateManager)
    {
        stateManager.Move(stateManager.GarbageTarget.SitPos.position);
    }

    public override void UpdateState(WaitressStateManager stateManager)
    {
        if (stateManager.GarbageTarget.SpawnedGarbage == null)
        {
            if (stateManager.PickupAction.CurrentGarbageNumber < stateManager.PickupAction.MaxGarbageNumber)
            {
                if (stateManager.ChooseActionState.ChooseGarbage(stateManager))
                {
                    stateManager.Move(stateManager.GarbageTarget.SitPos.position);
                }
                else if (!stateManager.PickupAction.PickedUp)
                {
                    stateManager.SetState(stateManager.ChooseActionState);
                }
                else
                {
                    ExitState(stateManager);
                }
            }
            else
            {
                ExitState(stateManager);
            }
        }
        else if (stateManager.PickupAction.CurrentGarbageNumber >= stateManager.PickupAction.MaxGarbageNumber)
        {
            ExitState(stateManager);
        }
    }

    public override void ExitState(WaitressStateManager stateManager)
    {
        stateManager.SetState(stateManager.ThrowAwayState);
    }
}
