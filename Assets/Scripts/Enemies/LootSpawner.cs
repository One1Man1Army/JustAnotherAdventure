using JAA.Data;
using JAA.Structure.Factory;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JAA.Enemies
{
	public class LootSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyDeath _enemyDeath;
		private IGameFactory _factory;
		
		private int _lootMin;
		private int _lootMax;

		private void Start()
		{
			_enemyDeath.DeathHappened += SpawnLoot;
		}

		public void Construct(IGameFactory factory)
		{
			_factory = factory;
		}

		public void SetLoot(int min, int max)
		{
			_lootMin = min;
			_lootMax = max;
		}

		private void SpawnLoot()
		{
			var loot = _factory.CreateLoot();
			loot.transform.position = transform.position;

			var lootItem = GenerateLoot();
			loot.Initialize(lootItem);
		}

		private Loot GenerateLoot()
		{
			var lootItem = new Loot();
			lootItem.value = Random.Range(_lootMin, _lootMax);
			return lootItem;
		}
	}
}