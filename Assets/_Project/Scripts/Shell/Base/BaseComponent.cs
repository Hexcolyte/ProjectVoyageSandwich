using UnityEngine;

namespace VoyageSandwich.Core.Game
{
    public abstract class BaseComponent: MonoBehaviour
    {
        private void Update() => Tick();

        protected virtual void Tick() {}
        public abstract void Initialize();
    }
}