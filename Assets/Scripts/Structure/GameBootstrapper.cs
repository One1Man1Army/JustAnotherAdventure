using JAA.Structure.StateMachine;
using JAA.Logic;
using UnityEngine;

namespace JAA.Structure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}