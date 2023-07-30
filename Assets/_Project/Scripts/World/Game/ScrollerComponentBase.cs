using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VoyageSandwich.World.Environment;
using VoyageSandwich.World.Base;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Game
{
    public abstract class ScrollerComponentBase<T1, T2>: BaseComponent
        where T1: MovableObjectBase<T2>
        where T2: MovableObjectRuntimeData
    {
        [SerializeField] protected T1 _objectPrefab;

        [Header("Positions")]
        [SerializeField] protected float _positionOffset = 3f;
        [SerializeField] private Transform _anchorTransform;

        protected ObjectPool<T1> _objectPool;
        protected Queue<T1> _existingObjectQueue = new Queue<T1>();
        protected Vector3 _anchorPosition => _anchorTransform.position;

        protected abstract float FinalYPos { get; }
        protected virtual float CurrentSongTime { get; }

        public override void Initialize()
        {
            base.Initialize();

            _objectPool = new ObjectPool<T1>
            (
                () => Instantiate<T1>(_objectPrefab), 
                (obj) => obj.Show(),
                (obj) => obj.Hide(),
                (obj) => obj.SelfDestroy(),
                false,
                32,
                100
            );
        }

        protected virtual void MoveOneStep()
        {
            if (_existingObjectQueue.Count <= 0)
                return;

            foreach (T1 obj in _existingObjectQueue)
            {
                float currentYPos = obj.CurrentYPos;
                currentYPos -= _positionOffset;
                
                obj.MoveY(currentYPos);
            }

            T1 firstObj = _existingObjectQueue.Peek();
            if (firstObj.CurrentYPos <= FinalYPos)
            {
                if (RemoveObject(out T1 objToBeRemoved))
                    OnLastObjectScrollEnded(objToBeRemoved);
            }
        }

        protected virtual bool RemoveObject(out T1 objToBeRemoved)
        {
            objToBeRemoved = null;

            if (_existingObjectQueue.Count == 0)
                return false;

            objToBeRemoved = _existingObjectQueue.Dequeue();
            _objectPool.Release(objToBeRemoved);

            return true;
        }

        protected abstract void OnLastObjectScrollEnded(T1 lastRemovedObject);
    }
}