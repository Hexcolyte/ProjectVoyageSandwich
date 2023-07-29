using UnityEngine;
using UnityEngine.Tilemaps;

namespace VoyageSandwich.Shell.Environment
{
    [CreateAssetMenu(menuName = "World/WorldSpriteInfo")]
    public class WorldSpriteInfo : ScriptableObject
    {
        [SerializeField] private Sprite _landSprite;

        public Sprite LandSprite => _landSprite;

    }
}