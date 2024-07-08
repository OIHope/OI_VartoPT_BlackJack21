using System;
using System.Collections.Generic;

namespace Assets.Core.Global
{
    public static class GlobalEvents
    {
        public const string EVENT_01 = ("EVENT_01");

        private static Dictionary<string, Delegate> _eventDictionary = new()
        {
            { EVENT_01, (Action)(() => {}) },
        };

        public static void Subscribe(string eventName, Action collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action)_eventDictionary[eventName] + collback;
            }
        }
        public static void Unsubscribe(string eventName, Action collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action)_eventDictionary[eventName] - collback;
            }
        }
        public static void InvokeEvent(string eventName)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                ((Action)_eventDictionary[eventName])?.Invoke();
            }
        }
    }
}