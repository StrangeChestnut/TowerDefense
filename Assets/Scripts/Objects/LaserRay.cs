using UnityEngine;

namespace Objects
{
    public class LaserRay : MonoBehaviour
    {
        public float damagePerSecond = 5f;

        private void OnTriggerStay(Collider other)
        {
            var enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.Character.Health.Damage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
