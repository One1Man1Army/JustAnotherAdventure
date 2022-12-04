using UnityEngine;

namespace JAA.Structure.StaticData
{
	[CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
	public class MonsterStaticData : ScriptableObject
	{
		public MonsterTypeID monsterTypeID;

		public int minLoot;
		public int maxLoot;
		
		[Range(1, 100)]
		public int hp;
		
		[Range(1f, 30f)]
		public float damage;
		
		[Range(0.5f, 1f)]
		public float effectiveDistance;
		
		[Range(0.5f, 1f)]
		public float cleavage;

		[Range(1f, 20f)]
		public float moveSpeed;

		[Range(0.3f, 10f)] 
		public float attackCooldown;
		
		public GameObject prefab;
	}
}