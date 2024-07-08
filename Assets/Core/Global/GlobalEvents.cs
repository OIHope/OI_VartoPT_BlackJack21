using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System;
using System.Collections.Generic;

namespace Assets.Core.Global
{
    public static class GlobalEvents
    {
        public const string ON_CARD_IS_TAKEN = ("ON_CARD_IS_TAKEN");
        public const string ON_PLAYER_FINISHED_TURN = ("ON_PLAYER_FINISHED_TURN");

        private static Dictionary<string, Delegate> _eventDictionary = new()
        {
            { ON_PLAYER_FINISHED_TURN, (Action<Player>)((_) => {}) },
            { ON_CARD_IS_TAKEN, (Action<Hand>)((_) => {}) },
        };

        public static void Subscribe(string eventName, Action collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action)_eventDictionary[eventName] + collback;
            }
        }
        public static void Subscribe<T>(string eventName, Action<T> collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action<T>)_eventDictionary[eventName] + collback;
            }
        }

        public static void Unsubscribe(string eventName, Action collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action)_eventDictionary[eventName] - collback;
            }
        }
        public static void Unsubscribe<T>(string eventName, Action<T> collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action<T>)_eventDictionary[eventName] - collback;
            }
        }

        public static void InvokeEvent(string eventName)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                ((Action)_eventDictionary[eventName])?.Invoke();
            }
        }
        public static void InvokeEvent<T>(string eventName, T parameter)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                ((Action<T>)_eventDictionary[eventName])?.Invoke(parameter);
            }
        }
    }
}