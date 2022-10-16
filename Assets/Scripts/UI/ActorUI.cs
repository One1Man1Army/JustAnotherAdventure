using System;
using JAA.Hero;
using JAA.Logic;
using UnityEngine;

namespace JAA.UI
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private HpBar _hpBar;
		private IHealth _health;

		public void Construct(IHealth health)
		{
			_health = health;
			_health.HealthChanged += UpdateHpBar;
		}
		private void Start()
		{
			if (_health == null)
				_health = GetComponent<IHealth>();
			if (_health != null)
				_health.HealthChanged += UpdateHpBar;
		}
		private void UpdateHpBar()
		{
			_hpBar.SetValue(_health.Current, _health.Max);
		}

		private void OnDestroy()
		{
			if (_health != null)
				_health.HealthChanged -= UpdateHpBar;
		}
	}
}