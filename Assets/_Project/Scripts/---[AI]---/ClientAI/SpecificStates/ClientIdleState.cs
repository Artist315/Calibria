using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClientIdleState : State<ClientStateManager>
{
    protected NavMeshAgent Agent;

    public override void EnterState(ClientStateManager clientManager)
    {
        clientManager.Move(clientManager.IdleTarget.position);

        if (clientManager.TargetSitPoint == null)
        {
            clientManager.StartCoroutine(ChooseFreeSeat(clientManager));
        }
        else
        {
            clientManager.TargetSitPoint.IsAvailable = false;
            clientManager.SitTarget = clientManager.TargetSitPoint.SitPos;
            ExitState(clientManager);
        }
    }
    
    public override void ExitState(ClientStateManager clientManager)
    {
        clientManager.SetState(clientManager.EnterBarState);
    }

    IEnumerator ChooseFreeSeat(ClientStateManager clientManager)
    {
        clientManager.FilterFreeSitPoints();
        
        if (clientManager.FreeSitPoints == null || clientManager.FreeSitPoints.Count == 0)
        {
            yield return new WaitForSeconds(3f);
            clientManager.StartCoroutine(ChooseFreeSeat(clientManager));
            yield break;
        }

        ClientSitPoint randomSitPoint = clientManager.FreeSitPoints[Random.Range(0, clientManager.FreeSitPoints.Count)];
        randomSitPoint.IsAvailable = false;

        clientManager.TargetSitPoint = randomSitPoint;
        clientManager.SitTarget = randomSitPoint.SitPos;

        ExitState(clientManager);
    }
}
