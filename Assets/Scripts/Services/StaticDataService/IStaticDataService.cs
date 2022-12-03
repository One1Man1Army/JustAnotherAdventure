using JAA.Services;
using JAA.Structure.StaticData;

namespace JAA.Structure.Services.StaticData
{
	public interface IStaticDataService : IService
	{
		void LoadMonsters();
		MonsterStaticData ForMonster(MonsterTypeID monsterTypeID);
	}
}