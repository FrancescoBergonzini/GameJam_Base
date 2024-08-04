using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study.GPP
{
    public interface IObserver
    {
        void OnNotify(object value, NotificationType notificationType);
    }

    public class Subject 
    {
        List<IObserver> _observers = new List<IObserver>();

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify(object value, NotificationType notificationType)
        {
            foreach (IObserver observer in _observers)
            {
                observer.OnNotify(value, notificationType);
            }
        }
    }
}

