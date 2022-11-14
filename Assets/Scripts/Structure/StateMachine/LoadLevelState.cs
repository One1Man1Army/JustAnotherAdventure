using System;
using JAA.Structure.Factory;
using JAA.CameraLogic;
using JAA.Hero;
using JAA.Logic;
using JAA.Services.PersistentProgress;
using JAA.UI;
using UnityEngine;

namespace JAA.Structure.StateMachine
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private const string EnemySpawnerTag = "EnemySpawner";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _persistentProgressService;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_persistentProgressService = persistentProgressService;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.CleanUp();
			_sceneLoader.Load(sceneName, OnLoaded);
		}
		
		public void Exit()
		{
			_loadingCurtain.Hide();
		}

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();
			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach (var progressReader in _gameFactory.ProgressReaders)
			{
				progressReader.LoadProgress(_persistentProgressService.progress);
			}
		}

		private void InitGameWorld()
		{
			InitSpawners();
			var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			CameraFollow(hero);
			InitHud(hero);
		}

		private void InitSpawners()
		{
			foreach (var spawner in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
			{
				var enemySpawner = spawner.GetComponent<EnemySpawner>();
				_gameFactory.Register(enemySpawner);
			}
		}

		private void InitHud(GameObject hero)
		{
			GameObject hud = _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
		}

		private void CameraFollow(GameObject hero) 
			=> Camera.main.GetComponent<CameraFollow>().Follow(hero);
	}
}