using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class ParseToStringForUnityText : ParseToStringForText
{
    public Text Text;

    protected override void ApplyText(string textString) => Text.text = textString;
}
