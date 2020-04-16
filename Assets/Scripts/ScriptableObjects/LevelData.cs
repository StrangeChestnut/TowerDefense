using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameObject mapPrefab;
        public List<StreamData> streams = new List<StreamData>();

        public Spawner[] mobSpawners;
        public TowerPlace[] towerPlaces;
        public CastleBase castle;
        public void OnSpawnMap(GameObject map)
        {
            castle = map.GetComponentInChildren<CastleBase>();;
            mobSpawners = map.GetComponentsInChildren<Spawner>();
            towerPlaces = map.GetComponentsInChildren<TowerPlace>();

            for (int i = 0; i < mobSpawners.Length; i++)
            {
                mobSpawners[i].SetStream(streams[i]);
            }
        }
    }
}