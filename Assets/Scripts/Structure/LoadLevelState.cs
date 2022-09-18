using System;
using JAA.CameraLogic;
using JAA.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JAA.Structure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private const string HeroPath = "Hero/Hero";
		private const string HudPath = "Hud/Hud";
		
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		private void OnLoaded()
		{
			var initialPoint = GameObject.FindWithTag(InitialPointTag);
			var hero = Instantiate(HeroPath, initialPoint.transform.position);
			CameraFollow(hero);
			
			Instantiate(HudPath);
			
			_stateMachine.Enter<GameLoopState>();
		}

		private static GameObject Instantiate(string path)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab);
		}
		
		private static GameObject Instantiate(string path, Vector3 at)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, at, Quaternion.identity);
		}
		
		private void CameraFollow(GameObject hero) 
			=> Camera.main.GetComponent<CameraFollow>().Follow(hero);

		public void Exit()
		{
			_loadingCurtain.Hide();
		}
	}
}