public class GoAfterKegState : State<BartenderStateManager>
{
    public override void EnterState(BartenderStateManager stateManager)
    {
        stateManager.Move(stateManager.KegSpawnPos.position);
    }
    
    public override void UpdateState(BartenderStateManager stateManager)
    {
        if (!stateManager.BartenderChooseActionState.ChooseKeg(stateManager))
            stateManager.SetState(stateManager.BartenderChooseActionState);
        
        if (stateManager.PickupAction.PickedUp)
        {
            ExitState(stateManager);
        }
    }
    
    public override void ExitState(BartenderStateManager stateManager)
    {
        stateManager.SetState(stateManager.PutKegState);
    }
}
