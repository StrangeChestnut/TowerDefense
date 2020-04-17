using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    
    private Enemy _target;

    private void Update()
    {
        if (_target == null) return;
        _attack.Fire(_target.Character.Health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target == null)
            _target = other.gameObject.GetComponentInParent<Enemy>();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (_target == null) return;
        if (_target == other.gameObject.GetComponent<Enemy>())
            _target = null;
    }
}
