using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeController _spawnPrefab;
    
    private ObjectPool<CubeController> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<CubeController>(
            () => Instantiate(_spawnPrefab, transform),
            cubeController => GetCube(cubeController),
            cubeController => ReleaseCube(cubeController)
        );
    }

    public CubeController GetCube()
    {
        return _pool.Get();
    }

    private void GetCube(CubeController cubeController)
    {
        cubeController.gameObject.SetActive(true);
        cubeController.OnCubeRemove += ReleaseCube;
    }
    
    private void ReleaseCube(CubeController cubeController)
    {
        cubeController.Reset();
        cubeController.gameObject.SetActive(false);
        cubeController.OnCubeRemove -= ReleaseCube;
    }
}
