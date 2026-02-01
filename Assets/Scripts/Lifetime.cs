using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lifetime
{
    private float _minLifetime;
    private float _maxLifetime;

    private float _lifetime = 0f;
    private float _age = 0f;
    private bool _isActivated = false;

    private ICoroutineRunner _coroutineRunner;
    private Action ReleaseCube;
    private Color _color;

    public Lifetime(
        ICoroutineRunner coroutineRunner,
        Action ReleaseCubeAction, 
        Color color,
        float minLifetime = 2f,
        float maxLifetime = 5f
        )
    {
        _coroutineRunner = coroutineRunner;
        ReleaseCube = ReleaseCubeAction;
        _color = color;
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;

        Reset();
    }

    public void ActivateLifetimeCountdown()
    {
        if (_isActivated)
        {
            return;
        }
        
        _isActivated = true;
        _coroutineRunner.StartCoroutine(ProcessUpdate());
    }

    public IEnumerator ProcessUpdate()
    {
        while (true)
        {
            _age += Time.deltaTime;

            if (_age >= _lifetime)
            {
                ReleaseCube();
                break;
            }
            
            _color.LerpToBlack(_age / _lifetime);
            yield return null;
        }
    }

    public void Reset()
    {
        _lifetime = _minLifetime + Random.value * (_maxLifetime - _minLifetime);
        _age = 0;
        _isActivated = false;
    }
}
