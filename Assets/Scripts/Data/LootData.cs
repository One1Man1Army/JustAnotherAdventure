using System;

namespace JAA.Data
{
	[Serializable]
	public class LootData
	{
		public int collected;
		
		public void Collect(Loot loot)
		{
			collected += loot.value;
		}
	}
}