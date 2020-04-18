using System;
using ScriptableObjects;
using UnityEditor.AI;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameView : MonoBehaviour
    {
        public GameController game;
        
        public Spawner[] mobSpawners;
        public TowerPlace[] towerPlaces;
        public CastleBase castle;
        
        public event Action SpawnMapEvent;
        public event Action NextWaveEvent;

        private void OnEnable()
        {
            game.OnOpen(this);
        }

        public void StartGame()
        {
            SpawnMap();
            BakeMesh();
            NextWave();
        }

        public void NextWave()
        {
            NextWaveEvent?.Invoke();
        }

        private void SpawnMap()
        {
            GameObject map = Instantiate(game.level.mapPrefab, transform.position, Quaternion.identity);   
            
            if (map == null) return;
            castle = map.GetComponentInChildren<CastleBase>();;
            towerPlaces = map.GetComponentsInChildren<TowerPlace>();
            mobSpawners = map.GetComponentsInChildren<Spawner>();

            SpawnMapEvent?.Invoke();
        }
        
        private void BakeMesh()
        {
            NavMeshBuilder.BuildNavMesh();
        }

        public void StopGame()
        {
            
        }

        private void OnDisable()
        {
            game.OnClose(this);
        }
    }
}