using JAA.Services;
using JAA.Structure.StateMachine;
using JAA.Logic;

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