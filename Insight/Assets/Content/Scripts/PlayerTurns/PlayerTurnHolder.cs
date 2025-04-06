using UnityEngine;
using System;

namespace PlayerTurns
{
    public delegate void PlayerTurn(PlayerCMD i_playerCMD, GridController i_grid);
    public abstract class PlayerTurnHolder : MonoBehaviour
    {
        protected GridController _mapGrid;
        public virtual void Init(GridController i_grid)
        {
            _mapGrid = i_grid;
        }
        public abstract void Turn(PlayerCMD i_playerCMD, GridController i_grid);
    }
}