using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class ParseToString : MonoBehaviour
{
    public string append;
    public bool FormatNumber = true;
    public UnityStringEvent stringEvent;
    public UnityIntEvent intEvent;

    public void ParseIntToString(int myInt)
    {
        if (append == "")
        {
            stringEvent.Invoke(myInt.ToString());
        }
        else
        {
            StringBuilder builder = new StringBuilder();
            stringEvent.Invoke(builder.Append(append).Append(myInt.ToString()).ToString());
        }
    }

    public void ParseSingleToString(Single mySingle)
    {
        if (append == "")
        {
            stringEvent.Invoke(mySingle.ToString());
        }
        else
        {
            StringBuilder builder = new StringBuilder();
            stringEvent.Invoke(builder.Append(append).Append(mySingle.ToString()).ToString());
        }
    }
}

[Serializable]
public class UnityEventString : UnityEvent<string> { }
[Serializable]
public class UnityEventInt : UnityEvent<int> { }
