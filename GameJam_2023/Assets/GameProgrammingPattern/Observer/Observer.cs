using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study.GPP
{
    public class Observer : MonoBehaviour
    {

        #region Observer
        public class Entity
        {
            public bool isHero;
        }

        public enum Action
        {
            EVENT_ENTITY_FELL,
            EVENT_ENTITY_SPAWN
            //....
        }

        interface IObserver
        {
            void OnNotify(Entity entity, Action _event);
        }

        class Achivements : IObserver
        {
            public void OnNotify(Entity entity, Action _event)
            {
                switch (_event)
                {
                    case Action.EVENT_ENTITY_FELL:
                        if (entity.isHero && heroIsOnBridge)
                        {
                            unlock(Achivement.HERO_FELL_OF_A_BRIDGE);
                            break;
                        }
                        break;

                        //other event..
                        //update heroIsOnBridge...
                }
            }

            bool heroIsOnBridge;

            enum Achivement
            {
                HERO_FELL_OF_A_BRIDGE,
                HERO_SPAWN
            }

            void unlock(Achivement achivement)
            {
                //Unlock is not already unlocked...
            }
        }

  


        #endregion

        #region subject

        class Subject
        {
            List<IObserver> _observers;
            int _numObservers;

            public void AddObserver(IObserver observer)
            {
                _observers.Add(observer);
            }

            public void RemoveObserver(IObserver observer)
            {
                _observers.Remove(observer);
            }

            void notify(Entity entity, Action _event)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNotify(entity, _event);
                }
            }
        }

        //
        class Physic
        {
            public Subject entityFell;

        }

        class BetterArchivement : IObserver
        {
            Physic _physic;
            public void OnNotify(Entity entity, Action _event)
            {
                throw new NotImplementedException();
            }

            public void ObserveEntityFell()
            {
                _physic.entityFell.AddObserver(this);
            }
        }

        #endregion

        #region C# observer


        class BetterSubject
        {
            List<IBetterObserver> _observers;

            void Attach(IBetterObserver observer)
            {
                _observers.Add(observer);
            }
            void Detach(IBetterObserver observer)
            {
                _observers.Remove(observer);
            }

            //in esempio passiamo solo chi ha notificato, non cosa...
            void Notify(BetterSubject entity)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNotify(entity);
                }
            }
        }

        interface IBetterObserver
        {
            public virtual void OnNotify(BetterSubject subject) { }
        }

        class BetterObserver : IBetterObserver
        {
            public void OnNotify(BetterSubject subject)
            {
                Debug.Log("notificato da " + subject.ToString());


            }

        }

        #endregion

        #region exemple with Action

        public class ActionSubject
        {
            List<ActionObserver> _observers;
            int _numObservers;

            void AddObserver(ActionObserver observer)
            {
                observer.OnNotify += Data;
            }

            void RemoveObserver(ActionObserver observer)
            {
                observer.OnNotify -= Data;
            }

            void notify(Entity entity, Action _event)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNotify.Invoke(_event);
                }
            }
            public void Data(Action data)
            {

            }
        }

        public class ActionObserver
        {
            public System.Action<Action> OnNotify;
        }
        #endregion

        #region Linked observers

        class LinedSubject
        {
            public LinkedObserver _head = null;

            //Methods
            public void AddObserver(LinkedObserver observer)
            {
                observer._next = _head;
                _head = observer;
            }

            public void RemoveObserver(LinkedObserver observer)
            {
                if(_head == observer)
                {
                    _head = observer._next;
                    observer._next = null;
                    return;
                }

                LinkedObserver _current = _head;
                while(_current != null)
                {
                    if(_current._next == observer)
                    {
                        _current._next = observer._next;
                        observer._next = null;
                        return;
                    }

                    _current = _current._next;
                }
            }

            public void Notify(Entity entity, Event _event)
            {
                LinkedObserver observer = _head;
                while(observer != null)
                {
                    observer.OnNotify(entity, _event);
                    observer = observer._next;
                }
            }
        }

        class LinkedObserver
        {
            public LinkedObserver _next = null;

            public void OnNotify(Entity entity, Event _event)
            {
                //
            }

        }
        #endregion
    }

}

