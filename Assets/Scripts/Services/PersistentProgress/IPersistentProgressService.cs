using JAA.Data;

namespace JAA.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}