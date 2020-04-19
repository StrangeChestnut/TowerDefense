using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class CastleBase : MonoBehaviour
    {
        [SerializeField] private Health _health;
        public Health Health => _health;
        public event Action CastleDestroyEvent;

        private void OnEnable()
        {
            _health.DieEvent += OnDie;
        }

        private void OnDie()
        {
            CastleDestroyEvent?.Invoke();
        }

        private void OnDisable()
        {
            _health.DieEvent -= OnDie;
        }
    }
}
