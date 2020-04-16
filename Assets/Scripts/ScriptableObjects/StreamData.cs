﻿using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Stream", menuName = "StreamData", order = 0)]
    public class StreamData : ScriptableObject
    { 
        public GameObject mobPrefab;
        public int mobCount;
        public float spawnDelay;
    }
}