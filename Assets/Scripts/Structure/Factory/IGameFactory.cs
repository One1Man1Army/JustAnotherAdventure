using JAA.Structure.Services;
using UnityEngine;

namespace JAA.Structure.Factory
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject initialPoint);
		void CreateHud();
	}
}