using System;
using UnityEngine;

namespace JAA.UI
{
	public class LookAtCamera : MonoBehaviour
	{
		private Transform _mainCam;

		private void Start() => 
			_mainCam = Camera.main.transform;

		private void Update()
		{
			Quaternion rotation = _mainCam.rotation;
			transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
		}
	}
}