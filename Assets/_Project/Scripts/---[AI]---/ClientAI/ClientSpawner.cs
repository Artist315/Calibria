using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public Transform ExitPos;
    public Transform IdlePos;

    [HideInInspector] public int MaxClientNumber;
    [HideInInspector] public float SpawnCooldown;

    [HideInInspector] public List<ClientSitPoint> SitPoints = new();
    [HideInInspector] public List<GameObject> ClientColl = new();

    [SerializeField] private GameObject[] _clients;

    void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    public IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        FindSitPoints();
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        if (ClientColl.Count >= MaxClientNumber) yield return new WaitUntil(() => ClientColl.Count < MaxClientNumber);
        SpawnMafia();

        yield return new WaitForSeconds(SpawnCooldown);

        StartCoroutine(Spawn());
    }

    public void SpawnMafia()
    {
        GameObject randomClient = _clients[Random.Range(0, _clients.Length)];
        GameObject spawnedClient = Instantiate(randomClient, transform.position, Quaternion.identity);
        ClientColl.Add(spawnedClient);

        ClientStateManager ClientStateManager = spawnedClient.GetComponent<ClientStateManager>();

        ClientStateManager.SitPoints = SitPoints;
        ClientStateManager.IdleTarget = IdlePos;
        ClientStateManager.ExitTarget = ExitPos;
    }

    void Update()
    {
        foreach (var item in ClientColl)
        {
            if (item == null)
            {
                ClientColl.Remove(item);
                break;
            }
        }
    }

    public void FindSitPoints()
    {
        SitPoints.Clear();

        GameObject[] sitPoints = GameObject.FindGameObjectsWithTag(TagConstants.ClientSitPoint);
        foreach (GameObject sitPoint in sitPoints)
        {
            if (sitPoint.TryGetComponent(out ClientSitPoint clientSitPoint))
                SitPoints.Add(clientSitPoint);
        }
    }
}
