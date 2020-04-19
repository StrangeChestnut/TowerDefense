using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnData _stream;
        private Coroutine _spawner;
        
        public event Action<Spawner> StopSpawnEvent;
        
        public void StartSpawn(SpawnData stream)
        {
            _stream = stream;
            _spawner = StartCoroutine(SpawnCoroutine());
        }

        private void Spawn()
        {
            Instantiate(_stream.mobPrefab, transform.position, Quaternion.identity, transform.parent);
        }

        IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < _stream.mobCount; i ++)
            {
                yield return new WaitForSeconds(_stream.spawnDelay);
                Spawn();
            }
            StopSpawnEvent?.Invoke(this);
        }

        public void StopSpawn()
        {
            StopCoroutine(_spawner);
            _spawner = null;
        }
    }
}