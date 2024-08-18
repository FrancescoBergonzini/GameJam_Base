using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using DG.Tweening;

namespace ScalEnigma
{
    public class ScanlEnigma : GameManagerBase
    {
        public new static ScanlEnigma Instance;

        public void Awake()
        {
            Instance = this;
        }

        [Space]
        public float swich_duration;
        public Ease swich_ease;

        public bool CanSelectObjects = true;


        public void SwichSize(BaseObject one, BaseObject two)
        {
            var one_dimension = one.GetSize();
            var two_dimension = two.GetSize();

            one.SwichSize(two_dimension);
            two.SwichSize(one_dimension);

            //safe wait routine
            StartCoroutine(safe_wait_routine());
            
            IEnumerator safe_wait_routine()
            {
                CanSelectObjects = false;

                yield return new WaitForSeconds(swich_duration);

                CanSelectObjects = true;

                one.DisableSelect(true);
                two.DisableSelect(true);
            }
        }

        [Space]
        public BaseObject one;
        public BaseObject two;
        public void OnObjectSelected(BaseObject baseObject)
        {
            if(one == null)
            {
                one = baseObject;
                return;
            }

            if(two == null)
            {
                two = baseObject;
            }

            SwichSize(one, two);
        }

        public void OnObjectDeselected(BaseObject baseObject)
        {
            if(one == baseObject)
            {
                one = null;
            }

            if(two == baseObject)
            {
                two = null;
            }
        }
    }
}

