public abstract class State<T>
{
    public virtual void EnterState(T stateManager) { }
    public virtual void UpdateState(T stateManager) { }
    public virtual void ExitState(T stateManager) { }
}
