using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;

    private void Update()
    {
        transform.position=new Vector3(_playerPosition.position.x, _playerPosition.position.y,transform.position.z);
    }
}


