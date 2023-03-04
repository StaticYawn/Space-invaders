using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new();

    public void Raise()
    {
        for(int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void Raise(InputAction.CallbackContext ctx)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            if (!ctx.performed) return;
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
