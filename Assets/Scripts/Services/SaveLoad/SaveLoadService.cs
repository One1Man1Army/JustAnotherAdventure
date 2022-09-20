using JAA.Data;
using UnityEngine;

namespace JAA.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";
		
		public void SaveProgress()
		{
			throw new System.NotImplementedException();
		}

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(ProgressKey)?
				.ToDeserialized<PlayerProgress>();
	}
}