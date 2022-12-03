using System.Collections.Generic;
using System.Linq;
using JAA.Structure.StaticData;
using UnityEngine;

namespace JAA.Structure.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;

		public void LoadMonsters()
		{
			_monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters").ToDictionary(x => x.monsterTypeID, x => x);
		}

		public MonsterStaticData ForMonster(MonsterTypeID monsterTypeID) => 
			_monsters.TryGetValue(monsterTypeID, out var staticData) ? staticData : null;
	}
}