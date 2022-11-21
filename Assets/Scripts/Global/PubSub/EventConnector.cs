using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJ2022.Global.PubSub
{
    public class EventConnector : MonoBehaviour
    {
        private Dictionary<string, UnityEvent<object>> _eventDictionary;
        public static EventConnector EventInstance;
        private static EventConnector _eventConnector;
        private static EventConnector Instance
        {
            get
            {
                if (!_eventConnector)
                    _eventConnector = FindObjectOfType<EventConnector>();
                _eventConnector?.Init();

                return _eventConnector;
            }
        }

        private void Awake()
        {
            if (EventInstance != null & EventInstance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                EventInstance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Init()
        {
            if (_eventDictionary == null)
                _eventDictionary = new();
        }

        /// <summary>
        /// Subscribe to a specific event and get an info when event send an update.
        /// </summary>
        /// <param name="eventName">The name of event.</param>
        /// <param name="listener">The event listener to process event update.</param>
        public static void Subscribe(string eventName, UnityAction<object> listener)
        {
            print(eventName);
            print(listener.Method.Name);
            if (listener == null) return;
            if (Instance._eventDictionary.TryGetValue(
                eventName, out UnityEvent<object> thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new();
                thisEvent.AddListener(listener);
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }
        /// <summary>
        /// Unsubscribe from a specific event and stop receiving an info when event send an update.
        /// </summary>
        /// <param name="eventName">The name of event.</param>
        /// <param name="listener">The event listener to process event update.</param>
        public static void Unsubscribe(string eventName, UnityAction<object> listener)
        {
            if (listener == null) return;
            if (Instance == null) return;
            if (Instance._eventDictionary.TryGetValue(
                eventName, out UnityEvent<object> thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        /// <summary>
        /// Publish/Send an update to the subscriber wrapped in message object.
        /// </summary>
        /// <param name="eventName">The name of event.</param>
        /// <param name="message">The update message that want to be sent.</param>
        public static void Publish(string eventName, object message)
        {
            if (message == null) return;
            if (Instance._eventDictionary.TryGetValue(
                eventName, out UnityEvent<object> thisEvent))
            {
              thisEvent.Invoke(message);
            }
        }
    }
}