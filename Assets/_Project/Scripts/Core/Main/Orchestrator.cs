using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.World.Game;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.Core.Main
{
    public class Orchestrator : MonoBehaviour
    {
        [SerializeField] private BaseComponent[] _components;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LandManager _landManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private Conductor _conductor;

        public void StartOrchestrating()
        {
            _cameraController.Initialize();
            _landManager.Initialize(_cameraController);
            _enemyManager.Initialize(_cameraController, _conductor);
            _conductor.Initialize();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _cameraController.Tick(deltaTime);
            _landManager.Tick(deltaTime);
            _enemyManager.Tick(deltaTime);
            _conductor.Tick(deltaTime);
        }
    }
}
