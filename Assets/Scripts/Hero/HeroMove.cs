using System;
using JAA.Data;
using JAA.Structure;
using JAA.Services;
using JAA.Services.Input;
using JAA.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Serialization;

namespace JAA.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed = 4.0f;
        private IInputService _inputService;
        private Camera _camera;
        
        public void LoadProgress(PlayerProgress progress)
        {
            throw new NotImplementedException();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.position = transform.position.AsVectorData();
        }

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}