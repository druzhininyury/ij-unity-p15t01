using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPoolable : MonoBehaviour, IPoolable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPoolGet()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
