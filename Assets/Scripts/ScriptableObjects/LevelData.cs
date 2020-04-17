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
    }
}