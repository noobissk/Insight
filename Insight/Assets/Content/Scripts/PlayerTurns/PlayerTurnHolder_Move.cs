using MiguelTools;
using UnityEngine;

namespace PlayerTurns
{
    public class PlayerTurnHolder_Move : PlayerTurnHolder
    {
        [SerializeField] Vector2Int vector;
        public override void Turn(PlayerCMD i_playerCMD, GridController i_grid)
        {
            // Vector3Int v = i_grid.WorldToCell(i_playerCMD.transform.position);
            
            TileType newPosTileType = i_grid.grid.GetGridValue(i_playerCMD.logicPosition + vector).type;

            if (newPosTileType != TileType.Wall)
                i_playerCMD.logicPosition += vector;
            else
                i_playerCMD.visual.localScale = new Vector3(0.75f, 0.75f, 1);
        }
    }
}
