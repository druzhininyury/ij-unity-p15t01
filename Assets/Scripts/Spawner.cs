using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CubePool))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _width = 1f;
    [SerializeField] private float _length = 1f;
    
    [SerializeField] private float _objectsPerSecond = 1f;

    private CubePool _cubePool;

    private readonly Color _gizmoColor = Color.green;

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
        _cubePool = GetComponent<CubePool>();
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
        CubeController spawnedObject = _cubePool.GetCube();
        Vector3 spawnPoint = GetRandomSpawnPosition();
        spawnedObject.transform.position = spawnPoint;
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.value * _width - _width / 2;
        float randomZ = Random.value * _length - _length / 2;
        return transform.TransformPoint(new Vector3(randomX, 0f, randomZ));
    }
}
