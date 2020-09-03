using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EventHub
{
    private static Dictionary<Type, List<object>> eventDictionary = new Dictionary<Type, List<object>>();

    private static List<Type> optionalEventTypes = new List<Type>
    {
        typeof(Signal.PurchaserInitialized)
    };
    
    public static void AddOptionalEventType(Type type)
    {
        optionalEventTypes.Add(type);
    }

    public static void Subscribe<TSignal>(Action<TSignal> callback)
    {
        if (eventDictionary.TryGetValue(typeof(TSignal), out var callbacks))
        {
            if (!callbacks.Contains(callback))
                callbacks.Add(callback);
        }
        else
        {
            callbacks = new List<object> {callback};
            eventDictionary[typeof(TSignal)] = callbacks;
        }
    }

    public static void Unsubscribe<TSignal>(Action<TSignal> callback)
    {
        if (eventDictionary.ContainsKey(typeof(TSignal)))
        {
            eventDictionary[typeof(TSignal)].Remove(callback);
        }
    }

    public static void Unsubscribe(Type type, object callback)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type].Remove(callback);
        }
    }

    public static void Fire<TSignal>() where TSignal : class // TSignal : new() => new TSignal()
    {
        if (eventDictionary.TryGetValue(typeof(TSignal), out var callbacks))
        {
            callbacks.ToList().ForEach(callback => ((Action<TSignal>) callback)(null));
        }
        else if (!optionalEventTypes.Contains(typeof(TSignal)))
        {
            Debug.LogWarning("No listener of type " + typeof(TSignal));
        }
    }

    public static void Fire<TSignal>(TSignal signal)
    {
        if (eventDictionary.TryGetValue(typeof(TSignal), out var callbacks))
        {
            callbacks.ToList().ForEach(callback => ((Action<TSignal>) callback)(signal));
        }
        else if (!optionalEventTypes.Contains(typeof(TSignal)))
        {
            Debug.LogWarning("No listener of type " + signal.GetType());
        }
    }
}
