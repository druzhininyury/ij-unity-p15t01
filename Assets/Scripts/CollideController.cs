using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(LifetimeController))]
public class CollideController : MonoBehaviour
{
    private LifetimeController _lifetimeController;
    private Renderer _renderer;

    private void Start()
    {
        _lifetimeController = GetComponent<LifetimeController>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlaneTag") == false)
        {
            return;
        }
        
        _lifetimeController.Activate();
        enabled = false;
    }
}
