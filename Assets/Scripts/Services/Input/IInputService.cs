using JAA.Structure.Services;
using UnityEngine;

namespace JAA.Structure.StateMachine.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}