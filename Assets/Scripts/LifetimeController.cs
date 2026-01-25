using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(PoolableController))]
public class LifetimeController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private float _lifetime = 0f;
    private float _age = 0f;
    
    private Renderer _renderer;
    
    private Color _initialColor;
    private MaterialPropertyBlock _materialPropertyBlock;

    private bool _isActivated = false;

    private PoolableController _poolableController;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        
        _poolableController = GetComponent<PoolableController>();
    }

    private void Update()
    {
        _age += Time.deltaTime;

        if (_age >= _lifetime)
        {
            _poolableController.ReleaseToPool();
        }
        
        Color currentColor = Color.Lerp(_initialColor, Color.black, _age / _lifetime);
        _materialPropertyBlock.SetColor("_Color", currentColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
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
        
        _initialColor = GetRandomColor();
        _materialPropertyBlock.SetColor("_Color", _initialColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    private void ResetSettings()
    {
        _materialPropertyBlock.SetColor("_Color", Color.white);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
        
        enabled = false;
        
        _lifetime =  + _minLifetime + Random.value * (_maxLifetime - _minLifetime);
        _age = 0f;
        _isActivated = false;
    }
    
    private Color GetRandomColor()
    {
        float defaultSaturation = 1f;
        float defualtValue = 1f;
        
        return Color.HSVToRGB(Random.value, defaultSaturation, defualtValue);
    }
}
