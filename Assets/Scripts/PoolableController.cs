using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableController : MonoBehaviour
{
    private IObjectPool<PoolableController> _pool;
    private List<IPoolable> _poolables;

    private void Awake()
    {
        _poolables = gameObject.GetComponents<IPoolable>().ToList();
    }

    public void HookToPool(IObjectPool<PoolableController> pool)
    {
        _pool = pool;
    }

    public void OnGetFromPool()
    {
        gameObject.SetActive(true);

        foreach (IPoolable poolable in _poolables)
        {
            poolable.OnPoolGet();
        }
    }

    public void OnReleaseToPool()
    {
        gameObject.SetActive(false);
    }

    public void ReleaseToPool()
    {
        _pool.Release(this);
    }
}
