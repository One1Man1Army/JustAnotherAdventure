using UnityEngine;

namespace JAA.Structure.StateMachine.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}