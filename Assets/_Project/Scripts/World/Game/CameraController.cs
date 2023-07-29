using UnityEngine;
using VoyageSandwich.Core.Game;

namespace VoyageSandwich.World.Scene
{
    public class CameraController: BaseComponent
    {
        [SerializeField] private Camera _camera;

        public override void Initialize()
        {
            float aspectRatio = _camera.aspect;
        }
    }
}