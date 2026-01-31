using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float _scale = 0.5f;
    
    public event Action<CubeController> OnCubeRemove;

    public void Reset()
    {
        ResetTransform();
    }

    private void ResetTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = _scale * Vector3.one;
    }
}
