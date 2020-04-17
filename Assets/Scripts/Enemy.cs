using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameController _game;
    [SerializeField] private Character _character;
    
    public float DefaultDestinationTime = 5f;
    public float AttackDestinationTime = 1f;
    public float DefaultSpeed = 5f;
    public float AttackSpeed = 0f;
    public float AttackDistance = 5f;

    private float _destinationTimer;

    private CastleBase _castle;

    private void OnEnable()
    {
        _castle = _game.level.Map.castle;
        _destinationTimer = 0f;
        _character.Walking.Speed = DefaultSpeed;
        _character.Walking.StopDistance(AttackDistance);
    }

    private void Update()
    {
        if (_castle != null)
        {
            var vector = _castle.transform.position - transform.position;
            var distance = vector.magnitude;
            var destinationTimer = 10f;
            if (distance < AttackDistance)
            {
                _character.Walking.Speed = AttackDistance;
                destinationTimer = AttackDestinationTime;
            }
            else if (distance >= AttackDistance)
            {
                _character.Walking.Speed = DefaultSpeed;
                destinationTimer = DefaultDestinationTime;
            }
				
            _destinationTimer -= Time.deltaTime;
            if (_destinationTimer > 0f) return;
            _character.Walking.MovePosition(_castle.transform.position);
            _destinationTimer = destinationTimer;
        }
    }
}
