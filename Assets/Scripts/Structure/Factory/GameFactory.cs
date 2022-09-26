using System.Collections.Generic;
using JAA.Services.PersistentProgress;
using JAA.Structure.AssetManagement;
using UnityEngine;

namespace JAA.Structure.Factory
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}
		public GameObject CreateHero(GameObject initialPoint) =>
			InstantiateRegistered(AssetPath.HeroPath, initialPoint.transform.position);

		public void CreateHud() => 
			InstantiateRegistered(AssetPath.HudPath);

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
		{
			var gameObject = _assets.Instantiate(prefabPath, position);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}
		
		private GameObject InstantiateRegistered(string prefabPath)
		{
			var gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}
		
		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (var reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(reader);
		}
		
		private void Register(ISavedProgressReader reader)
		{
			if (reader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
				
			ProgressReaders.Add(reader);
		}
	}
}