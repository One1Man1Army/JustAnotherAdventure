using JAA.Logic;
using JAA.Services.Input;

namespace JAA.Structure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }
    }
}