using System;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class WaveSpawner : MonoBehaviour
    {
        public WaveData[] Waves;
        public Spawner[] Spawners;

        private WaveData _wave;
        private bool _isWaitLaunch = true;
        private bool _inSpawnState;

        [SerializeField] private int _waveNumber;
        [SerializeField] private float _timeBeforeWave;
        [SerializeField] private List<Spawner> _activeSpawners;
        [SerializeField] public List<GameObject> enemies = new List<GameObject>();

        public event Action EndLastWaveEvent;

        private void NextWave()
        {
            _inSpawnState = true;
            _wave = Waves[_waveNumber++];
            
            var len = Mathf.Min(_wave.spawnersData.Length, Spawners.Length);
            for (int i = 0; i < len; i++)
            {
                var spawner = Spawners[i];
                if (spawner == null) continue;
                
                spawner.StopSpawnEvent += OnStopSpawner;
                _activeSpawners.Add(spawner);
                spawner.StartSpawn(_wave.spawnersData[i]);
            }
        }

        private void OnStopSpawner(Spawner spawner)
        {
            _activeSpawners.Remove(spawner);
            spawner.StopSpawnEvent -= OnStopSpawner;
            if (_activeSpawners.Count == 0)
                _inSpawnState = false;
        }

        private void Update()
        {
            if (_isWaitLaunch || WaveIsNotFinished) return;
            
            if (IsLastWave)
            {
                _isWaitLaunch = true;
                EndLastWaveEvent?.Invoke();
            }
            else
            {
                _timeBeforeWave -= Time.deltaTime;
                if (_timeBeforeWave <= 0f)
                {
                    NextWave();
                    _timeBeforeWave = Waves[_waveNumber].waveDelay;
                }
            }
        }

        private bool WaveIsNotFinished => _inSpawnState || AnyEnemyAlive;
        private bool AnyEnemyAlive => enemies.Count != 0;
        private bool IsLastWave => _waveNumber + 1 >= Waves.Length;

        public void Launch()
        {
            _isWaitLaunch = false;
        }
        
        public void AddEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
        }
        
        public void RemoveEnemy(GameObject enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
