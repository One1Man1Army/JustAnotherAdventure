using System.Linq;
using JAA.Logic;
using UnityEngine;

namespace JAA.Enemies
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		[SerializeField] private EnemyAnimator _animator;

		public float attackCooldown = 3f;
		public float cleavage = 0.5f;
		public float effectiveDistance = 0.5f;
		public float damage = 10f;
		
		private Transform _heroTransform;
		
		private bool _isAttacking;
		private float _timer;
		private int _layerMask;
		private Collider[] _hits = new Collider[1];
		private bool _attackIsActive;

		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}
		
		public void Construct(Transform heroObjectTransform) => 
			_heroTransform = heroObjectTransform;
		
		public void SwitchAttack(bool on) => 
			_attackIsActive = on;

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				PhysicsDebug.DrawDebug(CastPoint(), cleavage, 1f);
				hit.transform.GetComponent<IHealth>().TakeDamage(damage);
			}
		}

		private void OnAttackEnd()
		{
			_timer = attackCooldown;
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
			var hitsCount = Physics.OverlapSphereNonAlloc(CastPoint(), cleavage, _hits, _layerMask);
			hit = _hits.FirstOrDefault();
			return hitsCount > 0;
		}

		private Vector3 CastPoint() =>
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
			transform.forward * effectiveDistance;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_timer -= Time.deltaTime;
		}

		private bool CanAttack() => 
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() => 
			_timer <= 0;
	}
}
