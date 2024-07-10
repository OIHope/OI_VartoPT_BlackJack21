using Assets.Core.Hands;
using Assets.Core.PlayerContainer;
using System;
using System.Collections.Generic;

namespace Assets.Core.Global
{
    public static class GlobalEvents
    {
        public const string ON_PLAYER_TAKES_TURN = ("ON_PLAYER_TAKE_TURN");
        public const string ON_BOT_TAKES_TURN = ("ON_BOT_TAKE_TURN");
        public const string ON_CARDS_REVEALED = ("ON_CARDS_REVEALED");
        public const string ON_PLAYER_WIN = ("ON_PLAYER_WIN");
        public const string ON_PLAYER_LOSE = ("ON_PLAYER_LOSE");
        public const string ON_DRAW = ("ON_DRAW");
        public const string ON_RESTART_TRIGGERED = ("ON_RESTART_TRIGGERED");

        public const string ON_BOT_WAITS = ("ON_BOT_WAITS");
        public const string ON_BOT_TAKES_CARD = ("ON_BOT_TAKES_CARD");
        public const string ON_BOT_THINKS = ("ON_BOT_THINKS");
        public const string ON_BOT_PASS_TURN = ("ON_BOT_PASS_TURN");

        public const string ON_CARD_IS_TAKEN = ("ON_CARD_IS_TAKEN");
        public const string ON_PLAYER_TAKES_CARD = ("ON_PLAYER_TAKES_CARD");
        public const string ON_PLAYER_FINISHED_TURN = ("ON_PLAYER_FINISHED_TURN");
        public const string ON_SCORE_UPDATED = ("ON_SCORE_UPDATED");

        private static Dictionary<string, Delegate> _eventDictionary = new()
        {
            { ON_PLAYER_TAKES_TURN, (Action)(() => {}) },
            { ON_BOT_TAKES_TURN, (Action)(() => {}) },
            { ON_CARDS_REVEALED, (Action)(() => {}) },
            { ON_PLAYER_WIN, (Action)(() => {}) },
            { ON_PLAYER_LOSE, (Action)(() => {}) },
            { ON_DRAW, (Action)(() => {}) },
            { ON_RESTART_TRIGGERED, (Action)(() => {}) },

            { ON_BOT_WAITS, (Action)(() => {}) },
            { ON_BOT_TAKES_CARD, (Action)(() => {}) },
            { ON_BOT_THINKS, (Action)(() => {}) },
            { ON_BOT_PASS_TURN, (Action)(() => {}) },

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