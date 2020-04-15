using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wave", menuName = "WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public List<StreamData> streams = new List<StreamData>();
        public float waveDelay;
    }
}