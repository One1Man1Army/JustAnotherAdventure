using JAA.Structure.Services;
using JAA.Structure.StateMachine;
using JAA.Structure.StateMachine.Logic;
using JAA.Structure.StateMachine.Services.Input;

namespace JAA.Structure
{
    public class Game
    {
        public GameStateMachine stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}