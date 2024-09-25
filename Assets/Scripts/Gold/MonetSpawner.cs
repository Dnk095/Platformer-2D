using System.Collections;
using System.Linq;
using UnityEngine;

public class MonetSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        float delay = 4f;

        WaitForSeconds wait = new(delay);

        while (enabled)
        {
            TrySpawnInRandomPoint();

            yield return wait;
        }
    }

    private void TrySpawnInRandomPoint()
    {
        SpawnPoint[] spawnPoints = _spawnPoints.Where(point => point.CanSpawn == true).ToArray();

        if (spawnPoints.Length > 0)
            spawnPoints[Random.Range(0, spawnPoints.Length)].Spawn();
    }
}