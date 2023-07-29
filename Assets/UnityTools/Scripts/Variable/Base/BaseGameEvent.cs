using System;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
    public event Action<T> EventListeners = delegate {};

    void OnEnable()
    {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void Raise(T item)
    {
        EventListeners(item);
    }
}
