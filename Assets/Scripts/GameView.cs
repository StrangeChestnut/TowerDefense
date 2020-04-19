using System;
using Objects;
using ScriptableObjects;
using UnityEditor.AI;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public GameController game;
        
    public WaveSpawner spawner;
    public TowerPlace[] towerPlaces;
    public CastleBase castle;

    public event Action WonEvent;
    private void OnEnable()
    {
        game.OnOpen(this);
    }

    public void StartGame()
    {
        SpawnMap();
        BakeMesh();
        StartWaves();
    }
        
    private void SpawnMap()
    {
        GameObject level = Instantiate(game.levelPrefab, transform.position, Quaternion.identity);   
            
        if (level == null) return;
        castle = level.GetComponentInChildren<CastleBase>();;
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
            spawner.Launch();
        }
    }

    private void OnEndLastWave()
    {
        WonEvent?.Invoke();
    }

    public void StopGame()
    {
        if (spawner != null)
        {
            spawner.EndLastWaveEvent -= OnEndLastWave;
        }
    }

    private void OnDisable()
    {
        game.OnClose(this);
    }
}