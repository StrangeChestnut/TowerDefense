using System;
using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    public class Map : MonoBehaviour
    {
        public Spawner[] mobSpawners;
        public TowerPlace[] towerPlaces;
        public CastleBase castle;
        private void OnEnable()
        {
            castle = gameObject.GetComponentInChildren<CastleBase>();;
            mobSpawners = gameObject.GetComponentsInChildren<Spawner>();
            towerPlaces = gameObject.GetComponentsInChildren<TowerPlace>();
        }
    }
}