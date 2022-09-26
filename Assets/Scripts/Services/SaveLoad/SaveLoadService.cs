using JAA.Data;
using JAA.Services.PersistentProgress;
using JAA.Structure.Factory;
using UnityEngine;

namespace JAA.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{		
		private const string ProgressKey = "Progress";
		
		private readonly IPersistentProgressService _progressService;
		private readonly IGameFactory _gameFactory;

		public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
		{
			_progressService = progressService;
			_gameFactory = gameFactory;
		}
		
		public void SaveProgress()
		{
			foreach (var progressWriter in _gameFactory.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.progress);
			
			PlayerPrefs.SetString(ProgressKey, _progressService.progress.ToJson());
		}

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(ProgressKey)?
				.ToDeserialized<PlayerProgress>();
	}
}