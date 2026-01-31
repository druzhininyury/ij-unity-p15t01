using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PoolableController))]
[RequireComponent(typeof(LifetimeController))]
public class LifetimeController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private float _lifetime = 0f;
    private float _age = 0f;
    
    private ColorController _colorController;

    private bool _isActivated = false;

    private PoolableController _poolableController;

    private void Awake()
    {
        _colorController = GetComponent<ColorController>();
        _poolableController = GetComponent<PoolableController>();
    }

    private void Update()
    {
        _age += Time.deltaTime;

        if (_age >= _lifetime)
        {
            _poolableController.ReleaseToPool();
        }
        
        _colorController.LerpToBlack(_age / _lifetime);
    }

    public void OnPoolGet()
    {
       ResetSettings();
    }

    public void Activate()
    {
        if (_isActivated)
        {
            return;
        }

        _isActivated = true;
        enabled = true;
        
        _colorController.SetRandomColor();
    }

    private void ResetSettings()
    {
        _colorController.Reset();
        
        enabled = false;
        
        _lifetime =  + _minLifetime + Random.value * (_maxLifetime - _minLifetime);
        _age = 0f;
        _isActivated = false;
    }
}
