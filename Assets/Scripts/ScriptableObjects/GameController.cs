using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEditor.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameProxy", menuName = "GameProxy", order = 0)]
    public class GameController : ScriptableObject
    {
        private GameView _gameView;
        public GameObject levelPrefab;

        public WaveSpawner spawner;
        public TowerPlace[] towerPlaces;
        public CastleBase castle;

        private int _score;
        
        public event Action WonEvent;
        public event Action LoseEvent;
        
        public event Action<int> UpdateScoreEvent;
        public event Action<int> UpdateWaveEvent;
        public event Action<int> UpdateTimerEvent;
        public event Action<float> ChangeCastleHealthEvent;

        public void StartGame(GameView gameView)
        {
            _score = 0;
            UpdateScoreEvent?.Invoke(0);
            SpawnMap();
            BakeMesh();
            StartWaves();
        }
        
        private void SpawnMap()
        {
            GameObject level = Instantiate(levelPrefab);   
            
            if (level == null) return;
            castle = level.GetComponentInChildren<CastleBase>();;
            if (castle != null)
            {
                castle.CastleDestroyEvent += OnCastleDestroy;
                castle.ChangeCastleHealthEvent += ChangeCastleHealthEvent;
                ChangeCastleHealthEvent?.Invoke(castle.Health.DefaultHealth);
            }

            towerPlaces = level.GetComponentsInChildren<TowerPlace>();
            spawner = level.GetComponent<WaveSpawner>();
        }

        private void BakeMesh()
        {
            NavMeshBuilder.BuildNavMesh();
        }

        private void StartWaves()
        {
            if (spawner != null)
            {
                spawner.EndLastWaveEvent += OnEndLastWave;
                spawner.UpdateTimerEvent += UpdateTimerEvent;
                spawner.UpdateWaveEvent += UpdateWaveEvent;
                spawner.Launch();
            }
        }

        private void OnEndLastWave()
        {
            StopGame();
            WonEvent?.Invoke();
        }
    
        private void OnCastleDestroy()
        {
            StopGame();
            LoseEvent?.Invoke();
        }

        private void StopGame()
        {
            if (spawner != null)
            {
                spawner.Stop();
                spawner.EndLastWaveEvent -= OnEndLastWave;
                spawner.UpdateTimerEvent -= UpdateTimerEvent;
                spawner.UpdateWaveEvent -= UpdateWaveEvent;
            }

            if (castle != null)
            {
                castle.CastleDestroyEvent -= OnCastleDestroy;
                castle.ChangeCastleHealthEvent -= ChangeCastleHealthEvent;
            }
        }

        public void AddScore(int value)
        {
            _score += value;
            UpdateScoreEvent?.Invoke(_score);
        }
    }
}