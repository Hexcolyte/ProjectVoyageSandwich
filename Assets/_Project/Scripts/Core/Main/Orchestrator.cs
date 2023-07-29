using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.World.Game;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.Core.Main
{
    public class Orchestrator : MonoBehaviour
    {
        [SerializeField] private BaseComponent[] _components;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LandManager _landManager;

        public void StartOrchestrating()
        {
            _cameraController.Initialize();
            _landManager.Initialize(_cameraController);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _cameraController.Tick(deltaTime);
            _landManager.Tick(deltaTime);
        }
    }
}
