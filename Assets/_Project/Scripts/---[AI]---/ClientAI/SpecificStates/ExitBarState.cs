using UnityEngine;
using UnityEngine.AI;

public class ExitBarState : State<ClientStateManager>
{
    public override void EnterState(ClientStateManager stateManager)
    {
        stateManager.VisitorAnim.SetBool("IsSitting", false);

        stateManager.Move(stateManager.ExitTarget.position);
        stateManager.Agent.stoppingDistance = 3f;
    }

    public override void UpdateState(ClientStateManager stateManager)
    {
        if (stateManager.Agent.pathStatus == NavMeshPathStatus.PathComplete &&
            stateManager.Agent.remainingDistance <= stateManager.Agent.stoppingDistance)
            {
                ExitState(stateManager);
            }
    }

    public override void ExitState(ClientStateManager stateManager)
    {
        Object.Destroy(stateManager.gameObject);
    }
}
