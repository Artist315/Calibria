using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClientStateManager : MonoBehaviour
{
    public ClientIdleState IdleState = new ClientIdleState();
    public EnterBarState EnterBarState = new EnterBarState();
    public WaitOrderState WaitOrderState = new WaitOrderState();
    public EatState EatState = new EatState();
    public ExitBarState ExitBarState = new ExitBarState();

    [HideInInspector] public List<ClientSitPoint> SitPoints = new();
    [HideInInspector] public List<ClientSitPoint> FreeSitPoints = new();
    [HideInInspector] public ClientPickupAction ClientPickup;
    [HideInInspector] public ClientOrderReward OrderReward;
    [HideInInspector] public MoodStates MoodState;
    [HideInInspector] public Transform SitTarget;
    [HideInInspector] public NavMeshAgent Agent;
    
    /*[HideInInspector]*/ public List<PickupsEnum> OrderTypes;
    /*[HideInInspector]*/ public List<float> OrderTypesDropRates;
    
    public Animator VisitorAnim;
    
    [Header("Properties")]
    public ClientSitPoint TargetSitPoint;
    public PickupsEnum Order = PickupsEnum.None;
    public Transform IdleTarget;
    public Transform ExitTarget;

    [HideInInspector] public int MinEatTime, MaxEatTime;
    [HideInInspector] public float HappyTime, SadTime;
    [HideInInspector] public float MoodChangeDelay;

    private State<ClientStateManager> _currentState;

    void Start()
    {
        OrderReward = GetComponent<ClientOrderReward>();
        Agent = GetComponent<NavMeshAgent>();
        ClientPickup = GetComponent<ClientPickupAction>();

        MoodState = MoodStates.None;
        SetState(IdleState);
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SetState(State<ClientStateManager> state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public virtual void FilterFreeSitPoints()
    {
        FreeSitPoints.Clear();

        foreach (ClientSitPoint sitPoint in SitPoints)
        {
            if (sitPoint.IsAvailable && !sitPoint.IsVip)
            {
                FreeSitPoints.Add(sitPoint);
            }
        }
    }

    public void Move(Vector3 targetPos)
    {
        Agent.SetDestination(targetPos);
    }
}
