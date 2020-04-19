using System;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    enum EnemyState
    {
        Walk,
        Stay
    }
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Character _character;
        public Character Character => _character;
    
        public GameController game;
        
        [SerializeField] private float _defaultDestinationTime = 5f;
        [SerializeField] private float _defaultSpeed = 5f;
        [SerializeField] private float _damageValue = 5f;
        [SerializeField] private float _attackDistance = 5f;
        [SerializeField] private int _score = 50;

        private EnemyState _state;
        private float _destinationTimer;
        private CastleBase _castle;

        private void OnEnable()
        {
            _state = EnemyState.Walk;
            _castle = game.castle;
            _destinationTimer = 0f;
            _character.Walking.Speed = _defaultSpeed;
            game.spawner.AddEnemy(gameObject);
        }

        private void OnDisable()
        {
            game.spawner.RemoveEnemy(gameObject);
            game.AddScore(_score);
        }

        private void Update()
        {
            if (_castle != null)
            {
                _destinationTimer -= Time.deltaTime;
                if (_destinationTimer <= 0f)
                {
                    Character.Walking.MovePosition(_castle.transform.position);
                    _destinationTimer = _defaultDestinationTime;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _castle.gameObject)
            {
                _castle.Health.Damage(_damageValue);
                Destroy(gameObject);
            }
        }
    }
}
