using UnityEngine;

public class ClientLevelScale : MonoBehaviour
{
    [HideInInspector] public int[] LvlScale;
    [HideInInspector] public int[] MaxClientNumber;

    private ClientSpawner _clientSpawner;
    private int _highestPossibleLvlScale;

    private void Start()
    {
        _clientSpawner = GetComponent<ClientSpawner>();
    }

    private void Update()
    {
        for (int i = 0; i < LvlScale.Length; i++)
        {
            if (LvlScale[i] <= LevelManager.Instance.CurrentLevel + 1)
            {
                if (LvlScale[i] >= _highestPossibleLvlScale)
                {
                    _highestPossibleLvlScale = LvlScale[i];
                    _clientSpawner.MaxClientNumber = MaxClientNumber[i];
                }
            }
        }
    }
}
