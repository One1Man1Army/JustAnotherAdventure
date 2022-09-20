using JAA.Services;
using UnityEngine;

namespace JAA.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}