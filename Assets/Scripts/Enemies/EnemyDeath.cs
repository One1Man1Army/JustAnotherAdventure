using System;
using UnityEngine;
using UnityEngine.AI;

namespace JAA.Enemies
{
	[RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private EnemyHealth _enemyHealth;
		[SerializeField] private EnemyAnimator _animator;
		[SerializeField] private GameObject _deathFX;
		[SerializeField] private Attack _attack;
		[SerializeField] private Follow _follow;

		public event Action DeathHappened;

		private void Start() => 
			_enemyHealth.HealthChanged += OnHealthChanged;

		private void OnHealthChanged()
		{
			if (_enemyHealth.Current <= 0)
				Die();
		}

		private void Die()
		{
			_enemyHealth.HealthChanged -= OnHealthChanged;
			_animator.PlayDeath();
			SpawnDeathFX();
			_attack.SwitchAttack(false);
			_follow.enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			DeathHappened?.Invoke();
			Invoke(nameof(DelayedDestroy), 3f);
		}

		private void SpawnDeathFX() => 
			Instantiate(_deathFX, transform.position, Quaternion.identity);

		private void OnDestroy() => 
			_enemyHealth.HealthChanged -= OnHealthChanged;

		private void DelayedDestroy()
		{
			Destroy(gameObject);
		}
	}
}