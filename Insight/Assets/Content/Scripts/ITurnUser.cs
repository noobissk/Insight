using System;
using UnityEngine;

public abstract class TurnUser : MonoBehaviour
{
    public Vector2Int logicPosition;
    protected virtual void Initialize()
    {
        TurnController.Singleton.RegisterTurnUser(this);
    }
    public abstract void Turn();
}