using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(LifetimeController))]
public class CollideController : MonoBehaviour
{
    private LifetimeController _lifetimeController;

    private void Start()
    {
        _lifetimeController = GetComponent<LifetimeController>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(typeof(PlanesMarker), out Component _) == false)
        {
            return;
        }
        
        _lifetimeController.Activate();
        enabled = false;
    }
}
