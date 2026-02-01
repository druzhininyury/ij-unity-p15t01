using System;
using UnityEngine;

[RequireComponent(typeof(ColorController))]
[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private float _scale = 0.5f;
    
    private LifetimeController _lifetimeController;
    private ColorController _colorController;
    private Rigidbody _rigidbody;
    private CollideController _collideController;

    public event Action<CubeController> CubeRemoved;

    private void Awake()
    {
        _colorController = GetComponent<ColorController>();
        _rigidbody = GetComponent<Rigidbody>();
        _lifetimeController = new LifetimeController(this, () => CubeRemoved?.Invoke(this), _colorController);
        _collideController = new CollideController(_colorController, _lifetimeController);

        Reset();
    }

    private void OnCollisionEnter(Collision other)
    {
        _collideController.ProcessCollision(other);
    }

    public void Reset()
    {
        ResetTransform();
        ResetRigidbody();
        _colorController.Reset();
        _collideController.Reset();
        _lifetimeController.Reset();
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
