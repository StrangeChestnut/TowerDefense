using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    private void OnEnable()
    {
        _health = gameObject.GetComponentInParent<Health>();
        if (_health != null)
        {
            _health.ChangeHealthEvent += OnChangeHealth;
            _slider.maxValue = _health.DefaultHealth;
            _slider.value = _health.DefaultHealth;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.ChangeHealthEvent -= OnChangeHealth;
        }
    }

    private void OnChangeHealth(float value)
    {
        _slider.value = value;
    }
}
