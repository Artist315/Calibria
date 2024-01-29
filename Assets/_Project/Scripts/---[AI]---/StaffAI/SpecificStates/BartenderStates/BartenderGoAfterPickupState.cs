public class BartenderGoAfterPickupState : State<BartenderStateManager>
{
    public override void EnterState(BartenderStateManager stateManager)
    {
        GoAfterOrderItem(stateManager);
    }

    public override void UpdateState(BartenderStateManager stateManager)
    {
        if (stateManager.PickupAction.PickedUp)
        {
            ExitState(stateManager);
        }
    }

    public override void ExitState(BartenderStateManager stateManager)
    {
        stateManager.SetState(stateManager.BartenderGivePickupState);
    }

    private void GoAfterOrderItem(BartenderStateManager stateManager)
    {
        if (stateManager.CurrentOrder == PickupsEnum.Beer)
        {
            stateManager.Move(stateManager.BeerSpawnerPos.position);
        }
        else if (stateManager.CurrentOrder == PickupsEnum.Whiskey)
        {
            stateManager.Move(stateManager.WhiskeySpawnerPos.position);
        }
    }
}
