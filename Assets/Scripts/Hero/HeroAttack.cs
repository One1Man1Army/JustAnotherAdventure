using System;
using JAA.Data;
using JAA.Logic;
using JAA.Services;
using JAA.Services.Input;
using JAA.Services.PersistentProgress;
using UnityEngine;

namespace JAA.Hero
{
	[RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private HeroAnimator _heroAnimator;
		private IInputService _input;
		private HeroStats _stats;

		private static int _layerMask;
		private Collider[] _hits = new Collider[3];

		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
			_layerMask = 1 << LayerMask.NameToLayer("Hittable");
		}

		private void Update()
		{
			if (_input.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
				_heroAnimator.PlayAttack();
		}

		public void OnAttack()
		{
			for (int i = 0; i < Hit(); i++)
			{
				_hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(_stats.damage);
			}
		}
		
		public void LoadProgress(PlayerProgress progress) => 
			_stats = progress.heroStats;

		private int Hit() => 
			Physics.OverlapSphereNonAlloc(CastPoint() + transform.forward, _stats.damageRadius, _hits, _layerMask);

		private Vector3 CastPoint() =>
			new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
	}
}