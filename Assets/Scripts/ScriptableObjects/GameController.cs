﻿using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
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
        public CastleBase Castle => _gameView.castle;
        public WaveSpawner WaveSpawner => _gameView.spawner;

        public void OnOpen(GameView gameView)
        {
            _gameView = gameView;
            gameView.WonEvent += OnWonGame;
            gameView.StartGame();
        }

        private void OnWonGame()
        {
            _gameView.StopGame();
        }

        public void OnClose(GameView gameView)
        {
            _gameView = null;
        }
    }
}