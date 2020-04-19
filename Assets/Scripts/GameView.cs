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
    public event Action LoseEvent;
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
        if (castle != null)
            castle.CastleDestroyEvent += OnCastleDestroy;
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
    
    private void OnCastleDestroy()
    {
        LoseEvent?.Invoke();
    }

    public void StopGame()
    {
        if (spawner != null)
        {
            spawner.Stop();
            spawner.EndLastWaveEvent -= OnEndLastWave;
        }
        if (castle != null)
            castle.CastleDestroyEvent -= OnCastleDestroy;
    }

    private void OnDisable()
    {
        game.OnClose(this);
    }
}