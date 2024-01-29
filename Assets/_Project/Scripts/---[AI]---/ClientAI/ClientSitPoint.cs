using UnityEngine;

public class ClientSitPoint : MonoBehaviour
{
    public GameObject BeerGarbage, PastaGarbage, WhiskeyGarbage;
    public Transform GarbagePos;

    [HideInInspector] public Transform SitPos;
    [HideInInspector] public PickupsEnum OrderGarbage;
    [HideInInspector] public GameObject SpawnedGarbage;
    
    public bool IsAvailable = true;
    public bool IsVip = false;
    public bool IsPoker = false;

    private GameObject _garbage;

    void Awake()
    {
        SitPos = transform;
    }

    public void CreateGarbage()
    {
        if (OrderGarbage == PickupsEnum.Beer) _garbage = BeerGarbage;
        else if (OrderGarbage == PickupsEnum.Pasta) _garbage = PastaGarbage;
        else if (OrderGarbage == PickupsEnum.Whiskey) _garbage = WhiskeyGarbage;
        
        SpawnedGarbage = Instantiate(_garbage, GarbagePos.position, Quaternion.identity);
        
        GarbagePickupController pickupController = SpawnedGarbage.GetComponent<GarbagePickupController>();
        GarbageView garbageView = GarbagePos.GetComponent<GarbageView>();
        
        garbageView.GarbageEnum = pickupController.GarbageEnum;
        
        SpawnedGarbage.GetComponent<GarbageAnimationSwitch>().Anim = GarbagePos.GetComponentInChildren<Animator>();
        pickupController.OnPickup += MakeAvailable;
    }
    
    private void MakeAvailable(GameObject pickup)
    {
        IsAvailable = true;
        SpawnedGarbage = null;
        
        pickup.GetComponent<PickupController>().OnPickup -= MakeAvailable;
    }
}
