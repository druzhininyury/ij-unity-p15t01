using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _width = 1f;
    [SerializeField] private float _length = 1f;
    
    [SerializeField] private PoolableController _spawnPrefab;
    [SerializeField] private float _objectsPerSecond = 1f;

    private readonly Color _gizmoColor = Color.green;

    [SerializeField] private ObjectPool<PoolableController> _pool;

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

    private void Awake()
    {
        _pool = new ObjectPool<PoolableController>(
            () =>
            {
                PoolableController poolableController = Instantiate(_spawnPrefab, transform);
                poolableController.HookToPool(_pool);
                return poolableController;
            },
            poolableController => poolableController.OnGetFromPool(),
            poolableController => poolableController.OnReleaseToPool()
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
       WaitForSeconds wait = new WaitForSeconds(_objectsPerSecond);
        
        while (true)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        PoolableController spawnedObject = _pool.Get();
        Vector3 spawnPoint = GetRandomSpawnPosition();
        spawnedObject.transform.position = spawnPoint;
        spawnedObject.transform.rotation = _spawnPrefab.transform.rotation;
        spawnedObject.transform.localScale = _spawnPrefab.transform.localScale;
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.value * _width - _width / 2;
        float randomZ = Random.value * _length - _length / 2;
        return transform.TransformPoint(new Vector3(randomX, 0f, randomZ));
    }
}
