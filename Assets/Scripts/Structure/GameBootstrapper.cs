using JAA.Structure.StateMachine;
using JAA.Structure.StateMachine.Logic;
using UnityEngine;

namespace JAA.Structure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, curtain);
            _game.stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}