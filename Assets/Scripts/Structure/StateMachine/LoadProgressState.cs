using System;
using JAA.Data;
using JAA.Services.PersistentProgress;
using JAA.Services.SaveLoad;

namespace JAA.Structure.StateMachine
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_stateMachine = stateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}
		
		public void Enter()
		{
			LoadProgressOrInitNew();
			_stateMachine.Enter<LoadLevelState, string>(_progressService.progress.worldData.positionOnLevel.level);
		}

		public void Exit()
		{
			
		}
		
		private void LoadProgressOrInitNew() => 
			_progressService.progress = _saveLoadService.LoadProgress() ?? NewProgress();

		private PlayerProgress NewProgress() => 
			new PlayerProgress("Main");
	}
}