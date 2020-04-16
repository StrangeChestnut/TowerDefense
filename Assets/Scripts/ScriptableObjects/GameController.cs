using System;
using DefaultNamespace;
using UnityEditor.AI;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameProxy", menuName = "GameProxy", order = 0)]
    public class GameController : ScriptableObject
    {
        public LevelData level;
        public int wave = 0;

        public void OnOpen(GameView gameView)
        {
            wave = 0;
            gameView.SpawnMapEvent += level.OnSpawnMap;
            gameView.StartGameEvent += BakeMesh;
            gameView.StartGameEvent += NextWave;
            gameView.StartGame();
        }

        private void BakeMesh()
        {
            NavMeshBuilder.BuildNavMesh();
        }

        public void NextWave()
        {
            foreach (var spawner in level.mobSpawners)
            {
                spawner.NewWave(wave++);
            }
        }

        public void OnClose(GameView gameView)
        {
            gameView.StopGame();
            gameView.StartGameEvent -= NextWave;
            gameView.StartGameEvent -= BakeMesh;
            gameView.SpawnMapEvent -= level.OnSpawnMap;
        }
    }
}