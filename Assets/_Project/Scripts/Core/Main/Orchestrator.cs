using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.World.Game;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Audio;
using VoyageSandwich.Core.Player;

namespace VoyageSandwich.Core.Main
{
    public class Orchestrator : MonoBehaviour
    {
        [SerializeField] private BaseComponent[] _components;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LandManager _landManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private Conductor _conductor;
        [SerializeField] private PlayerController _playerController;

        public void StartOrchestrating()
        {
            _conductor.Initialize();
            _cameraController.Initialize();
            _landManager.Initialize(_cameraController, _conductor);
            _enemyManager.Initialize(_cameraController, _conductor);
            _conductor.Initialize();
            _playerController.Initialize();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _cameraController.Tick(deltaTime);
            _landManager.Tick(deltaTime);
            _enemyManager.Tick(deltaTime);
            _conductor.Tick(deltaTime);
            _playerController.Tick(deltaTime);
        }
    }
}
