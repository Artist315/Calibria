using UnityEngine;

public class BartenderStateManager : StaffStateManager<BartenderStateManager>
{
    public BartenderChooseActionState BartenderChooseActionState = new BartenderChooseActionState();
    public BartenderGoAfterPickupState BartenderGoAfterPickupState = new BartenderGoAfterPickupState();
    public BartenderGivePickupState BartenderGivePickupState = new BartenderGivePickupState();
    public BartenderThrowAwayState BartenderThrowAwayState = new BartenderThrowAwayState();
    public GoAfterKegState GoAfterKegState = new GoAfterKegState();
    public PutKegState PutKegState = new PutKegState();

    [Header("SpawnersPos")]
    public Transform BeerSpawnerPos;
    public Transform WhiskeySpawnerPos;
    public Transform KegSpawnPos;

    [Header("PickupSpawner")]
    public AlcoholPickupSpawner AlcoholPickupSpawner;

    [HideInInspector] public BartenderPickupAction PickupAction;

    protected override void Start()
    {
        Instance = this;
        PickupAction = GetComponent<BartenderPickupAction>();

        base.Start();

        SetState(BartenderChooseActionState);
    }

    protected override void Update()
    {
        base.Update();

        CheckOrderRelevancy();
    }

    public void CheckOrderRelevancy()
    {
        if (CurrentClient != null && CurrentClient.Order == PickupsEnum.None && PickupAction.PickedUp)
        {
            if (!CheckOtherClient(PickupAction))
            {
                SetState(BartenderThrowAwayState);
            }
            else
            {
                SetState(BartenderGivePickupState);
            }
        }
        else if (CurrentClient != null && CurrentClient.Order == PickupsEnum.None)
            SetState(BartenderChooseActionState);
    }
}
