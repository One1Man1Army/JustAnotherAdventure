using JAA.Structure.AssetManagement;
using JAA.Structure.Factory;
using JAA.Services;
using JAA.Services.Input;
using JAA.Services.PersistentProgress;
using JAA.Services.SaveLoad;
using JAA.Structure.Services.StaticData;
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
			_stateMachine.Enter<LoadProgressState>();
		}

		private void RegisterServices()
		{
			_services.RegisterSingle<IInputService>(InputService());
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			RegisterStaticDataService();
			_services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()));
			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
		}

		private void RegisterStaticDataService()
		{
			var staticData = new StaticDataService();
			staticData.LoadMonsters();
			_services.RegisterSingle<IStaticDataService>(staticData);
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