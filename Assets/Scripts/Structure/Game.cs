using JAA.Services.Input;
using UnityEngine;

namespace JAA.Structure
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            if (Application.isEditor)
            {
                InputService = new StandaloneInputService();
            }
            else
            {
                InputService = new MobileInputService();
            }
        }
    }
}