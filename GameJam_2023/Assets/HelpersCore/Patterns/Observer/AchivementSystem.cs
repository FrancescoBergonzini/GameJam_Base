using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore
{

    public class AchivementSystem : IObserver
    {
        public void OnInizialize()
        {
            //allo start, ma meglio ogni volta che viene istanziato un nuovo poi da osservare...
            foreach (var poi in Object.FindObjectsOfType<PointOfInterest>())
            {
                poi.TriggerEvent.RegisterObserver(this);
            }
        }

        public void OnNotify(object value, NotificationType notificationType)
        {
            if(notificationType == NotificationType.AchivementUnlock)
            {
                //logica archivement sbloccato..
            }
        }
    }

    public enum NotificationType
    {
        none = 0,
        AchivementUnlock = 1,
        triggerEvent = 1

    }

    public class PointOfInterest : MonoBehaviour
    {
        private Subject _triggerEvent;

        //qua uso un subject come oggetto per poter lavorare senza ereditarietà
        //normale implementazione ha POintOfInterest : Subject con Subject Monobehaviour..
        public Subject TriggerEvent => _triggerEvent;

        private void OnTriggerEnter(Collider other)
        {
            _triggerEvent.Notify(value: this.name, NotificationType.AchivementUnlock);
        }
    }
}

