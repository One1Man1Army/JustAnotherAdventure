using System;
using CodeBase.Enemy;
using UnityEngine;

namespace JAA.Enemies
{
	public class Aggro : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private AgentMoveToPlayer _follow;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;

			SwitchFollow(false);
		}

		private void TriggerEnter(Collider collider)
		{
			SwitchFollow(true);
		}
		
		private void TriggerExit(Collider collider)
		{
			SwitchFollow(false);
		}

		private void SwitchFollow(bool on) => 
			_follow.enabled = on;
	}
}