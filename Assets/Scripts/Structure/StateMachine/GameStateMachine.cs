using System;
using System.Collections.Generic;
using JAA.Structure.Factory;
using JAA.Services;
using JAA.Logic;
using JAA.Services.PersistentProgress;
using JAA.Services.SaveLoad;

namespace JAA.Structure.StateMachine
{
	public class GameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
		{
			_states = new Dictionary<Type, IExitableState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
				[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
				[typeof(GameLoopState)] = new GameLoopState(this)
			};
		}
		
		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChaneState<TState>();
			state.Enter();
		}
		
		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChaneState<TState>();
			state.Enter(payload);
		}
		
		public TState ChaneState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();
			
			TState state = GetState<TState>();
			_activeState = state;
			
			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
}
