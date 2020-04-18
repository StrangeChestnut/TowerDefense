using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEditor.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameProxy", menuName = "GameProxy", order = 0)]
    public class GameController : ScriptableObject
    {
        private GameView _gameView;
        public readonly List<GameObject> enemies = new List<GameObject>();
        [SerializeField] private List<Spawner> _activeSpawners = new List<Spawner>();
        [SerializeField] private int _wave;
            
        public LevelData level;
        public CastleBase Castle => _gameView.castle;
        public void OnOpen(GameView gameView)
        {
            _wave = 0;
            _gameView = gameView;
            gameView.SpawnMapEvent += OnSpawnMap;
            gameView.NextWaveEvent += TryNextWave;
            gameView.StartGame();
        }

        private void OnSpawnMap()
        {
            if (_gameView.mobSpawners == null) return;
            for (int i = 0; i < _gameView.mobSpawners.Length; i++)
            {
                var spawner = _gameView.mobSpawners[i];
                spawner.SetStream(level.streams[i]);
                spawner.StopSpawnEvent += OnStopSpawn;
            }
        }

        private void OnStopSpawn(Spawner spawner)
        {
            _activeSpawners.Remove(spawner);
        }

        public void AddEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
        }
        
        public void RemoveEnemy(GameObject enemy)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0 && _activeSpawners.Count == 0)
                _gameView.NextWave();
        }

        private void TryNextWave()
        {
            foreach (var spawner in _gameView.mobSpawners)
                if (spawner.HasWave(_wave))
                    _activeSpawners.Add(spawner);

            if (_activeSpawners.Count == 0)
                _gameView.StopGame();
            else
            {
                foreach (var spawner in _gameView.mobSpawners)
                    spawner.NewWave(_wave);
                _wave++;
            }
        }

        public void OnClose(GameView gameView)
        {
            _activeSpawners.Clear();
            gameView.SpawnMapEvent -= OnSpawnMap;
            gameView.NextWaveEvent -= TryNextWave;
            _gameView = null;
        }
    }
}