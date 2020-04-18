using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class LaserWeapon : Weapon
    {
        public GameObject laserRay;
        public Transform shotPoint;
        
        private void OnEnable()
        {
            RotateAction += ChangeLength;
            StopFire();
        }

        public override void Fire()
        {
            if (laserRay == null) return;
            laserRay.SetActive(true);
        }
        
        public override void StopFire()
        {
            if (laserRay == null) return;
            laserRay.SetActive(false);
        }

        private void ChangeLength(Vector3 target)
        {
            if (laserRay == null) return;
            var vector = target - shotPoint.position;
            var scale = laserRay.transform.localScale;
            scale.y = vector.magnitude;
            laserRay.transform.localScale = scale;
        }
    }

    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Fire();
        public abstract void StopFire();
        
        public event Action<Vector3> RotateAction;

        public void Rotate(Vector3 target)
        {
            var vector = target - transform.position;
            transform.rotation = Quaternion.LookRotation(vector);
            RotateAction?.Invoke(target);
        }
    }
}