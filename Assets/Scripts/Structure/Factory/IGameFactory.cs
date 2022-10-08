using System;
using System.Collections.Generic;
using JAA.Services;
using JAA.Services.PersistentProgress;
using UnityEngine;

namespace JAA.Structure.Factory
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject initialPoint);
		GameObject HeroObject { get; }
		event Action HeroCreated;
		void CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
	}
}