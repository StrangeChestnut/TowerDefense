using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Walking _walking;
    public Walking Walking => _walking;
    public Health Health => _health;

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.DieEvent += OnDead;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.DieEvent -= OnDead;
        }
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}
