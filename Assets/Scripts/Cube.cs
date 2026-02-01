using System;
using UnityEngine;

[RequireComponent(typeof(Color))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private float _scale = 0.5f;
    
    private Lifetime _lifetime;
    private Color _color;
    private Rigidbody _rigidbody;
    private Collide _collide;

    public event Action<Cube> CubeRemoved;

    private void Awake()
    {
        _color = GetComponent<Color>();
        _rigidbody = GetComponent<Rigidbody>();
        _lifetime = new Lifetime(this, () => CubeRemoved?.Invoke(this), _color);
        _collide = new Collide(_color, _lifetime);

        Reset();
    }

    private void OnCollisionEnter(Collision other)
    {
        _collide.ProcessCollision(other);
    }

    public void Reset()
    {
        ResetTransform();
        ResetRigidbody();
        _color.Reset();
        _collide.Reset();
        _lifetime.Reset();
    }

    private void ResetTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = _scale * Vector3.one;
    }

    private void ResetRigidbody()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
