using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JAA.Structure
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public SceneLoader(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null)
		{
			if (SceneManager.GetActiveScene().name == name)
			{
				onLoaded?.Invoke();
				return;
			}
			
			_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
		}

		private IEnumerator LoadScene(string name, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

			while (!waitNextScene.isDone)
			{
				yield return null;
			}
			
			onLoaded?.Invoke();
		}
	}
}