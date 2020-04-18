using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "SpawnData", order = 0)]
    public class SpawnData : ScriptableObject
    {
        public GameObject mobPrefab;
        public int mobCount;
        public float spawnDelay;
    }
}