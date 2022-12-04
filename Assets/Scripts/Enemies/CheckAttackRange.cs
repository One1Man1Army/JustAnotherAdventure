using System;
using JAA.Enemies;
using UnityEngine;

namespace JAA.Enemies
{
	[RequireComponent(typeof(Attack))]
	public class CheckAttackRange : MonoBehaviour
	{
		[SerializeField] private Attack _attack;
		[SerializeField] private TriggerObserver _triggerObserver;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
			
			_attack.SwitchAttack(false);
		}

		private void TriggerExit(Collider obj)
		{
			_attack.SwitchAttack(false);
		}

		private void TriggerEnter(Collider obj)
		{
			_attack.SwitchAttack(true);
		}
	}
}