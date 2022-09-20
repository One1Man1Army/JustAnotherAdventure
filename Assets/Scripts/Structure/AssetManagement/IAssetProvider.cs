using JAA.Services;
using UnityEngine;

namespace JAA.Structure.AssetManagement
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path, Vector3 at);
		GameObject Instantiate(string path);
	}
}