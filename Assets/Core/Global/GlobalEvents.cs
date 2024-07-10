using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System;
using System.Collections.Generic;

namespace Assets.Core.Global
{
    public static class GlobalEvents
    {
        public const string ON_CARD_IS_TAKEN = ("ON_CARD_IS_TAKEN");
        public const string ON_PLAYER_TAKES_CARD = ("ON_PLAYER_TAKES_CARD");
        public const string ON_PLAYER_FINISHED_TURN = ("ON_PLAYER_FINISHED_TURN");
        public const string ON_SCORE_UPDATED = ("ON_SCORE_UPDATED");

        private static Dictionary<string, Delegate> _eventDictionary = new()
        {
            { ON_PLAYER_TAKES_CARD, (Action<Hand,bool>)((_,_) => {}) },
            { ON_CARD_IS_TAKEN, (Action<Hand>)((_) => {}) },
            { ON_PLAYER_FINISHED_TURN, (Action<Player>)((_) => {}) },
            { ON_SCORE_UPDATED, (Action<Hand,int>)((_,_) => {})},
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
        public static void Subscribe<T1,T2>(string eventName, Action<T1,T2> collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action<T1,T2>)_eventDictionary[eventName] + collback;
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
        public static void Unsubscribe<T1, T2>(string eventName, Action<T1, T2> collback)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] = (Action<T1, T2>)_eventDictionary[eventName] - collback;
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
        public static void InvokeEvent<T1,T2>(string eventName, T1 parameter1, T2 parameter2)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                ((Action<T1,T2>)_eventDictionary[eventName])?.Invoke(parameter1, parameter2);
            }
        }
    }
}