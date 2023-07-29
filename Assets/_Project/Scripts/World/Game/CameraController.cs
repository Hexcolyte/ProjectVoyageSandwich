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
            base.Initialize();

            float aspectRatio = GetCameraAspectRatio();
            float factor = Mathf.InverseLerp(.35f, .8f, aspectRatio);
            float zPos = Mathf.Lerp(-8f, -6f, factor);
            _camera.transform.localPosition = new Vector3(0f, 0f, zPos);

            Debug.Log($"Camera position initialized, Aspect Ratio: {aspectRatio}, Camera Z Position: {zPos}");
        }
    }
}