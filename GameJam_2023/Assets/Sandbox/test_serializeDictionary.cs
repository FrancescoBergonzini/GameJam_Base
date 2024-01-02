using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_code : MonoBehaviour
{
    [Serializable]
    public class ClassTest
    {
        public int life;
        public float damage;

        public enum tipologia
        {
            none = default,
            umano,
            animale,
            artificiales
        }

        public tipologia tipo;

        public arma_data arma;
    }

    [Serializable]
    public struct arma_data
    {
        public string arma;
        bool stato;
    } 

    public SerializedDictionary<int, string> int_string;

    public SerializedDictionary<ClassTest, string> custom_string;
}
