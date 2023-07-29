using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ParseToStringForTMPText : ParseToStringForText
{
    public TextMeshProUGUI TMPText;

    protected override void ApplyText(string textString) => TMPText.text = textString;
}
