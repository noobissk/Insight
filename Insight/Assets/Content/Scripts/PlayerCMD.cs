using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerTurns;


public class PlayerCMD : TurnUser
{
    private PlayerTurn[] _commands;
    void Start()
    {
        // logicPosition = GridController.Singleton.playerPosition;
        Initialize();
    }
    public void LoadCommands(PlayerTurn[] i_playerTurns)
    {
        _commands = i_playerTurns;
    }

    public override void Turn()
    {
        _commands[TurnController.Singleton.turnID](this, GridController.Singleton);
    }
    void Update()
    {
        // transform.position = GridController.Singleton.grid.GetWorldPosition(logicPosition);
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.blue;

        Gizmos.DrawCube(GridController.Singleton.grid.GetWorldPosition(logicPosition), Vector3.one * 0.1f);
    }
}