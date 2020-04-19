using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class Tower : MonoBehaviour
    {
        public GameController game;
        public Weapon weapon;
        public float attackDistance = 5f;
        private Transform _target;

        private void Update()
        {
            if (_target != null)
            {
                if (InSight(_target.position))
                {
                    weapon.Rotate(_target.position);
                    weapon.Fire();
                }
                else
                    UpdateTarget();
            }
            else
            {
                weapon.StopFire();
                UpdateTarget();
            }
        }

        private bool InSight(Vector3 target)
        {
            var vector = target - transform.position;
            vector = new Vector2(vector.x, vector.z);
            return vector.magnitude <= attackDistance;
        }

        private void UpdateTarget()
        {
            var shortestDist = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (var enemy in game.spawner.enemies)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < shortestDist)
                {
                    shortestDist = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && InSight(nearestEnemy.transform.position))
                _target = nearestEnemy.transform;
            else
                _target = null;
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}
