using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamCore
{

    public class GameSessionBrain : MonoBehaviour
    {
        public string Name => this.gameObject.name;

        public UnityEvent OnGameSectionEnter;
        public Action OnGameUpdate;
        public UnityEvent OnGameSectionExit;

        private void OnEnable()
        {
            OnGameSectionEnter?.Invoke();

            OnEnter();
        }

        private void Update()
        {
            OnGameUpdate?.Invoke();

            OnUpdate();
        }

        private void OnDisable()
        {
            OnGameSectionExit?.Invoke();

            OnExit();
        }


        #region Ovverridable event

        public bool SetActive()
        {
            if (this.gameObject.activeInHierarchy)
            {
                return false;
            }
            else
            {
                this.gameObject.SetActive(true);
                return true;
            }
        }

        public bool SetDeactive()
        {
            if (!this.gameObject.activeInHierarchy)
            {
                return false;
            }
            else
            {
                this.gameObject.SetActive(false);
                return true;
            }
        }
        
        public virtual void OnEnter()
        {
#if UNITY_EDITOR
            Debug.Log($"OnEnter: {Name}");
#endif
        }

        public virtual void OnExit()
        {
#if UNITY_EDITOR

            Debug.Log($"OnExit: {Name}");
#endif
        }

        public virtual void OnUpdate()
        {
#if UNITY_EDITOR

            Debug.Log($"OnUpdate: {Name}");
#endif
        }

        #endregion
    }
}

