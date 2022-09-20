using JAA.Structure.Factory;
using JAA.CameraLogic;
using JAA.Logic;
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

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		private void OnLoaded()
		{
			var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			CameraFollow(hero);
			
			_gameFactory.CreateHud();

			_stateMachine.Enter<GameLoopState>();
		}

		private void CameraFollow(GameObject hero) 
			=> Camera.main.GetComponent<CameraFollow>().Follow(hero);

		public void Exit()
		{
			_loadingCurtain.Hide();
		}
	}
}