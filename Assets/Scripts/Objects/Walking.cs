using UnityEngine;
using UnityEngine.AI;

namespace Objects
{
    public class Walking : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Rigidbody _rigidbody;
    
        public float Speed
        {
            set
            {
                if (_agent != null)
                {
                    _agent.speed = value;
                }
            }
        }
        
        private void Update()
        {
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, transform.rotation, Time.deltaTime);
        }

        public void MovePosition(Vector3 position)
        {
            _agent.SetDestination(position);
        }
    }
}

