using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_scale : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.DOScale(2, 1).From(1);
        }
    }
}
