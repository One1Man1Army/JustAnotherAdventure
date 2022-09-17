using JAA.Services.Input;

namespace JAA.Structure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine stateMachine;

        public Game()
        {
            stateMachine = new GameStateMachine();
        }
    }
}