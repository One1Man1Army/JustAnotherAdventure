using System;
using UnityEngine;

namespace JAA.Structure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            _game.stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}