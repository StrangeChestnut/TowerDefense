using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private StreamData _stream;
        private IEnumerator _spawner;
        private WaveData _wave;
        
        private Dictionary<int, int> _indices = new Dictionary<int, int>();
        
        public void SetStream(StreamData stream)
        {
            _stream = stream;
            for (int i = 0; i < _stream.waves.Count; i++)
            {
                _indices.Add(_stream.waves[i].number, i);
            } 
        }

        public void NewWave(int waveNumber)
        {
            _wave = _stream.waves[_indices[waveNumber]];
            _spawner = SpawnCoroutine();
            StartCoroutine(_spawner);
        }

        private void Spawn()
        {
            var mob = Instantiate(_wave.mobPrefab, transform.position, Quaternion.identity, transform.parent);
        }

        IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_wave.waveDelay);
            for (int i = 0; i < _wave.mobCount; i ++)
            {
                Spawn();
                yield return new WaitForSeconds(_wave.spawnDelay);
            }
        }
    }
}