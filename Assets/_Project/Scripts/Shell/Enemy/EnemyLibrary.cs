using UnityEngine;
using VoyageSandwich.Shell.Enum;

namespace VoyageSandwich.Shell.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/EnemyLibrary")]
    public class EnemyLibrary : ScriptableObject
    {
        [SerializeField] private EnemyTypeInfo[] _enemyTypeLibrary;
        public EnemyTypeInfo[] EnemyTypeLibrary => _enemyTypeLibrary;

        public bool TryGetPossibleAnimation(EnemyTypeEnum enemyType, out AnimatorOverrideController animatorController)
        {
            animatorController = null;
            foreach(EnemyTypeInfo typeInfo in _enemyTypeLibrary)
            {
                if (typeInfo.EnemyType == enemyType)
                {
                    animatorController = typeInfo.AnimatorController;
                    return true;
                }
            }

            return false;
        }
    }

    [System.Serializable]
    public class EnemyTypeInfo
    {
        public EnemyTypeEnum EnemyType;
        public AnimatorOverrideController AnimatorController;
    }
}