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
        SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        while (spawnPoint.CanSpawn == false)
            spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        return spawnPoint;
    }

    private bool CheckAllPionts()
    {
        return  _spawnPoints.Any(x => x.CanSpawn == true);
    }
}
