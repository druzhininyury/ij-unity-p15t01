using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Color : MonoBehaviour
{
    private readonly UnityEngine.Color _initialColor = UnityEngine.Color.white;
    
    private Renderer _renderer;
    
    private int _colorNameId = Shader.PropertyToID("_Color");
    private UnityEngine.Color _baseColor;
    private MaterialPropertyBlock _materialPropertyBlock;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_materialPropertyBlock);
    }

    public void Reset()
    {
        _materialPropertyBlock.SetColor(_colorNameId, _initialColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void SetRandomColor()
    {
        _baseColor = GetRandomColor();
        _materialPropertyBlock.SetColor(_colorNameId, _baseColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void LerpToBlack(float value)
    {
        UnityEngine.Color currentColor = UnityEngine.Color.Lerp(_baseColor, UnityEngine.Color.black, value);
        _materialPropertyBlock.SetColor(_colorNameId, currentColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    
    private UnityEngine.Color GetRandomColor()
    {
        float defaultSaturation = 1f;
        float defualtValue = 1f;
        
        return UnityEngine.Color.HSVToRGB(Random.value, defaultSaturation, defualtValue);
    }
}
