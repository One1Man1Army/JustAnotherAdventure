using System;
using JAA.Structure.Factory;
using JAA.CameraLogic;
using JAA.Logic;
using JAA.Services.PersistentProgress;
using UnityEngine;

namespace JAA.Structure.StateMachine
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";

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
			var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			CameraFollow(hero);
			_gameFactory.CreateHud();
		}

		private void CameraFollow(GameObject hero) 
			=> Camera.main.GetComponent<CameraFollow>().Follow(hero);
	}
}