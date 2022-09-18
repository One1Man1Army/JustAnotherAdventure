using System.Collections;
using UnityEngine;

namespace JAA.Structure
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}