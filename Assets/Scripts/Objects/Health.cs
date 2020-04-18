using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
		[SerializeField] private float _defaultHealth;
		public float DefaultHealth => _defaultHealth;

		public float HealthPoints { get; private set; }

    	public event Action DieEvent;
    	public event Action<float> ChangeHealthEvent;

        private void OnEnable()
        {
	        HealthPoints = _defaultHealth;
    	}

    	public void Damage(float value)
    	{
    		HealthPoints = Mathf.Max(0f, HealthPoints - value);

    		ChangeHealthEvent?.Invoke(HealthPoints);

    		if (HealthPoints > 0f) return;

    		DieEvent?.Invoke();
    	}
	
}
