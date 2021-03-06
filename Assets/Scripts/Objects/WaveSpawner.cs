﻿using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class WaveSpawner : MonoBehaviour
    {
        public WaveData[] Waves;
        public Spawner[] Spawners;

        private bool _inSpawnState;
        private Coroutine _timer;

        [SerializeField] private int _waveNumber;
        [SerializeField] private int _timeBeforeWave;
        [SerializeField] private List<Spawner> _activeSpawners;
        [SerializeField] public List<GameObject> enemies = new List<GameObject>();

        public event Action EndLastWaveEvent;
        public event Action<int> UpdateTimerEvent;
        public event Action<int> UpdateWaveEvent;


        private bool WaveIsNotFinished => _inSpawnState || AnyEnemyAlive;
        private bool AnyEnemyAlive => enemies.Count != 0;
        private bool IsLastWave => _waveNumber + 1 >= Waves.Length;

        public void Launch()
        {
            _waveNumber = 0;
            StartTimerBeforeWave(_waveNumber);
        }
        
        public void Stop()
        {
            foreach (var enemy in enemies.ToArray())
                Destroy(enemy);
            enemies.Clear();

            foreach (var spawner in _activeSpawners)
                spawner.StopSpawn();
            _activeSpawners.Clear();
            
            if (_timer != null)
                StopCoroutine(_timer);
        }

        private void StartTimerBeforeWave(int waveNumber)
        {
            _timer = StartCoroutine(WaveTimer(waveNumber));
        }

        private IEnumerator WaveTimer(int waveNumber)
        {
            if (waveNumber >= Waves.Length) yield break;
            var wave = Waves[waveNumber];
            if (wave == null) yield break;
            
            _timeBeforeWave = wave.waveDelay;
            UpdateTimerEvent?.Invoke(_timeBeforeWave);
            while (_timeBeforeWave > 0)
            {
                yield return new WaitForSeconds(1);
                UpdateTimerEvent?.Invoke(--_timeBeforeWave);
            }
            StartWave(wave);
            UpdateWaveEvent?.Invoke(waveNumber);
        }
        
        private void StartWave(WaveData wave)
        {
            _inSpawnState = true;
            var len = Mathf.Min(wave.spawnersData.Length, Spawners.Length);
            for (int i = 0; i < len; i++)
            {
                var spawner = Spawners[i];
                if (spawner == null) continue;
                
                spawner.StopSpawnEvent += OnStopSpawner;
                _activeSpawners.Add(spawner);
                spawner.StartSpawn(wave.spawnersData[i]);
            }
        }

        public void AddEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
        }
        
        public void RemoveEnemy(GameObject enemy)
        {
            enemies.Remove(enemy);
            if (WaveIsNotFinished) return;
            if (IsLastWave)
                EndLastWaveEvent?.Invoke();
            else
                StartTimerBeforeWave(++_waveNumber);
        }
        
        private void OnStopSpawner(Spawner spawner)
        {
            _activeSpawners.Remove(spawner);
            spawner.StopSpawnEvent -= OnStopSpawner;
            if (_activeSpawners.Count == 0)
                _inSpawnState = false;
        }
    }
}
