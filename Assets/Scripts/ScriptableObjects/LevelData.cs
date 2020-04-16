using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameObject mapPrefab;
        public List<StreamData> streams = new List<StreamData>();

        public Map Map { get; private set; }

        public void OnSpawnMap(GameObject mapObject)
        {
            Map = mapObject.GetComponent<Map>();
            if (Map == null) return;
            
            for (int i = 0; i < Map.mobSpawners.Length; i++)
            {
                Map.mobSpawners[i].SetStream(streams[i]);
            }
        }
    }
}