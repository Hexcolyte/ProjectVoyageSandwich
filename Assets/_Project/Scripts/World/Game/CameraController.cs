using UnityEngine;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.World.Game
{
    public class CameraController: BaseComponent
    {
        [SerializeField] private Camera _camera;

        public Camera GetCamera() => _camera;
        public float GetCameraAspectRatio() => _camera.aspect;

        public override void Initialize()
        {
        }
    }
}