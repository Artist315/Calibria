using UnityEngine;

public class BeerSpawnView : MonoBehaviour
{
    [SerializeField] private GameObject _redFlag, _greenFlag;

    private PickupSpawnPos _beerSpawn;

    private void Start()
    {
        _beerSpawn = GetComponent<PickupSpawnPos>();
    }

    private void Update()
    {
        if (_beerSpawn.IsFueled)
        {
            _greenFlag.SetActive(true);
            _redFlag.SetActive(false);
        }
        else
        {
            _greenFlag.SetActive(false);
            _redFlag.SetActive(true);
        }
    }
}
