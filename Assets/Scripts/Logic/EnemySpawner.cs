using System;
using System.Collections;
using System.Collections.Generic;
using JAA.Data;
using JAA.Services.PersistentProgress;
using JAA.Structure.StaticData;
using UnityEngine;

namespace JAA.Logic
{
    [RequireComponent(typeof(UniqueID))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeID monsterTypeID;
        private string _id;
        [SerializeField] private bool _isSlain;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().ID;
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
           
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
                progress.killData.clearedSpawners.Add(_id);
        }
    }
}
