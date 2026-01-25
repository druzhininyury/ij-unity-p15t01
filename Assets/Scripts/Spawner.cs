using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _width = 1f;
    [SerializeField] private float _length = 1f;
    
    // ToDo: Поменять тип GameObject на более специализированный.
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private float _objectsPerSecond = 1f;

    private readonly Color _gizmoColor = Color.green;

    private float _delayTimer = 0f;

    private void OnDrawGizmosSelected()
    {
        Vector3 center = transform.localPosition;

        Vector3[] areaCorners =
        {
            new Vector3(_width / 2, 0f, _length / 2),
            new Vector3(_width / 2, 0f, -_length / 2),
            new Vector3(-_width / 2, 0f, -_length / 2),
            new Vector3(-_width / 2, 0f, _length / 2)
        };

        for (int index = 0; index < areaCorners.Length; ++index)
        {
            areaCorners[index] = transform.TransformPoint(areaCorners[index]);
        }

        for (int index = 0; index < areaCorners.Length; ++index)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawLine(areaCorners[index % areaCorners.Length], areaCorners[(index + 1) % areaCorners.Length]);
        }
    }

    private void Update()
    {
        _delayTimer += Time.deltaTime;

        if (_delayTimer < _objectsPerSecond)
        {
            return;
        }
        
        int objectsToSpawnCounter = (int)(_delayTimer /  _objectsPerSecond);
        _delayTimer %= _objectsPerSecond;

        for (int objectNumber = 0; objectNumber < objectsToSpawnCounter; ++objectNumber)
        {
            Vector3 spawnPoint = GetRandomSpawnPosition();
            Instantiate(_spawnPrefab, spawnPoint, _spawnPrefab.transform.rotation);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.value * _width - _width / 2;
        float randomZ = Random.value * _length - _length / 2;
        return transform.TransformPoint(new Vector3(randomX, 0f, randomZ));
    }
}
