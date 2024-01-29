using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StaffStateManager<T> : MonoBehaviour
{
    protected T Instance;

    [Header("Target Points")]
    public Transform IdlePos;
    public Transform DumpPos;

    [Header("Spawners")]
    public ClientSpawner OrdinaryClientSpawner;
    public ClientSpawner VipClientSpawner;

    [Header("ResponsibleOrders")]
    public List<PickupsEnum> OrdersToTake;

    [HideInInspector] public ClientStateManager CurrentClient;
    [HideInInspector] public List<GameObject> AllClientsColl;
    [HideInInspector] public PickupsEnum CurrentOrder;
    [HideInInspector] public NavMeshAgent Agent;

    protected State<T> CurrentState;

    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        UpdateClientsColl();
        CurrentState.UpdateState(Instance);
    }

    public void SetState(State<T> state)
    {
        CurrentState = state;
        CurrentState.EnterState(Instance);
    }

    protected void UpdateClientsColl()
    {
        AllClientsColl.Clear();
        AllClientsColl.AddRange(VipClientSpawner.ClientColl);
        AllClientsColl.AddRange(OrdinaryClientSpawner.ClientColl);
    }

    public virtual void Move(Vector3 targetPos)
    {
        Agent.SetDestination(targetPos);
    }

    public bool CheckOtherClient(PickupAction pickupAction)
    {
        foreach (GameObject client in AllClientsColl)
        {
            if (client == null) continue;

            if (client.TryGetComponent(out ClientStateManager currentClient))
            {
                if (pickupAction.CurrentPickup == currentClient.Order)
                {
                    CurrentClient = currentClient;
                    CurrentOrder = currentClient.Order;
                    return true;
                }
            }
        }
        return false;
    }
}