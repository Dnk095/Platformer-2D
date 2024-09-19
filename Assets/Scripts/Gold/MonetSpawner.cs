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
            if (CheckAllPionts())
                GetRandomPoint().Spawn();

            yield return wait;
        }
    }

    private SpawnPoint GetRandomPoint()
    {
        SpawnPoint[] spawnPoint = _spawnPoints.Where(point => point.CanSpawn == true).ToArray();

        return spawnPoint[Random.Range(0, spawnPoint.Length)];
    }

    private bool CheckAllPionts()
    {
        return _spawnPoints.Any(x => x.CanSpawn == true);
    }
}