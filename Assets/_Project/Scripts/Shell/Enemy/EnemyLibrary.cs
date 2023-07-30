using UnityEngine;
using VoyageSandwich.Shell.Enum;

namespace VoyageSandwich.Shell.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/EnemyLibrary")]
    public class EnemyLibrary : ScriptableObject
    {
        [SerializeField] private EnemyTypeInfo[] _enemyTypeLibrary;
        public EnemyTypeInfo[] EnemyTypeLibrary => _enemyTypeLibrary;

        public bool TryGetPossibleSprites(EnemyTypeEnum enemyType, out Sprite[] sprites)
        {
            sprites = null;
            foreach(EnemyTypeInfo typeInfo in _enemyTypeLibrary)
            {
                if (typeInfo.EnemyType == enemyType)
                {
                    sprites = typeInfo.Sprites;
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
        public Sprite[] Sprites;
    }
}