using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitressStateManager : StaffStateManager<WaitressStateManager>, IActivation
{
    public ChooseActionState ChooseActionState = new ChooseActionState();
    public GoAfterPickupState GoAfterPickupState = new GoAfterPickupState();
    public GoAfterGarbageState GoAfterGarbageState = new GoAfterGarbageState();
    public GivePickupState GivePickupState = new GivePickupState();
    public ThrowAwayState ThrowAwayState = new ThrowAwayState();

    public Transform SinkPos;

    [Header("SpawnersPos")]
    public Transform PastaSpawnerPos;

    [HideInInspector] public WaitressPickupAction PickupAction;
    [HideInInspector] public List<ClientSitPoint> SitPoints;
    [HideInInspector] public ClientSitPoint GarbageTarget;

    [SerializeField] private UpgradeUI _upgradeUI;

    private Coroutine coroutine { get; set; }

    public void Activate()
    {
        Start();
    }
    protected override void Start()
    {

        Instance = this;
        PickupAction = GetComponent<WaitressPickupAction>();

        base.Start();

        if (coroutine == null)
        {
            coroutine = StartCoroutine(CheckOnNewSitPoints());
        }

        SetState(ChooseActionState);
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
                SetState(ThrowAwayState);
            }
            else
            {
                SetState(GivePickupState);
            }
        }
        else if (CurrentClient != null && CurrentClient.Order == PickupsEnum.None)
            SetState(ChooseActionState);
    }

    private IEnumerator CheckOnNewSitPoints()
    {
        yield return new WaitForSeconds(0.5f);

        FindSitPoints();
        yield return new WaitUntil(() => _upgradeUI.KitchenUpgrade.IsUpgraded);
        FindSitPoints();
        yield return new WaitUntil(() => _upgradeUI.VipUpgrade.IsUpgraded);
        FindSitPoints();
    }

    private void FindSitPoints()
    {
        SitPoints.Clear();

        GameObject[] sitPoints = GameObject.FindGameObjectsWithTag(TagConstants.ClientSitPoint);
        foreach (GameObject sitPoint in sitPoints)
        {
            SitPoints.Add(sitPoint.GetComponent<ClientSitPoint>());
        }

        Debug.Log($"{SitPoints.Count} was found");
    }
}
