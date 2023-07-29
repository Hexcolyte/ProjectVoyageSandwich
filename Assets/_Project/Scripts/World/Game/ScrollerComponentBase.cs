using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VoyageSandwich.World.Environment;
using VoyageSandwich.World.Base;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Game
{
    public abstract class ScrollerComponentBase<T>: BaseComponent where T: MovableObjectBase
    {
        [SerializeField] protected T _objectPrefab;

        [Header("Positions")]
        [SerializeField] protected float _positionOffset = 3f;
        [SerializeField] private Transform _anchorTransform;

        protected ObjectPool<T> _objectPool;
        protected Queue<T> _existingObjectQueue = new Queue<T>();
        protected Vector3 _anchorPosition => _anchorTransform.position;

        protected abstract float FinalYPos { get; }

        public override void Initialize()
        {
            base.Initialize();

            _objectPool = new ObjectPool<T>
            (
                () => Instantiate<T>(_objectPrefab), 
                (obj) => obj.Show(),
                (obj) => obj.Hide(),
                (obj) => obj.SelfDestroy(),
                false,
                32,
                100
            );
        }

        public override void Tick(float deltaTime)
        {
            if (_existingObjectQueue.Count <= 0)
                return;

            T firstLand = _existingObjectQueue.Peek();

            if (firstLand.CurrentYPos <= FinalYPos)
            {
                T landToBeRemoved = _existingObjectQueue.Dequeue();
                _objectPool.Release(landToBeRemoved);

                OnLastObjectScrollEnded();
            }

            foreach (T obj in _existingObjectQueue)
            {
                float currentYPos = obj.CurrentYPos;
                currentYPos -= deltaTime;
                
                obj.MoveY(currentYPos);
            }
        }

        protected abstract void OnLastObjectScrollEnded();
    }
}