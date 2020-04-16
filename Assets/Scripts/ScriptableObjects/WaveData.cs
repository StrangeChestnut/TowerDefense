using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wave", menuName = "WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public GameObject mobPrefab;
        public int mobCount;
        public float spawnDelay;
        public float waveDelay;
        public int number;
    }
}