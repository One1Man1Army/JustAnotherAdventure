using System;
using System.Collections.Generic;
using JAA.Logic;

namespace JAA.Structure
{
	public class GameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_states = new Dictionary<Type, IExitableState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
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
