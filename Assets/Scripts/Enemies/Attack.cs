using System;
using System.Linq;
using JAA.Hero;
using JAA.Services;
using JAA.Structure.Factory;
using UnityEngine;
using UnityEngine.Serialization;

namespace JAA.Enemies
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		[SerializeField] private EnemyAnimator _animator;

		[SerializeField] private float _attackCooldown = 3f;
		[SerializeField] private float _cleavage = 0.5f;
		[SerializeField] private float _effectiveDistance = 0.5f;
		[SerializeField] private float _damage = 10f;

		private IGameFactory _factory;
		private Transform _heroTransform;
		
		private bool _isAttacking;
		private float _timer;
		private int _layerMask;
		private Collider[] _hits = new Collider[1];
		private bool _attackIsActive;

		private void Awake()
		{
			_factory = AllServices.Container.Single<IGameFactory>();
			_factory.HeroCreated += OnHeroCreated;
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}
		
		public void SwitchAttack(bool on) => 
			_attackIsActive = on;

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				PhysicsDebug.DrawDebug(CastPoint(), _cleavage, 1f);
				hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
			}
		}

		private void OnAttackEnd()
		{
			_timer = _attackCooldown;
			_isAttacking = false;
		}
		
		private void StartAttack()
		{
			transform.LookAt(_heroTransform);
			_animator.PlayAttack();
			_isAttacking = true;
		}
		
		private bool Hit(out Collider hit)
		{
			var hitsCount = Physics.OverlapSphereNonAlloc(CastPoint(), _cleavage, _hits, _layerMask);
			hit = _hits.FirstOrDefault();
			return hitsCount > 0;
		}

		private Vector3 CastPoint() =>
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
			transform.forward * _effectiveDistance;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_timer -= Time.deltaTime;
		}

		private bool CanAttack() => 
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() => 
			_timer <= 0;

		private void OnHeroCreated() =>
			_heroTransform = _factory.HeroObject.transform;
	}
}
