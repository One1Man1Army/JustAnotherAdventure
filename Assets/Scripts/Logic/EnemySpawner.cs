using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using JAA.Data;
using JAA.Services;
using JAA.Services.PersistentProgress;
using JAA.Structure.Factory;
using JAA.Structure.StaticData;
using UnityEngine;

namespace JAA.Logic
{
    [RequireComponent(typeof(UniqueID))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        private IGameFactory _factory;
        
        public MonsterTypeID monsterTypeID;
        private string _id;
        [SerializeField] private bool _isSlain;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().ID;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.clearedSpawners.Contains(_id))
                _isSlain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            var monster = _factory.CreateMonster(monsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.DeathHappened += Slay;
        }

        private void Slay()
        {
            _isSlain = true;
            if (_enemyDeath != null)
                _enemyDeath.DeathHappened -= Slay;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
                progress.killData.clearedSpawners.Add(_id);
        }
    }
}
