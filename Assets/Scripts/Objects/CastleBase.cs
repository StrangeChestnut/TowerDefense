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
    }
}
