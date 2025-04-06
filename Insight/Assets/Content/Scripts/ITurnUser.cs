using System;
using UnityEngine;

public abstract class TurnUser : MonoBehaviour
{
    protected virtual void Initialize()
    {
        TurnController.Singleton.RegisterTurnUser(this);
    }
    public abstract void Turn();
}