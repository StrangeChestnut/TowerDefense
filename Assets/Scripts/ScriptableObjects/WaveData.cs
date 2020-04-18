using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wave", menuName = "WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public SpawnData[] spawnersData;
        public float waveDelay;
        public int number;
    }
}