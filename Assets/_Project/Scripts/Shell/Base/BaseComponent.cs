using UnityEngine;

namespace VoyageSandwich.Shell.Base
{
    public abstract class BaseComponent: MonoBehaviour
    {
        private bool _initialized;
        public bool IsInitialized => _initialized;

        public virtual void Tick(float deltaTime) {}

        public virtual void Initialize() 
        {
            _initialized = true;
        }
    }
}