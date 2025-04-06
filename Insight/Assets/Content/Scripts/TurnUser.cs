using System;
using UnityEngine;

public abstract class TurnUser : MonoBehaviour
{
    public Transform visual;
    public Vector2Int logicPosition;
    protected virtual void Initialize()
    {
        TurnController.Singleton.RegisterTurnUser(this);
        // visualPosition = logicPosition;
    }
    public abstract void Turn();
}