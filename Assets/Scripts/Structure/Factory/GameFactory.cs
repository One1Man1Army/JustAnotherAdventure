using System.Collections.Generic;
using JAA.Enemies;
using JAA.Logic;
using JAA.Services.PersistentProgress;
using JAA.Structure.AssetManagement;
using JAA.Structure.Services.StaticData;
using JAA.Structure.StaticData;
using JAA.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace JAA.Structure.Factory
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		public GameFactory(IAssetProvider assets, IStaticDataService staticData)
		{
			_assets = assets;
			_staticData = staticData;
		}
		public GameObject CreateHero(GameObject initialPoint)
		{
			HeroObject = InstantiateRegistered(AssetPath.HeroPath, initialPoint.transform.position);
			return HeroObject;
		}

		private GameObject HeroObject { get;  set; }

		public GameObject CreateHud() => 
			InstantiateRegistered(AssetPath.HudPath);

		public GameObject CreateLoot() => 
			InstantiateRegistered(AssetPath.LootPath);

		public GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent)
		{
			var monsterData = _staticData.ForMonster(monsterTypeID);
			var monster = Object.Instantiate(monsterData.prefab, parent.position, Quaternion.identity, parent);

			var health = monster.GetComponent<IHealth>();
			health.Max = monsterData.hp;
			health.Current = monsterData.hp;
			
			monster.GetComponent<ActorUI>().Construct(health);
			monster.GetComponent<AgentMoveToPlayer>().Construct(HeroObject.transform);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.moveSpeed;
			monster.GetComponent<RotateToHero>()?.Construct(HeroObject.transform);
			
			var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			lootSpawner.Construct(this);
			lootSpawner.SetLoot(monsterData.minLoot, monsterData.maxLoot);

			var attack = monster.GetComponent<Attack>();
			attack.Construct(HeroObject.transform);
			attack.damage = monsterData.damage;
			attack.cleavage = monsterData.cleavage;
			attack.effectiveDistance = monsterData.effectiveDistance;
			attack.attackCooldown = monsterData.attackCooldown;

			return monster;
		}

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
		{
			var gameObject = _assets.Instantiate(prefabPath, position);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			var gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (var reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(reader);
		}

		public void Register(ISavedProgressReader reader)
		{
			if (reader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
				
			ProgressReaders.Add(reader);
		}
	}
}