using UnityEngine;

public class CollideController
{
    private ColorController _colorController;
    private LifetimeController _lifetimeController;

    private bool _hasFirstTouch = false;

    public CollideController(ColorController colorController, LifetimeController lifetimeController)
    {
        _colorController = colorController;
        _lifetimeController = lifetimeController;
    }
    
    public void ProcessCollision(Collision other)
    {
        if (_hasFirstTouch)
        {
            return;
        }
        
        if (other.gameObject.TryGetComponent(typeof(PlanesMarker), out Component _) == false)
        {
            return;
        }

        _hasFirstTouch = true;
        _colorController.SetRandomColor();
        _lifetimeController.ActivateLifetimeCountdown();
    }

    public void Reset()
    {
        _hasFirstTouch = false;
    }
}
