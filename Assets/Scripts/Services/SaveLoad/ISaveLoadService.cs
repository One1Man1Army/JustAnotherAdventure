using JAA.Data;

namespace JAA.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		PlayerProgress LoadProgress();
	}
}