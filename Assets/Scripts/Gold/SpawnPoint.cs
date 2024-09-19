using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Monet _monetPrefab;
    [SerializeField] private Monet _monet;

    private bool _canSpawn = true;

    public bool CanSpawn => _canSpawn;

    public void Spawn()
    {
        _monet = Instantiate(_monetPrefab, transform.position, transform.rotation);
        ChangeState();

        _monet.PickUp += OnPickUp;
    }

    private void ChangeState()
    {
        _canSpawn = !_canSpawn;
    }

    private void OnPickUp()
    {
        ChangeState();
        _monet.PickUp -= OnPickUp;
    }
}
