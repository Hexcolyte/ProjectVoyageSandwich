using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Variables/String Variable")]
public class StringVariable : BaseVariable<string, StringEvent>
{
    protected override bool AllowMaxValue => false;
}