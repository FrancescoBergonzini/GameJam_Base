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

            void AddObserver(IObserver observer)
            {
                _observers.Add(observer);
            }

            void RemoveObserver(IObserver observer)
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
            public void OnNotify(BetterSubject subject);
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
    }
        
    }

