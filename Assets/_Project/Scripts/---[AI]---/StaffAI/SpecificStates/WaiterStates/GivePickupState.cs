public class GivePickupState : State<WaitressStateManager>
{
    public override void EnterState(WaitressStateManager stateManager)
    {
        stateManager.Move(stateManager.CurrentClient.transform.position);
    }

    public override void UpdateState(WaitressStateManager stateManager)
    {
        if (!stateManager.PickupAction.PickedUp)
        {   
            ExitState(stateManager);
        }
    }

    public override void ExitState(WaitressStateManager stateManager)
    {
        stateManager.SetState(stateManager.ChooseActionState);
    }
}
