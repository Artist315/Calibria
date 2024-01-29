using UnityEngine;

public class AlcoholPickupSpawner : PickupSpawner
{

    [HideInInspector] public int CurrentAlcoholCapacity;

    private int _maxAlcoholCapacity;

    void Start()
    {
        _maxAlcoholCapacity = MaxPickups;
        CurrentAlcoholCapacity = 0;
    }

    protected override void Spawn(PickupSpawnPos spawnPos)
    {
        if (CurrentAlcoholCapacity <= 0)
        {
            Cooldown = 0f;
            return;
        }

        CurrentAlcoholCapacity--;

        if (_audioManager != null)
        {
            _audioManager.PlayPredefinedAudio();

        }

        base.Spawn(spawnPos);
        spawnPos.IsFueled = false;
    }

    public void FillKegerator(int kegFillAmount)
    {
        CurrentAlcoholCapacity += kegFillAmount;
        if (CurrentAlcoholCapacity > _maxAlcoholCapacity) CurrentAlcoholCapacity = _maxAlcoholCapacity;

        foreach (var item in SpawnPos)
        {
            item.IsFueled = true;
        }
    }

    protected override void ChooseSpawnPoint()
    {
        foreach (PickupSpawnPos spawnPos in SpawnPos)
        {
            if (spawnPos.IsAvailable && spawnPos.IsFueled)
            {
                Spawn(spawnPos);
                break;
            }
        }
    }
}
