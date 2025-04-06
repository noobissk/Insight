using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerTurns;


public class PlayerCMD : TurnUser
{
    [SerializeField] private GridController _grid;
    private PlayerTurn[] _commands;
    void Start()
    {
        Initialize();
    }
    public void LoadCommands(PlayerTurn[] i_playerTurns)
    {
        _commands = i_playerTurns;
    }

    public override void Turn()
    {
        _commands[TurnController.Singleton.turnID](this, _grid);
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.blue;

        Gizmos.DrawCube(_grid.grid.GetWorldPosition(logicPosition), Vector3.one * 0.1f);
    }
}