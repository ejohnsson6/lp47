﻿
using System;

using UnityEngine;

public class KillableFromAbove : MonoBehaviour
{
    private Action<int> onDamageTaken;

    public virtual void TakeDamage(int amount)
    {
        onDamageTaken?.Invoke(amount);
    }


    public void RegisterOnDamageTaken(Action<int> cb)
    {
        onDamageTaken += cb;
    }
}
