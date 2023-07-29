using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Game Events/Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    [Button]
    public void Raise() => Raise(new Void());
}
