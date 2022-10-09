using System;

namespace JAA.Data
{
	[Serializable]
	public class HeroState
	{
		public float maxHP;
		public float currentHP;

		public void ResetHP() =>
			currentHP = maxHP;
	}
}