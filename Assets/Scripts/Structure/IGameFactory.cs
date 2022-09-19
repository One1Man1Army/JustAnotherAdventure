using UnityEngine;

namespace JAA.Structure
{
	public interface IGameFactory
	{
		GameObject CreateHero(GameObject initialPoint);
		void CreateHud();
	}
}