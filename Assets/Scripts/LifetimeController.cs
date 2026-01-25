using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class LifetimeController : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private float _lifetime;
    private float _age = 0f;
    
    private Renderer _renderer;
    
    private Color _initialColor;
    private MaterialPropertyBlock _materialPropertyBlock;

    private bool _isActivated = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        
        _lifetime =  + _minLifetime + Random.value * (_maxLifetime - _minLifetime);
    }

    private void Update()
    {
        _age += Time.deltaTime;

        if (_age >= _lifetime)
        {
            Destroy(gameObject);
        }
        
        Color currentColor = Color.Lerp(_initialColor, Color.black, _age / _lifetime);
        _materialPropertyBlock.SetColor("_Color", currentColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
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
    
    private Color GetRandomColor()
    {
        float defaultSaturation = 1f;
        float defualtValue = 1f;
        
        return Color.HSVToRGB(Random.value, defaultSaturation, defualtValue);
    }
}
