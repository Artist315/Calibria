public class BartenderGivePickupState : State<BartenderStateManager>
{
    public override void EnterState(BartenderStateManager stateManager)
    {
        stateManager.Move(stateManager.CurrentClient.transform.position);
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
