using System;
using UnityEngine;

namespace Objects
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private float _defaultHealth;
		public float DefaultHealth => _defaultHealth;

		private float _health;

		public event Action DieEvent;
		public event Action<float> ChangeHealthEvent;

		private void OnEnable()
		{
			_health = _defaultHealth;
		}

		public void Damage(float value)
		{
			_health = Mathf.Max(0f, _health - value);

			ChangeHealthEvent?.Invoke(_health);

			if (_health > 0f) return;

			DieEvent?.Invoke();
		}
	}
}
