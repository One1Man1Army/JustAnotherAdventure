using JAA.Structure.AssetManagement;
using UnityEngine;

namespace JAA.Structure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}
		public GameObject CreateHero(GameObject initialPoint) => 
			_assets.Instantiate(AssetPath.HeroPath, initialPoint.transform.position);

		public void CreateHud() => 
			_assets.Instantiate(AssetPath.HudPath);
	}
}