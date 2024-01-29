using UnityEngine;
using UnityEngine.AI;

public class EnterBarState : ClientIdleState
{
    public override void EnterState(ClientStateManager clientManager)
    {
        clientManager.Move(clientManager.SitTarget.position);
    }
    
    public override void UpdateState(ClientStateManager clientManager)
    {
        if (clientManager.Agent.pathStatus != NavMeshPathStatus.PathComplete) return;
        
        float distance = Vector3.Distance(clientManager.SitTarget.position, clientManager.transform.position);
        if (clientManager.Agent.remainingDistance != 0f || distance > 0.5f) return;
        
        ExitState(clientManager);
    }
    
    public override void ExitState(ClientStateManager clientManager)
    {
        clientManager.SetState(clientManager.WaitOrderState);
    }
}
