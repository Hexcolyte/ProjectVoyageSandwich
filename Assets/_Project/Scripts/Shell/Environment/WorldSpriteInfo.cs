using UnityEngine;
using UnityEngine.Tilemaps;

namespace VoyageSandwich.Shell.Environment
{
    [CreateAssetMenu(menuName = "World/WorldSpriteInfo")]
    public class WorldSpriteInfo : ScriptableObject
    {
        [SerializeField] private Sprite _landSprite;
        [SerializeField] private Sprite _leftWalkablePathSprite;
        [SerializeField] private Sprite _centerWalkablePathSprite;
        [SerializeField] private Sprite _rightWalkablePathSprite;
        [SerializeField] private SpriteSpawnChance[] _propSpriteChance;

        public Sprite LandSprite => _landSprite;
        public SpriteSpawnChance[] PropSpriteChance => _propSpriteChance;
        public Sprite LeftWalkablePathSprite => _leftWalkablePathSprite;
        public Sprite CenterWalkablePathSprite => _centerWalkablePathSprite;
        public Sprite RightWalkablePathSprite => _rightWalkablePathSprite;
    }

    [System.Serializable]
    public class SpriteSpawnChance
    {
        public Sprite Sprite;
        public int Chance;
    }
}