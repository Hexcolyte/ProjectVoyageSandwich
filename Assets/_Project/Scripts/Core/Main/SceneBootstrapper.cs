using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoyageSandwich.Core.Main
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] Orchestrator _orchestrator;
        void Awake()
        {
            _orchestrator.StartOrchestrating();
        }
    }
}
