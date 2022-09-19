using JAA.Structure.AssetManagement;
using JAA.Structure.Factory;
using JAA.Structure.Services;
using JAA.Structure.StateMachine.Services.Input;
using UnityEngine;

namespace JAA.Structure.StateMachine
{
	public class BootstrapState: IState
	{
		private const string Initial = "Initial";
		
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			
			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, EnterLoadLevel);
		}
		
		public void Exit()
		{
			
		}

		private void EnterLoadLevel()
		{
			_stateMachine.Enter<LoadLevelState, string>("Main");
		}

		private void RegisterServices()
		{
			_services.RegisterSingle<IInputService>(InputService());
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
		}

		private static IInputService InputService()
		{
			if (Application.isEditor)
			{
				return new StandaloneInputService();
			}
			else
			{
				return new MobileInputService();
			}
		}
	}
}