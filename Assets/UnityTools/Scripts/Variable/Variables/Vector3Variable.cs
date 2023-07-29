using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector3 Variable")]
public class Vector3Variable : BaseVariable<Vector3, Vector3Event>
{
    protected override bool AllowMaxValue => false;

    public override void Add(Vector3 val) => Value += val;

    public override bool Minus(Vector3 val, bool clampToZero = false)
    {
        Value -= val;
        return true;
    }

    public override bool IsZero() => Value == Vector3.zero;
}