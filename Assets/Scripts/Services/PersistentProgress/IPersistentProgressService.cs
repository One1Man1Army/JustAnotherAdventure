using JAA.Data;

namespace JAA.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress progress { get; set; }
	}
}