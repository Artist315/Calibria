public class PutKegState : State<BartenderStateManager>
{
    public override void EnterState(BartenderStateManager stateManager)
    {
        stateManager.Move(stateManager.IdlePos.position);
    }

    public override void UpdateState(BartenderStateManager stateManager)
    {
        if (!stateManager.PickupAction.PickedUp)
        {
            ExitState(stateManager);
        }
    }

    public override void ExitState(BartenderStateManager stateManager)
    {
        stateManager.SetState(stateManager.BartenderChooseActionState);
    }
}
