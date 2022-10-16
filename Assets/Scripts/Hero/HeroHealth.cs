using System;
using JAA.Data;
using JAA.Logic;
using JAA.Services.PersistentProgress;
using UnityEngine;

namespace JAA.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
	{
		[SerializeField] private HeroAnimator _animator;
		private HeroState _heroState;

		public float Max
		{
			get => _heroState.maxHP;
			set => _heroState.maxHP = value;
		}

		public event Action HealthChanged;

		public float Current
		{
			get => _heroState.currentHP;
			set
			{
				if (_heroState.currentHP != value)
				{
					_heroState.currentHP = value;
					HealthChanged?.Invoke();
				}
			}
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_heroState = progress.heroState;
			HealthChanged?.Invoke();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.heroState.currentHP = Current;
			progress.heroState.maxHP = Max;
		}

		public void TakeDamage(float damage)
		{
			if (Current <= 0)
				return;

			Current -= damage;
			_animator.PlayHit();
		}
	}
}
