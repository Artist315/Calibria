public abstract class ClientState
{
    public abstract void EnterState(ClientStateManager stateManager);
    public virtual void UpdateState(ClientStateManager stateManager) { }
    public abstract void ExitState(ClientStateManager stateManager);
}
