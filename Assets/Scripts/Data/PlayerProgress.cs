using System;
using UnityEngine.Serialization;

namespace JAA.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public WorldData worldData;
		public HeroState heroState;
		public HeroStats heroStats;

		public PlayerProgress(string initialLevel)
		{
			worldData = new WorldData(initialLevel);
			heroState = new HeroState();
			heroStats = new HeroStats();
		}
	}
}