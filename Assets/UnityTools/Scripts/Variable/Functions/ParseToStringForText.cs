using System.Text;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;


// [RequireComponent(typeof(Text))]
public abstract class ParseToStringForText : MonoBehaviour
{
    // public Text thisText;
    public bool FormatNumber = true;
    public bool AddZeroInFront = false;
    [ShowIf("AddZeroInFront")]public int ZeroInFrontFormatLength = 3;
    public string append;
    public bool doIncrementAnimation;
    [ShowIf("doIncrementAnimation")] public bool skipFirstIncrementAnimation;
    [ShowIf("doIncrementAnimation")] public float animationDuration = 1f;

    public bool doExtraAppend;
    [ShowIf("doExtraAppend")] public string sameAppend;
    [ShowIf("doExtraAppend")] public string moreAppend;
    [ShowIf("doExtraAppend")] public string lessAppend;

    public bool EmptyStringOnNegativeValue = false;

    private int _lastInt = -1;
    private int _prevInt = 0;
    private bool _skipped = false;

    public void ParseIntToString(int item)
    {
        if (EmptyStringOnNegativeValue && item < 0)
        {
            ApplyText(string.Empty);
            return;
        }

        if (doIncrementAnimation)
        {
            if (skipFirstIncrementAnimation && !_skipped)
            {
                _skipped = true;
                SetText(item, _lastInt);
                return;
            }

        }
        else
        {
            SetText(item, _lastInt);
        }
    }

    private void SetText(int item, int lastInt)
    {
        _prevInt = item;

        if (append == "" && !doExtraAppend)
        {
            ApplyText(item.ToString());

            return;
        }

        StringBuilder builder = new StringBuilder();

        if (doExtraAppend && lastInt != -1)
        {
            if (lastInt == item)
                builder.Append(AddZeroInFront ? sameAppend.PadLeft(ZeroInFrontFormatLength, '0') : sameAppend);
            else if (lastInt < item)
                builder.Append(AddZeroInFront ? moreAppend.PadLeft(ZeroInFrontFormatLength, '0') : moreAppend);
            else
                builder.Append(AddZeroInFront ? lessAppend.PadLeft(ZeroInFrontFormatLength, '0') : lessAppend);
        }

        builder.Append(append).Append(item.ToString());
        ApplyText(builder.ToString());
    }

    protected abstract void ApplyText(string textString);

    public void SetLastInt(int i)
    {
        if (i == -1)
            Reset();
        else
            _lastInt = i;
    }

    public void Reset()
    {
        _lastInt = -1;
        _skipped = false;
    }
}
