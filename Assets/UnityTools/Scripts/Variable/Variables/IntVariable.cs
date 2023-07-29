using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int Variable")]
public class IntVariable : BaseVariable<int, IntEvent>
{
    public override void Add(int val) => Value += val;

    public override void AddOne() => Value++;

    public override bool IsMax() => Value >= MaxValue;

    public override bool IsMax(int val) => val >= MaxValue;

    public override bool Minus(int val, bool clampToZero = false)
    {
        if (clampToZero && Value - val <= 0)
            Value = 0;
        else
            Value -= val;

        return true;
    }

    public override void MinusOne() => Value--;

    public override bool CanMinus(int val) => Value - val >= 0;

    public override int FillToMax(bool raiseEvent = true)
    {
        if (raiseEvent)
            SetValue(MaxValue);
        else
            SetValueWithoutTriggeringEvent(MaxValue);

        int valueToMax = MaxValue - Value;
        return valueToMax;
    }

    public override int RemainingToMax() => MaxValue - Value;
}