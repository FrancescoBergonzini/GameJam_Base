using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using DG.Tweening;

namespace GameJamCore
{
    public static class UnityUtilities
    {

        #region Values and ranges

        [Serializable]
        public class Range
        {
            public float min, max;
        }

        // A value that will be randomized multiplying it by the random range
        [Serializable]
        public class RandomizedValue
        {
            public float value;
            public Range random;

            [Tooltip("return value * UnityEngine.Random.Range(random.min, random.max);")]
            public float Value
            {
                get
                {
                    // use max for general rule
                    if (random.min == 0 && random.max == 0)
                        return value;
                    else
                        return value * UnityEngine.Random.Range(random.min, random.max);
                }
            }
        }

        // A range that will be randomized multiplying it by the min/max random ranges
        [Serializable]
        public class RandomizedRange : Range
        {
            public Range random_min;     // value.min * Random.Range(random_min.min, random_min.max),
            public Range random_max;     //idem		

            public Range RangeValue
            {
                get
                {
                    var r = new Range();

                    r.min = min * (random_min.min != 0 && random_min.max != 0 ? UnityEngine.Random.Range(random_min.min, random_min.max) : 1f);
                    r.max = max * (random_max.min != 0 && random_max.max != 0 ? UnityEngine.Random.Range(random_max.min, random_max.max) : 1f);

                    return r;
                }
            }

            public float RandomValueInRange
            {
                get
                {
                    var range = RangeValue;

                    return UnityEngine.Random.Range(range.min, range.max);
                }
            }
        }

        [Serializable]
        public class AnimatedRandomizedValue : RandomizedValue
        {
            public float time = 0;

            [Space]
            public AnimationCurve curve;

            [Space]
            public Ease ease = Ease.Unset;
        }

        [Serializable]
        public class AnimatedRange : Range
        {
            public float delay = 0;
            public float time = 0;

            [Space]
            public AnimationCurve curve;

            [Space]
            public Ease ease = Ease.Unset;
        }

        [Serializable]
        public class AnimatedRandomizedRange : RandomizedRange
        {
            public float time = 0;

            [Space]
            public AnimationCurve curve;

            [Space]
            public Ease ease = Ease.Unset;
        }


        #endregion

        #region Vector3

        // TODO: move in math utility class
        public static Vector3 RandomVector(float min, float max)
        {
            var x = UnityEngine.Random.Range(min, max);
            var y = UnityEngine.Random.Range(min, max);
            var z = 0f;
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Permette di ottenere un vettore randomizzato in Z.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angle_min"></param>
        /// <param name="angle_max"></param>
        /// <returns></returns>
        public static Vector3 RandomizeRangeZAxis(this Vector3 v, float angle_min, float angle_max)
        {
            float randomAngle = UnityEngine.Random.Range(angle_min, angle_max);

            // Create a Quaternion rotation around the Z-axis
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

            // Apply the rotation to a normalized vector
            Vector3 randomizedVector = randomRotation * v;

            // Normalize the result to ensure it's still a unit vector
            randomizedVector.Normalize();

            return randomizedVector;
        }

        /// Vector3.up.RandomizeRangeZAxis(-45, 45) questo randomizza partendo da up con questo angolo di variabilità


        #endregion

        #region Screen

        //non funziona
        public static Vector3 GetRandomPointInViewport(float x_viewport_border = 0.05f, float y_viewport_border = 0.05f)
        {
            float randomX = UnityEngine.Random.Range(0f, 1f);
            float randomY = UnityEngine.Random.Range(0f, 1f);

            Vector3 randomPoint = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, Camera.main.nearClipPlane));

            return randomPoint;


        }

        #endregion
    }
}


