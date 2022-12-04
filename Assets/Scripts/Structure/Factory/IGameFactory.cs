using System;
using System.Collections.Generic;
using JAA.Enemies;
using JAA.Services;
using JAA.Services.PersistentProgress;
using JAA.Structure.StaticData;
using UnityEngine;

namespace JAA.Structure.Factory
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject initialPoint);
		GameObject CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
		void Register(ISavedProgressReader reader);
		GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent);
		LootPiece CreateLoot();
	}
}