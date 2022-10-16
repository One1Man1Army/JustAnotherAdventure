using System;
using System.Collections;
using System.Collections.Generic;
using JAA.Data;
using JAA.Services.PersistentProgress;
using UnityEngine;

namespace JAA.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private GameObject _deathFX;
        private bool _isDead;

        private void Start() => 
            _heroHealth.HealthChanged += OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _heroHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            _animator.PlayDeath();
            Instantiate(_deathFX, transform.position, Quaternion.identity);
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= OnHealthChanged;
    }
}
