using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameObject mapPrefab;
        public List<WaveData> waves = new List<WaveData>();
    }
}