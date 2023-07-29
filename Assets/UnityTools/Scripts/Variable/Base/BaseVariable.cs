using UnityEngine;
using NaughtyAttributes;

public abstract class BaseVariable<T, E> : ScriptableObject, ISerializationCallbackReceiver
    where E : BaseGameEvent<T>
{
    [ResizableTextArea]
    public string Description;

    #region Value
    [Header("Value")]
    public T initialValue;

    [ReadOnly, SerializeField] private T _value;

    public T Value
    {
        get { return _value; }
        set { SetValue(value); }
    }

    protected virtual void SetValueWithoutTriggeringEvent(T val, bool noPreviousValue = false)
    {
        if (StorePreviousValue && !noPreviousValue)
            SetPreviousValue(_value);

        if (HasMaxValue)
        {
            if (IsMax(val))
            {
                val = _maxValue;
            }
        }

        _value = val;
    }

    protected virtual void SetValue(T val, bool noPreviousValue = false)
    {
        SetValueWithoutTriggeringEvent(val, noPreviousValue);
        RaiseValueUpdate();
    }

    #endregion

    #region MaxValue
    [Header("Max Value")]
    [ShowIf("AllowMaxValue")] public bool EnableMaxValue;
    [ShowIf("HasMaxValue")] public T initialMaxValue;

    private bool HasMaxValue => AllowMaxValue && EnableMaxValue;
    protected virtual bool AllowMaxValue => true;

    [ShowIf("HasMaxValue"), ReadOnly, SerializeField] private T _maxValue;
    public T MaxValue
    {
        get { return _maxValue; }
        set { SetMaxValue(value); }
    }

    private void SetMaxValue(T val, bool raiseEvent = true)
    {
        if (!HasMaxValue)
            return;

        _maxValue = val;

        if (raiseEvent)
            RaiseMaxValueUpdate();
    }
    #endregion

    #region Previous Value
    [Header("Previous Value")]
    public bool StorePreviousValue = false;

    [ReadOnly]
    [ShowIf("StorePreviousValue")]
    [SerializeField]
    private T _previousValue;
    public T PreviousValue
    {
        get { return _previousValue; }
        set { SetPreviousValue(value); }
    }

    private void SetPreviousValue(T value)
    {
        _previousValue = value;
        RaisePreviousValueUpdate();
    }

    #endregion

    #region Debug
    [Header("Debug")]
    public T testSetValue;
    [ShowIf("HasMaxValue")] public T testSetMaxValue;

    [Button]
    public void TestSetTo()
    {
        Value = testSetValue;
        MaxValue = testSetMaxValue;
    }
    #endregion

    #region Events
    [Header("Events")]
    public E onValueUpdate;

    [ShowIf("HasMaxValue")] public E onMaxValueUpdate;
    [ShowIf("StorePreviousValue"), Required] public E onPreviousValueUpdate;

    private void RaiseValueUpdate()
    {
        if (onValueUpdate != null)
            onValueUpdate.Raise(_value);
    }

    private void RaiseMaxValueUpdate()
    {
        if (HasMaxValue && onMaxValueUpdate != null)
            onMaxValueUpdate.Raise(_maxValue);
    }

    private void RaisePreviousValueUpdate()
    {
        if (onPreviousValueUpdate != null)
        {
            onPreviousValueUpdate.Raise(_previousValue);
        }
    }
    #endregion

    #region Public Methods
    public void ResetValue()
    {
        _value = initialValue;
        RaiseValueUpdate();
    }

    public void ResetMaxValue()
    {
        _maxValue = initialMaxValue;
        RaiseMaxValueUpdate();
    }

    public void RefreshValue() => RaiseValueUpdate();

    public void RefreshMaxValue() => RaiseMaxValueUpdate();

    #region Add
    public virtual void Add(T val)
    {
        Debug.LogWarning("This variable does not support 'Add'");
    }

    [Button]
    public virtual void AddOne()
    {
        Debug.LogWarning("This variable does not support 'AddOne'");
    }
    #endregion

    #region Minus
    [Button]
    public virtual void MinusOne()
    {
        Debug.LogWarning("This variable does not support 'MinusOne'");
    }

    public virtual bool CanMinus(T val)
    {
        Debug.LogWarning("This variable does not support 'CanMinus'");
        return false;
    }

    public virtual bool Minus(T val, bool clampToZero = false)
    {
        Debug.LogWarning("This variable does not support 'Minus'");
        return false;
    }
    #endregion

    public void SetTo(T val, bool noPreviousValue = false, bool raiseEvent = true)
    {
        if (raiseEvent)
            SetValue(val, noPreviousValue);
        else
            SetValueWithoutTriggeringEvent(val, noPreviousValue);
    }

    public void SetMaxTo(T val, bool raiseEvent = true)
    {
        if (!HasMaxValue)
            return;

        SetMaxValue(val, raiseEvent);
    }

    public void SetPreviousTo(T val) => SetPreviousValue(val);

    public virtual bool IsMax()
    {
        Debug.LogWarning("This variable does not support 'IsMax'");
        return false;
    }

    public virtual bool IsMax(T val)
    {
        Debug.LogWarning("This variable does not support 'IsMax(T)'");
        return false;
    }

    public virtual T FillToMax(bool raiseEvent = true)
    {
        Debug.LogWarning("This variable does not support 'FillToMax'");
        return default(T);
    }

    public virtual T RemainingToMax()
    {
        Debug.LogWarning("This variable does not support 'RemainingToMax'");
        return default(T);
    }

    public virtual bool IsZero()
    {
        Debug.LogWarning("This variable does not support 'IsZero'");
        return false;
    }
    #endregion

    #region Scriptable Object
    private void OnEnable()
    {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void OnAfterDeserialize()
    {
        _value = initialValue;

        if (HasMaxValue)
            _maxValue = initialMaxValue;
    }

    public void OnBeforeSerialize() { }
    #endregion
}