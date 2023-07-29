using UnityEngine;

namespace VoyageSandwich.Shell.Base
{
    public abstract class BaseComponent: MonoBehaviour
    {
        public virtual void Tick(float deltaTime) {}
        public virtual void Initialize() {}
    }
}