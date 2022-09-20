using UnityEngine;

namespace JAA.Data
{
	public static class DataExtensions
	{
		public static Vector3Data AsVectorData(this Vector3 vector) => 
			new Vector3Data(vector.x, vector.y, vector.z);

		public static Vector3 AsUnityVector(this Vector3Data vector) =>
			new Vector3(vector.x, vector.y, vector.z);

		public static T ToDeserialized<T>(this string json) =>
			JsonUtility.FromJson<T>(json);
	}
}