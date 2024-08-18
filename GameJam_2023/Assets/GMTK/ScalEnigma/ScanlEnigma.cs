using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace ScalEnigma
{
    public static class Layers
    {
        //new layers test
        public const int Player = 10;
        public const int Object = 11;
        public const int Edge = 12;

    }

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
            var one_size = one.current_size;
            var two_size = two.current_size;

            one.SwichSize(two_size);
            two.SwichSize(one_size);

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

        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

