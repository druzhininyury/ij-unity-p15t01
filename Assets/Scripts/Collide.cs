using UnityEngine;

public class Collide
{
    private Color _color;
    private Lifetime _lifetime;

    private bool _hasFirstTouch = false;

    public Collide(Color color, Lifetime lifetime)
    {
        _color = color;
        _lifetime = lifetime;
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
        _color.SetRandomColor();
        _lifetime.ActivateLifetimeCountdown();
    }

    public void Reset()
    {
        _hasFirstTouch = false;
    }
}
