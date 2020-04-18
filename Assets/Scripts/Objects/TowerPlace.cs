using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TowerPlace : MonoBehaviour
    {
        [SerializeField] private GameObject _tower;

        private GameObject _current;
        private float _height;

        private void Start()
        {
            var collider = gameObject.GetComponent<BoxCollider>();
            if (collider == null) return;
            _height = gameObject.GetComponent<BoxCollider>().size.y;
        }

        private void OnMouseDown()
        {
            if (_current == null)
            {
                _current = Instantiate(_tower, transform.parent.position + Vector3.up * _height, Quaternion.identity, transform.parent.parent);
            }
            else
            {
                Destroy(_current);
                _current = null;
            }
        }
    }
}
