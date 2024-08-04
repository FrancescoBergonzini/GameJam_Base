using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeath : MonoBehaviour
{
    public static Action OnDeath;

    public void OnDestroy()
    {
        OnDeath.Invoke();
    }

    [ContextMenu("Death")]
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
