using System;
using MiguelTools;
using MyBox;
using UnityEngine;

public abstract class TurnUser : MonoBehaviour
{
    [ReadOnly] public TileType tileType;
    public Transform visual;
    public Vector2Int logicPosition, oldPosition;
    protected virtual void Initialize()
    {
        TurnController.Singleton.RegisterTurnUser(this);
        oldPosition = logicPosition;
        // visualPosition = logicPosition;
    }
    public abstract void Turn();
}