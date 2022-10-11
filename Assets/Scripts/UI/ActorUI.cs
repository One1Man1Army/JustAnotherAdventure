using System;
using JAA.Hero;
using UnityEngine;

namespace JAA.UI
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private HpBar _hpBar;
		private HeroHealth _heroHealth;

		public void Construct(HeroHealth heroHealth)
		{
			_heroHealth = heroHealth;
			_heroHealth.HealthChanged += UpdateHpBar;
		}
		private void UpdateHpBar() => 
			_hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);

		private void OnDestroy() => 
			_heroHealth.HealthChanged -= UpdateHpBar;
	}
}