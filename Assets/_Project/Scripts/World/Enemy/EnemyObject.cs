using UnityEngine;
using VoyageSandwich.Shell.Base;

using VoyageSandwich.World.Base;

namespace VoyageSandwich.World.Enemy
{
    public class EnemyObject : MovableObjectBase
    {
        public void Rotate(Quaternion newRotation)
        {
            transform.rotation = newRotation;
        }
    }
}