using System;
using JAA.Data;
using JAA.Structure;
using JAA.Services;
using JAA.Services.Input;
using JAA.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (CurrentLevel() != progress.worldData.positionOnLevel.level) return;
            
            var savedPosition = progress.worldData.positionOnLevel.position;
            if (savedPosition != null)
                Warp(savedPosition);
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.positionOnLevel =
                new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

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