using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.World.Scene;

namespace VoyageSandwich.Core.Main
{
    public class Orchestrator : MonoBehaviour
    {
        [SerializeField] CameraController _cameraController;
        public void StartOrchestrating()
        {
            _cameraController.Initialize();
        }
    }
}
