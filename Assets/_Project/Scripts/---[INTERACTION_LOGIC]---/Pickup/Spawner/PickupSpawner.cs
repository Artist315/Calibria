using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public PickupSpawnPos[] SpawnPos;
    [HideInInspector] public List<GameObject> Pickups = new();
    
    public int MaxPickups;
    public float SpawnCooldown;
    
    public GameObject Pickup;
    
    [HideInInspector] public float Cooldown = 0;

    [SerializeField]
    internal AudioManager _audioManager;


    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }
    }

    void Update()
    {
        if (Pickups.Count >= MaxPickups) return;

        Cooldown += Time.deltaTime;

        if (Cooldown >= SpawnCooldown)
        {
            Cooldown = 0;
            
            ChooseSpawnPoint();
        }
    }

    private void PlayPickUpSound(GameObject pickup)
    {
        if (_audioManager != null)
        {
            _audioManager.PlayPredefinedAudio();
        }
    }
    protected virtual void Spawn(PickupSpawnPos spawnPos)
    {
        GameObject pickup = Instantiate(Pickup, spawnPos.Position, Quaternion.identity);
        spawnPos.IsAvailable = false;
        
        PickupController pickupController = pickup.GetComponent<PickupController>();
        pickupController.OnPickup += ClearPickup;
        pickupController.OnPickup += PlayPickUpSound;

        Pickups.Add(pickup);
    }
    
    protected virtual void ChooseSpawnPoint()
    {
        foreach (PickupSpawnPos spawnPos in SpawnPos)
        {
            if (spawnPos.IsAvailable)
            {
                Spawn(spawnPos);
                break;
            }
        }
    }

    protected void ClearPickup(GameObject pickup)
    {
        Pickups.Remove(pickup);
        
        foreach (PickupSpawnPos spawnPos in SpawnPos)
        {
            if (spawnPos.Position == pickup.transform.position)
            {
                spawnPos.IsAvailable = true;
                break;
            }
        }
        
        pickup.GetComponent<PickupController>().OnPickup -= ClearPickup;
        pickup.GetComponent<PickupController>().OnPickup -= PlayPickUpSound;
    }
}
