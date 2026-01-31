using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LifetimeController
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private float _lifetime = 0f;
    private float _age = 0f;
    private bool _isActivated = false;

    private Action ReleaseCube;
    private ColorController _colorController;

    public LifetimeController(Action ReleaseCubeAction, ColorController colorController)
    {
        ReleaseCube = ReleaseCubeAction;
        _colorController = colorController;

        Reset();
    }

    public void ActivateLifetimeCountdown()
    {
        _isActivated = true;
    }

    public void ProcessUpdate(float deltaTime)
    {
        if (_isActivated == false)
        {
            return;
        }
        
        _age += deltaTime;

        if (_age >= _lifetime)
        {
            ReleaseCube();
        }
        
        _colorController.LerpToBlack(_age / _lifetime);
    }

    public void Reset()
    {
        _lifetime = _minLifetime + Random.value * (_maxLifetime - _minLifetime);
        _age = 0;
        _isActivated = false;
    }
}
