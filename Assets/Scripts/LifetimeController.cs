using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifetimeController
{
    private float _minLifetime;
    private float _maxLifetime;

    private float _lifetime = 0f;
    private float _age = 0f;
    private bool _isActivated = false;

    private Action ReleaseCube;
    private ColorController _colorController;

    public LifetimeController(
        Action ReleaseCubeAction, 
        ColorController colorController,
        float minLifetime = 2f,
        float maxLifetime = 5f
        )
    {
        ReleaseCube = ReleaseCubeAction;
        _colorController = colorController;
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;

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
