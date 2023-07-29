using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour
    where E : BaseGameEvent<T>
    where UER : UnityEvent<T>
{
    [SerializeField] private E gameEvent;

    public E GameEvent { get { return gameEvent; } set { RegisterNewListener(value); } }

    [SerializeField]
    protected UER unityEventResponse;

    void OnEnable()
    {
        if (gameEvent == null) return;

        GameEvent.EventListeners += OnEventRaised;
    }

    void OnDisable()
    {
        if (gameEvent == null) return;

        GameEvent.EventListeners -= OnEventRaised;
    }

    public void OnEventRaised(T item)
    {
        unityEventResponse.Invoke(item);
    }

    void RegisterNewListener(E newEvent)
    {
        if(gameEvent != null)
            GameEvent.EventListeners -= OnEventRaised;

        gameEvent = newEvent;

        GameEvent.EventListeners += OnEventRaised;
    }
}
