using System;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameController _game;

        public event Action<GameObject> SpawnMapEvent;
        public event Action StartGameEvent;

        private void OnEnable()
        {
            _game.OnOpen(this);
        }
        
        public void StartGame()
        {
            SpawnMap();
            StartGameEvent?.Invoke();
        }

        private void SpawnMap()
        {
            GameObject map = Instantiate(_game.level.mapPrefab, transform.position, Quaternion.identity);    
            SpawnMapEvent?.Invoke(map);
        }

        public void StopGame()
        {
            
        }
        
        private void OnDisable()
        {
            _game.OnClose(this);
        }

        
    }
}